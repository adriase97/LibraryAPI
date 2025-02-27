using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace UnitTests.Services
{
    [TestClass]
    public class TokenServiceTests
    {
        private Mock<UserManager<IdentityUser>> _userManagerMock;
        private ITokenService _tokenService;
        private IdentityUser _testUser;

        [TestInitialize]
        public void Setup()
        {
            // Configurar el mock de UserManager
            _userManagerMock = new Mock<UserManager<IdentityUser>>(
                new Mock<IUserStore<IdentityUser>>().Object,
                null, null, null, null, null, null, null, null
            );

            // Crear un usuario de prueba
            _testUser = new IdentityUser
            {
                Id = "123",
                UserName = "TestUser",
                Email = "test@example.com"
            };

            // Simular roles y claims del usuario
            var roles = new List<string> { "Admin", "User" };
            var claims = new List<Claim> { new Claim("CustomClaim", "CustomValue") };

            _userManagerMock.Setup(u => u.GetRolesAsync(It.IsAny<IdentityUser>())).ReturnsAsync(roles);
            _userManagerMock.Setup(u => u.GetClaimsAsync(It.IsAny<IdentityUser>())).ReturnsAsync(claims);

            // Configuración de clave JWT válida (al menos 32 caracteres)
            var configurationData = new Dictionary<string, string>
        {
            { "JwtSettings:Secret", "SuperSecretKeyForTesting_AtLeast32Chars!" },
            { "JwtSettings:Issuer", "TestIssuer" },
            { "JwtSettings:Audience", "TestAudience" },
            { "JwtSettings:ExpireMinutes", "60" }
        };

            var configuration = new ConfigurationBuilder().AddInMemoryCollection(configurationData).Build();

            _tokenService = new TokenService(configuration, _userManagerMock.Object);
        }

        [TestMethod]
        public async Task GenerateTokenAsync_ShouldReturnToken()
        {
            // Act
            var token = await _tokenService.GenerateTokenAsync(_testUser);

            // Assert
            Assert.IsNotNull(token);
            Assert.IsFalse(string.IsNullOrEmpty(token));
        }

        [TestMethod]
        public async Task GenerateTokenAsync_WhenUserIsValid()
        {
            // Act
            var roles = new List<string> { "Admin", "User" };
            var token = await _tokenService.GenerateTokenAsync(_testUser);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var tokenRoles = jwtToken.Claims
                         .Where(c => c.Type == "role")
                         .Select(c => c.Value)
                         .ToList();

            // Assert            
            Assert.IsNotNull(jwtToken);
            Assert.IsTrue(jwtToken.Claims.Any(c => c.Type == JwtRegisteredClaimNames.Sub && c.Value == _testUser.Id));
            Assert.IsTrue(jwtToken.Claims.Any(c => c.Type == JwtRegisteredClaimNames.Email && c.Value == _testUser.Email));
            Assert.IsTrue(jwtToken.Claims.Any(c => c.Type == JwtRegisteredClaimNames.Name && c.Value == _testUser.UserName));
            Assert.IsTrue(jwtToken.Claims.Any(c => c.Type == "name" && c.Value == _testUser.UserName));
            CollectionAssert.AreEquivalent(roles, tokenRoles);
        }
    }


}
