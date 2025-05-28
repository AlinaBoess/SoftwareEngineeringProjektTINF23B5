
using Microsoft.IdentityModel.Tokens;
using RestaurantReservierung.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using RestaurantReservierung.Data;
using Prometheus;
using RestaurantReservierung.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
/*
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve);*/
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ReservationService>();
builder.Services.AddScoped<RestaurantService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TableService>();
builder.Services.AddScoped<FeedbackService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  // Swagger API registreren

builder.Services.AddHttpContextAccessor();


// Datenban Kontext regestrieren 
builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseLazyLoadingProxies()
        .UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(10, 11, 11))
));

builder.Services.AddSwaggerGen(c =>
{
    // allows Swagger API documentation
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    // Sicherheit mit JWT-Token einrichten
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


// JWT Konfiguration
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactFrontend",
        policy => policy.WithOrigins("http://localhost:3000")  
                         .AllowAnyHeader()
                         .AllowAnyMethod());

    options.AddPolicy("AllowReactFrontend",
        policy => policy.WithOrigins("https://localhost:3000")  
                         .AllowAnyHeader()
                         .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("AllowReactFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();              // WICHTIG: Routing aktivieren
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpMetrics();          // Prometheus-Middleware (nach Routing, vor Endpoints)

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<UserExistsMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();  // Controller-Mapping
    endpoints.MapMetrics();      // Prometheus-Metrics-Endpunkt
});



app.Run();
