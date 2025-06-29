using NUnit.Framework;
using RestaurantReservierung.Services;
using Moq;
using RestaurantReservierung.Models;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestaurantReservierung.Tests.Unit
{
    [TestFixture]
    public class AuthServiceTests
    {
        private AuthService _authService;
        private Mock<IConfiguration> _configMock;

        [SetUp]
        public void SetUp()
        {
            _configMock = new Mock<IConfiguration>();
            _configMock.Setup(c => c["Jwt:Key"]).Returns("ThisShouldBeARelativelyLongAndSecureSecretKeyString");
            _configMock.Setup(c => c["Jwt:Issuer"]).Returns("TestIssuer");
            _configMock.Setup(c => c["Jwt:Audience"]).Returns("TestAudience");

            _authService = new AuthService(_configMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _authService = null;
            _configMock = null;
        }

        [Test]
        public void HashPasswordForRegistration_KnownPassword_ReturnsExpectedSha256()
        {
            var expected = "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8";
            var hash = _authService.HashPasswordForRegistration("password");

            Assert.That(hash, Is.EqualTo(expected), "HashPasswordForRegistration should produce correct SHA-256 hex");
        }

        [Test]
        public void IsCorrectPassword_WithMatchingHash_ReturnsTrue()
        {
            var raw = "secret123";
            var hashed = _authService.HashPasswordForRegistration(raw);
            var user = new User { Password = hashed };

            Assert.That(_authService.IsCorrectPassword(raw, user), Is.True, "IsCorrectPassword should return true when hashes match");
        }

        [Test]
        public void IsCorrectPassword_WithNonMatchingHash_ReturnsFalse()
        {
            var user = new User { Password = "password" };

            Assert.That(_authService.IsCorrectPassword("anything", user), Is.False, "IsCorrectPassword should return false when hashes do not match");
        }

        [Test]
        public void GenerateJwtToken_ValidUser_ReturnsTokenWithExpectedClaims()
        {
            var user = new User { Email = "test@example.com", Role = "User" };
            var tokenString = _authService.GenerateJwtToken(user);
            Assert.That(string.IsNullOrEmpty(tokenString), Is.False);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(tokenString);

            var emailClaim = token.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email);

            Assert.That(emailClaim?.Value, Is.EqualTo("test@example.com"));
            Assert.That(token.Issuer, Is.EqualTo(_configMock.Object["Jwt:Issuer"]));
            Assert.That(token.Audiences.Contains(_configMock.Object["Jwt:Audience"]), Is.True);
        }

        [Test]
        public void GenerateJwtToken_NullUser_ThrowsArgumentNullException()
        {
            var ex = Assert.Throws<NullReferenceException>(() => _authService.GenerateJwtToken(null));
            Assert.That(ex.Message, Is.Not.Null);
        }
    }
}
