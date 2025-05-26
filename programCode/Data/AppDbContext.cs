using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using RestaurantReservierung.Models;

namespace RestaurantReservierung.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminAction> AdminActions { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<Table> Tables { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=185.228.137.229;port=3306;database=RestaurantReservierung;user=RRes;password=DBRRes23B5", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.11.11-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<AdminAction>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PRIMARY");

            entity.HasIndex(e => e.AdminId, "adminId");

            entity.Property(e => e.EventId)
                .HasColumnType("int(11)")
                .HasColumnName("eventId");
            entity.Property(e => e.ActionDescription)
                .HasMaxLength(511)
                .HasColumnName("actionDescription");
            entity.Property(e => e.ActionPerformed)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("actionPerformed");
            entity.Property(e => e.ActionType)
                .HasColumnType("enum('ADD','MODIFIY','DELETE')")
                .HasColumnName("actionType");
            entity.Property(e => e.AdminId)
                .HasColumnType("int(11)")
                .HasColumnName("adminId");

            entity.HasOne(d => d.Admin).WithMany(p => p.AdminActions)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("AdminActions_ibfk_1");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PRIMARY");

            entity.ToTable("Feedback");

            entity.HasIndex(e => e.RestaurantId, "fk_feedback_restaurant");

            entity.HasIndex(e => e.ReservationId, "reservationId");

            entity.HasIndex(e => e.UserId, "userId");

            entity.Property(e => e.FeedbackId)
                .HasColumnType("int(11)")
                .HasColumnName("feedbackId");
            entity.Property(e => e.Comment)
                .HasMaxLength(511)
                .HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.Rating)
                .HasColumnType("int(11)")
                .HasColumnName("rating");
            entity.Property(e => e.ReservationId)
                .HasColumnType("int(11)")
                .HasColumnName("reservationId");
            entity.Property(e => e.RestaurantId)
                .HasColumnType("int(11)")
                .HasColumnName("restaurantId");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("userId");

            entity.HasOne(d => d.Reservation).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.ReservationId)
                .HasConstraintName("Feedback_ibfk_2");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.RestaurantId)
                .HasConstraintName("fk_feedback_restaurant");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Feedback_ibfk_1");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PRIMARY");

            entity.ToTable("Image");

            entity.Property(e => e.ImageId)
                .HasColumnType("int(11)")
                .HasColumnName("imageId");
            entity.Property(e => e.Data)
                .HasColumnType("mediumblob")
                .HasColumnName("data");
            entity.Property(e => e.MimeType)
                .HasMaxLength(255)
                .HasColumnName("mimeType");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.ReservationId).HasName("PRIMARY");

            entity.ToTable("Reservation");

            entity.HasIndex(e => e.TableId, "tableId");

            entity.HasIndex(e => e.UserId, "userId");

            entity.Property(e => e.ReservationId)
                .HasColumnType("int(11)")
                .HasColumnName("reservationId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("createdAt");
            entity.Property(e => e.EndTime)
                .HasColumnType("timestamp")
                .HasColumnName("endTime");
            entity.Property(e => e.StartTime)
                .HasColumnType("timestamp")
                .HasColumnName("startTime");
            entity.Property(e => e.TableId)
                .HasColumnType("int(11)")
                .HasColumnName("tableId");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("userId");

            entity.HasOne(d => d.Table).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.TableId)
                .HasConstraintName("Reservation_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Reservation_ibfk_1");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.RestaurantId).HasName("PRIMARY");

            entity.ToTable("Restaurant");

            entity.HasIndex(e => e.ImageId, "imageId");

            entity.HasIndex(e => e.UserId, "userId");

            entity.Property(e => e.RestaurantId)
                .HasColumnType("int(11)")
                .HasColumnName("restaurantId");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.ImageId)
                .HasColumnType("int(11)")
                .HasColumnName("imageId");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.OpeningHours)
                .HasMaxLength(255)
                .HasColumnName("openingHours");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("userId");
            entity.Property(e => e.Website)
                .HasMaxLength(255)
                .HasColumnName("website");

            entity.HasOne(d => d.Image).WithMany(p => p.Restaurants)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Restaurant_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Restaurants)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Restaurant_ibfk_1");
        });

        modelBuilder.Entity<Table>(entity =>
        {
            entity.HasKey(e => e.TableId).HasName("PRIMARY");

            entity.ToTable("Table");

            entity.HasIndex(e => e.RestaurantId, "restaurantId");

            entity.Property(e => e.TableId)
                .HasColumnType("int(11)")
                .HasColumnName("tableId");
            entity.Property(e => e.Area)
                .HasMaxLength(255)
                .HasColumnName("area");
            entity.Property(e => e.Capacity)
                .HasColumnType("int(11)")
                .HasColumnName("capacity");
            entity.Property(e => e.RestaurantId)
                .HasColumnType("int(11)")
                .HasColumnName("restaurantId");
            entity.Property(e => e.TableNr)
                .HasColumnType("int(11)")
                .HasColumnName("tableNr");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Tables)
                .HasForeignKey(d => d.RestaurantId)
                .HasConstraintName("Table_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("userId");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasDefaultValueSql("'CUSTOMER'")
                .HasColumnType("enum('CUSTOMER','ADMIN','RESTAURANT_OWNER')")
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
