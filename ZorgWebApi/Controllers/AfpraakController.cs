using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ZorgWebApi.Interfaces;
using ZorgWebApi.Models;
using ZorgWebApi.Services;

namespace ZorgWebApi.Controllers
{
    /// <summary>
    /// Controller for managing afspraken (appointments).
    /// </summary>
    [ApiController]
    [Route("Afspraak")]
    public class AfspraakController : ControllerBase
    {
        private readonly ILogger<AfspraakController> _logger;
        private readonly IAfspraakRepository _repository;
        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AfspraakController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="authenticationService">The authentication service instance.</param>
        /// <param name="repository">The afspraak repository instance.</param>
        public AfspraakController(ILogger<AfspraakController> logger, IAuthenticationService authenticationService, IAfspraakRepository repository)
        {
            _repository = repository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Gets the list of afspraken for the authenticated user.
        /// </summary>
        /// <returns>A list of afspraken.</returns>
        /// <response code="200">Returns the list of afspraken</response>
        /// <response code="401">If the user is not authenticated</response>
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

        /// <summary>
        /// Creates a new afspraak.
        /// </summary>
        /// <param name="afspraak">The afspraak model.</param>
        /// <returns>A result indicating the outcome of the operation.</returns>
        /// <response code="200">If the afspraak is successfully created</response>
        /// <response code="400">If the request is invalid</response>
        /// <response code="401">If the user is not authenticated</response>
        /// <example>
        /// Example request body:
        /// {
        ///     "Titel": "Doktersafspraak",
        ///     "NaamDokter": "Dr. Smith",
        ///     "DatumTijd": "2023-10-15T14:30:00",
        ///     "Actief": 1
        /// }
        /// </example>
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



        /// <summary>
        /// Deletes an afspraak by ID.
        /// </summary>
        /// <param name="id">The ID of the afspraak to delete.</param>
        /// <returns>A result indicating the outcome of the operation.</returns>
        /// <response code="204">If the afspraak is successfully deleted</response>
        /// <response code="401">If the user is not authenticated</response>
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

