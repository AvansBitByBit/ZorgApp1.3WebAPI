using Microsoft.AspNetCore.Mvc;
using ZorgWebApi.Interfaces;
using ZorgWebApi.Models;
using ZorgWebApi.Services;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ZorgWebApi.Controllers
{
    [ApiController]
    [Route("Afspraak")]
    public class AfspraakController : ControllerBase
    {
        private readonly ILogger<AfspraakController> _logger;
        private readonly IAfspraakRepository _repository;
        private readonly IAuthenticationService _authenticationService;

        public AfspraakController(ILogger<AfspraakController> logger, IAuthenticationService authenticationService, IAfspraakRepository repository)
        {
            _repository = repository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpGet(Name = "GetAfspraken")]
        public async Task<ActionResult<IEnumerable<AfspraakModel>>> Get()
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            if (userId == null)
            {
                _logger.LogWarning("Unauthorized access attempt to GetAfspraken.");
                return Unauthorized();
            }

            _logger.LogInformation("Fetching afspraken for user {UserId}", userId);
            var afspraken = await _repository.GetAfspraken(userId);
            return Ok(afspraken);
        }

        [HttpGet("{id}", Name = "GetAfspraakById")]
        public async Task<ActionResult<AfspraakModel>> GetAfspraakById(Guid id)
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            if (userId == null)
            {
                _logger.LogWarning("Unauthorized access attempt to GetAfspraakById.");
                return Unauthorized();
            }

            var afspraak = await _repository.GetAfspraakById(id, userId);
            if (afspraak == null || afspraak.UserId != userId)
            {
                _logger.LogWarning("User {UserId} attempted to access afspraak {AfspraakId} which does not belong to them.", userId, id);
                return Unauthorized();
            }

            return Ok(afspraak);
        }

        [HttpPost(Name = "CreateAfspraak")]
        public async Task<ActionResult> Add([FromBody] AfspraakModel afspraak)
        {
            var currentUser = _authenticationService.GetCurrentAuthenticatedUserId();
            if (currentUser == null)
            {
                _logger.LogWarning("Unauthorized access attempt to CreateAfspraak.");
                return Unauthorized();
            }

            // Set the UserId explicitly
            afspraak.UserId = currentUser;

            if (string.IsNullOrEmpty(afspraak.Titel) || string.IsNullOrEmpty(afspraak.NaamDokter) || string.IsNullOrEmpty(afspraak.DatumTijd))
            {
                _logger.LogWarning("Invalid request data: {Afspraak}", afspraak);
                return BadRequest("Titel, NaamDokter, and DatumTijd are required.");
            }

            _logger.LogInformation("Checking for existing afspraken for user {UserId}", currentUser);
            var afspraken = await _repository.GetAfspraken(currentUser);
            if (afspraken.Any(a => a.Titel == afspraak.Titel))
            {
                _logger.LogWarning("Afspraak with title {Titel} already exists for user {UserId}", afspraak.Titel, currentUser);
                return BadRequest("Deze Titel bestaat al!!");
            }

            if (afspraken.Count() >= 9)
            {
                _logger.LogWarning("User {UserId} has reached the maximum number of afspraken.", currentUser);
                return BadRequest("Je kan niet meer dan 9 afspraken maken!!");
            }

            afspraak.ID = Guid.NewGuid();
            _logger.LogInformation("Creating new afspraak: {Afspraak}", afspraak);
            await _repository.CreateAfspraak(afspraak);
            return Ok("Afspraak is toegevoegd");
        }

        [HttpDelete("{id}", Name = "DeleteAfspraak")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            if (userId == null)
            {
                _logger.LogWarning("Unauthorized access attempt to DeleteAfspraak.");
                return Unauthorized();
            }

            _logger.LogInformation("Deleting afspraak with ID {AfspraakId} for user {UserId}", id, userId);
            await _repository.DeleteAfspraak(id, userId);
            return NoContent();
        }
    }
}
