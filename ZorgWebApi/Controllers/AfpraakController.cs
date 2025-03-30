using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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
        public async Task<ActionResult> Add([FromBody] AfspraakModel afspraak)
        {
            var CurrentUser = _authenticationService.GetCurrentAuthenticatedUserId();
            var Afspraken = await _repository.GetAfspraken(CurrentUser);
            bool Afspraakexists = false;
            foreach (AfspraakModel Test in Afspraken)
            {
                if (Test.Titel == afspraak.Titel)
                {
                    Afspraakexists = true;
                    return BadRequest("Deze Titel bestaat al!!");
                }
                afspraak.UserId = CurrentUser;
            }
            if(Afspraken.Count() >= 9)
            {
                return BadRequest("Je kan niet meer dan 9 afspraken maken!!");
            }
            else
            {
                afspraak.ID = Guid.NewGuid();
                await _repository.CreateAfspraak(afspraak);
                return Ok("Afspraak is toegevoegd");
            }
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
