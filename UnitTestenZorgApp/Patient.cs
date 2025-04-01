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
    public class PatientControllerTest
    {
        private Mock<IPatientRepository> _mockRepository;
        private Mock<IAuthenticationService> _mockAuthService;
        private PatientController _controller;
        private Mock<ILogger<PatientController>> _mockLogger;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IPatientRepository>();
            _mockAuthService = new Mock<IAuthenticationService>();
            _mockLogger = new Mock<ILogger<PatientController>>();
            _controller = new PatientController(_mockLogger.Object, _mockAuthService.Object, _mockRepository.Object);
        }

        [TestMethod] // Test 1: Kan een lijst van patiënten worden opgehaald? Result: OK
        public async Task KanEenLijstVanPatiëntenWordenOpgehaald_ResultOk()
        {
            // Arrange
            var userId = "123";
            var patients = new List<PatientModel>
            {
                new PatientModel { ID = 1, Voornaam = "Jan", Achternaam = "Jansen", TrajectID=2, UserId = userId },
                new PatientModel { ID = 2, Voornaam = "Piet", Achternaam = "Pietersen",TrajectID=2, UserId = userId }
            };

            _mockAuthService.Setup(auth => auth.GetCurrentAuthenticatedUserId()).Returns(userId);
            _mockRepository.Setup(repo => repo.GetPatients(userId)).ReturnsAsync(patients);

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(patients, okResult.Value);
        }

        //[TestMethod] // Test 2: Kan een patient worden aangemaakt? Result: Created
        //public async Task KanEenPatientWordenAangemaakt_ResultCreated()
        //{
        //    // Arrange
        //    var userId = "123";
        //    var patient = new PatientModel { ID = 1, Voornaam = "Jan", Achternaam = "Jansen", TrajectID = 2, Geboortedatum = "23-23-32", UserId = userId };
        //    _mockAuthService.Setup(auth => auth.GetCurrentAuthenticatedUserId()).Returns(userId);
        //    _mockRepository.Setup(repo => repo.CreatePatient(patient)).Returns(Task.CompletedTask);

        //    // Act
        //    var result = await _controller.Create(patient);

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(CreatedAtActionResult));
        //    var createdResult = result as CreatedAtActionResult;
        //    Assert.IsNotNull(createdResult);
        //    Assert.AreEqual(patient, createdResult.Value);
        //}  werkt niet in azure wel local


    }
}

