using Microsoft.AspNetCore.Mvc;
using ZorgWebApi.Interfaces;
using ZorgWebApi.Models;
using ZorgWebApi.Services;

namespace ZorgWebApi.Controllers
{
    [ApiController]
    [Route("Dagboek")]
    public class DagboekController : ControllerBase
    {
        private readonly ILogger<DagboekController> _logger;
        private readonly IDagboekRepository _repository;
        private readonly IAuthenticationService _authenticationService;

        public DagboekController(ILogger<DagboekController> logger, IAuthenticationService authenticationService, IDagboekRepository repository)
        {
            _repository = repository;
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpGet(Name = "GetDagboekEntries")]
        public async Task<ActionResult<IEnumerable<DagboekModel>>> Get()
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var entries = await _repository.GetDagboeken(userId);
            return Ok(entries);
        }

        [HttpPost(Name = "CreateDagboekEntry")]
        public async Task<ActionResult> Create([FromBody] DagboekModel dagboek)
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            dagboek.UserId = userId;
            await _repository.CreateDagboek(dagboek);
            return CreatedAtRoute("GetDagboekEntries", new { id = dagboek.ID }, dagboek);
        }

        [HttpPut("{id}", Name = "UpdateDagboekEntry")]
        public async Task<IActionResult> Update(int id, [FromBody] DagboekModel dagboek)
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            dagboek.ID = id;
            dagboek.UserId = userId;
            await _repository.UpdateDagboek(dagboek);
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteDagboekEntry")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var dagboek = new DagboekModel { ID = id, UserId = userId };
            await _repository.DeleteDagboek(id, userId);
            return NoContent();
        }
    }
}
