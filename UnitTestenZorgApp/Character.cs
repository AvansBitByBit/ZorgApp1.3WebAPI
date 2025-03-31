using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ZorgWebApi.Interfaces;
using ZorgWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZorgWebApi.Controllers;

namespace UnitTestenZorgApp
{
    [TestClass]
    public class CharacterSystemTests
    {
        private Mock<ICharacterService> _mockService;
        private Mock<IAuthenticationService> _mockAuthService;
        private CharacterController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mockService = new Mock<ICharacterService>();
            _mockAuthService = new Mock<IAuthenticationService>();
            _controller = new CharacterController(_mockService.Object, _mockAuthService.Object);
        }

        [TestMethod] // Test 1: Kan een character worden opgehaald? Result: OK
        public async Task KanEenCharacterWordenOpgehaald_ResultOk()
        {
            // Arrange
            var userId = "123";
            var character = new Character { ID = 1, HairColor = "Blond", SkinColor = "Fair", SpacesuitColor = "Red", Hat = "None", EyeColor = "Blue", Gender = "Male", UserId = userId };

            _mockAuthService.Setup(auth => auth.GetCurrentAuthenticatedUserId()).Returns(userId);
            _mockService.Setup(service => service.GetCharacterByUserIdAsync(userId)).ReturnsAsync(character);

            // Act
            var result = await _controller.GetCharacter();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(character, okResult.Value);
        }

        [TestMethod] // Test 2: Kan een character worden aangemaakt? Result: OK

        public async Task KanEenCharacterWordenAangemaakt_ResultOk()
        {
            var userId = "123";
            var character = new Character
            {
                ID = 1,
                HairColor = "Blond",
                SkinColor = "Fair",
                SpacesuitColor = "Red",
                Hat = "None",
            };

            _mockAuthService.Setup(auth => auth.GetCurrentAuthenticatedUserId()).Returns(userId);
            _mockService.Setup(service => service.GetCharacterByUserIdAsync(userId)).ReturnsAsync((Character)null);

            // Act
            var result = await _controller.CreateCharacter(character);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(character, okResult.Value);
        }



        }
    }
