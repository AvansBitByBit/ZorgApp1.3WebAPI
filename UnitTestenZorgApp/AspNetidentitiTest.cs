using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace UnitTestenZorgApp
{
    [TestClass]
    public class AspNetIdentityAuthenticationServiceTests
    {
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private AspNetIdentityAuthenticationService _authenticationService;

        [TestInitialize]
        public void Setup()
        {
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _authenticationService = new AspNetIdentityAuthenticationService(_mockHttpContextAccessor.Object);
        }

        [TestMethod] // Test: Kan de geauthenticeerde gebruikers-ID worden opgehaald? Result: OK
        public void KanDeGeauthenticeerdeGebruikersIdWordenOpgehaald_ResultOk()
        {
            // Arrange
            var userId = "123";
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext
            {
                User = claimsPrincipal
            };

            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

            // Act
            var result = _authenticationService.GetCurrentAuthenticatedUserId();

            // Assert
            Assert.AreEqual(userId, result);
        }

        [TestMethod] // Test: Kan de geauthenticeerde gebruikers-ID worden opgehaald als er geen gebruiker is? Result: Null
        public void KanDeGeauthenticeerdeGebruikersIdWordenOpgehaald_GeenGebruiker_ResultNull()
        {
            // Arrange
            var httpContext = new DefaultHttpContext
            {
                User = new ClaimsPrincipal(new ClaimsIdentity())
            };

            _mockHttpContextAccessor.Setup(x => x.HttpContext).Returns(httpContext);

            // Act
            var result = _authenticationService.GetCurrentAuthenticatedUserId();

            // Assert
            Assert.IsNull(result);
        }
    }
}


