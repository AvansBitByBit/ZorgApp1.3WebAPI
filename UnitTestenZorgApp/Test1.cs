﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ZorgWebApi.Interfaces;
using ZorgWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZorgWebApi.Controllers;
using Microsoft.Extensions.Logging;
using System;

namespace UnitTestenZorgApp
{
    [TestClass]
    public class AfspraakModelTest
    {
        private Mock<IAfspraakRepository> _mockRepository;
        private Mock<IAuthenticationService> _mockAuthService;
        private AfspraakController _controller;
        private Mock<ILogger<AfspraakController>> _mockLogger;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<IAfspraakRepository>();
            _mockAuthService = new Mock<IAuthenticationService>();
            _mockLogger = new Mock<ILogger<AfspraakController>>();
            _controller = new AfspraakController(_mockLogger.Object, _mockAuthService.Object, _mockRepository.Object);
        }

        [TestMethod] // test 1 Kan er een afspraak worden aangemaakt? result: OK
        public void kanErEenAfspraakGemaaktWorden_ResultOk()
        {
            // Arrange
            AfspraakModel afspraak = new AfspraakModel
            {
                Titel = "Afspraak met dokter",
                NaamDokter = "Dr. Jansen",
                DatumTijd = "2021-06-01 10:00",
                UserId = "123",
                Actief = 1
            };

            // Act
            AfspraakModel result = afspraak;

            // Assert
            Assert.AreEqual(afspraak, result);
        }

        [TestMethod] // Kan een user een afspraak opvragen van een andere user? result: Not OK
        public async Task kanEenUserEenAfspraakOpvragenVanEenAndereUser_ResultNotOk()
        {
            // Arrange
            var currentUserId = "123";
            var otherUserId = "456";
            var afspraak = new AfspraakModel
            {
                ID = Guid.NewGuid(),
                Titel = "Afspraak met dokter",
                NaamDokter = "Dr. Jansen",
                DatumTijd = "2021-06-01 10:00",
                UserId = otherUserId,
                Actief = 1
            };

            _mockAuthService.Setup(auth => auth.GetCurrentAuthenticatedUserId()).Returns(currentUserId);
            _mockRepository.Setup(repo => repo.GetAfspraakById(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(afspraak);

            // Act
            var result = await _controller.GetAfspraakById(afspraak.ID);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(UnauthorizedResult));
        }
    }
}
