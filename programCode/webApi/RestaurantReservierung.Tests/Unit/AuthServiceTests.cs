using NUnit.Framework;
using RestaurantReservierung.Services;
using Moq;
using RestaurantReservierung.Models;

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
            _configMock.Setup(c => c["Jwt:Key"]).Returns("MySuperSecretKey");
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

    }
}
