using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ZorgWebApi.Interfaces;
using ZorgWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZorgWebApi.Controllers;
using Microsoft.Extensions.Logging;

namespace UnitTestenZorgApp
{
    [TestClass]
    public class DagboekSystemTests
    {
        private Mock<IDagboekRepository> _mockRepository;
        private Mock<IAuthenticationService> _mockAuthService;
        private DagboekController _controller;
        private Mock<ILogger<DagboekController>> _mockLogger;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IDagboekRepository>();
            _mockAuthService = new Mock<IAuthenticationService>();
            _mockLogger = new Mock<ILogger<DagboekController>>();
            _controller = new DagboekController(_mockLogger.Object, _mockAuthService.Object, _mockRepository.Object);
        }

        [TestMethod] // Test 1: Kan een lijst van dagboekvermeldingen worden opgehaald? Result: OK
        public async Task KanEenLijstVanDagboekvermeldingenWordenOpgehaald_ResultOk()
        {
            // Arrange
            var userId = "123";
            var dagboekEntries = new List<DagboekModel>
            {
                new DagboekModel { ID = 1, Title = "Dag 1", Contents = "Inhoud van dag 1", UserId = userId },
                new DagboekModel { ID = 2, Title = "Dag 2", Contents = "Inhoud van dag 2", UserId = userId }
            };

            _mockAuthService.Setup(auth => auth.GetCurrentAuthenticatedUserId()).Returns(userId);
            _mockRepository.Setup(repo => repo.GetDagboeken(userId)).ReturnsAsync(dagboekEntries);

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(dagboekEntries, okResult.Value);
        }




        [TestMethod] // Test 3: Kan een dagboekvermelding worden bijgewerkt? Result: NoContent
        public async Task KanEenDagboekvermeldingWordenBijgewerkt_ResultNoContent()
        {
            // Arrange
            var userId = "123";
            var dagboek = new DagboekModel { ID = 1, Title = "Dag 1", Contents = "Bijgewerkte inhoud van dag 1", UserId = userId };

            _mockAuthService.Setup(auth => auth.GetCurrentAuthenticatedUserId()).Returns(userId);
            _mockRepository.Setup(repo => repo.UpdateDagboek(dagboek)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Update(dagboek.ID, dagboek);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        
    }
}
