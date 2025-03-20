using Microsoft.AspNetCore.Mvc;
using ZorgWebApi.Interfaces;
using ZorgWebApi.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZorgWebApi.Controllers
{
    [ApiController]
    [Route("tips")]
    public class TipController : ControllerBase
    {
        private readonly ILogger<TipController> _logger;
        private readonly ITipRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TipController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="repository">The tip repository instance.</param>
        public TipController(ILogger<TipController> logger, ITipRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Gets the list of all tips.
        /// </summary>
        /// <returns>A collection of <see cref="TipModel"/>.</returns>
        [HttpGet(Name = "GetTips")]
        public async Task<ActionResult<IEnumerable<TipModel>>> Get()
        {
            try
            {
                var tips = await _repository.GetTips();
                return Ok(tips);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching tips: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Gets a tip by its ID.
        /// </summary>
        /// <param name="id">The ID of the tip to retrieve.</param>
        /// <returns>The <see cref="TipModel"/> with the specified ID.</returns>
        [HttpGet("{id}", Name = "GetTipById")]
        public async Task<ActionResult<TipModel>> Get(int id)
        {
            try
            {
                var tip = await _repository.GetTipById(id);
                if (tip == null)
                {
                    return NotFound();
                }
                return Ok(tip);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching tip with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Creates a new tip.
        /// </summary>
        /// <param name="tip">The tip model containing the data to be inserted.</param>
        [HttpPost(Name = "CreateTip")]
        public async Task<ActionResult> Create([FromBody] TipModel tip)
        {
            try
            {
                await _repository.CreateTip(tip);
                return CreatedAtAction(nameof(Get), new { id = tip.Id }, tip);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating tip: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Updates an existing tip.
        /// </summary>
        /// <param name="id">The ID of the tip to be updated.</param>
        /// <param name="tip">The tip model containing the updated data.</param>
        [HttpPut("{id}", Name = "UpdateTip")]
        public async Task<IActionResult> Update(int id, [FromBody] TipModel tip)
        {
            try
            {
                if (id != tip.Id)
                {
                    return BadRequest("The ID of the tip did not match the ID of the route");
                }
                await _repository.UpdateTip(tip);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating tip with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        /// <summary>
        /// Deletes a tip by its ID.
        /// </summary>
        /// <param name="id">The ID of the tip to be deleted.</param>
        [HttpDelete("{id}", Name = "DeleteTip")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _repository.DeleteTip(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting tip with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}

