using Microsoft.AspNetCore.Mvc;
using ZorgWebApi.Interfaces;
using ZorgWebApi.Models;
using ZorgWebApi.Services;

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
                return Unauthorized();
            }

            var afspraken = await _repository.GetAfspraken(userId);
            return Ok(afspraken);
        }

        [HttpPost(Name = "CreateAfspraak")]
        public async Task<ActionResult> Create([FromBody] AfspraakModel afspraak)
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            afspraak.UserId = userId;
            await _repository.CreateAfspraak(afspraak);
            return CreatedAtRoute("GetAfspraken", new { id = afspraak.ID }, afspraak);
        }

        [HttpDelete("{id}", Name = "DeleteAfspraak")]
        public async Task<IActionResult> Delete(int id)
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
