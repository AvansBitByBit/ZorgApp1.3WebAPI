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
        [HttpGet(Name = "GetAfspraken")]
        public async Task<ActionResult<IEnumerable<AfspraakModel>>> Get()
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var afspraken = await _repository.GetAfspraken(userId);
            return Ok(afspraken);
        }

        /// <summary>
        /// Creates a new afspraak.
        /// </summary>
        /// <param name="afspraak">The afspraak model.</param>
        /// <returns>A result indicating the outcome of the operation.</returns>
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
                return Unauthorized();
            }

            if (string.IsNullOrEmpty(afspraak.Titel) || string.IsNullOrEmpty(afspraak.NaamDokter) || string.IsNullOrEmpty(afspraak.DatumTijd))
            {
                return BadRequest("Titel, NaamDokter, and DatumTijd are required.");
            }

            var afspraken = await _repository.GetAfspraken(currentUser);
            bool afspraakExists = false;
            foreach (AfspraakModel test in afspraken)
            {
                if (test.Titel == afspraak.Titel)
                {
                    afspraakExists = true;
                    return BadRequest("Deze Titel bestaat al!!");
                }
            }
            if (afspraken.Count() >= 9)
            {
                return BadRequest("Je kan niet meer dan 9 afspraken maken!!");
            }
            else
            {
                afspraak.ID = Guid.NewGuid();
                afspraak.UserId = currentUser;
                await _repository.CreateAfspraak(afspraak);
                return Ok("Afspraak is toegevoegd");
            }
        }

        /// <summary>
        /// Deletes an afspraak by ID.
        /// </summary>
        /// <param name="id">The ID of the afspraak to delete.</param>
        /// <returns>A result indicating the outcome of the operation.</returns>
        [HttpDelete("{id}", Name = "DeleteAfspraak")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            await _repository.DeleteAfspraak(id, userId);
            return NoContent();
        }
    }
}
