using Microsoft.AspNetCore.Mvc;
using ZorgWebApi.Interfaces;
using ZorgWebApi.Models;

namespace ZorgWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private readonly ILogger<PatientController> _logger;
    private readonly IPatientRepository _repository;
    private readonly IAuthenticationService _authenticationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PatientController"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="authenticationService">The authentication service instance.</param>
    /// <param name="repository">The patient repository instance.</param>
    public PatientController(ILogger<PatientController> logger, IAuthenticationService authenticationService, IPatientRepository repository)
    {
        _repository = repository;
        _logger = logger;
        _authenticationService = authenticationService;
    }

    /// <summary>
    /// Gets the list of patients associated with the authenticated user.
    /// </summary>
    /// <returns>A collection of <see cref="PatientModel"/>.</returns>
    [HttpGet(Name = "GetPatient")]
    public async Task<ActionResult<IEnumerable<PatientModel>>> Get()
    {
        try
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            if (userId == null)
            {
                return Unauthorized();
            }
            var patients = await _repository.GetPatients(userId);
            return Ok(patients);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error fetching patients: {ex.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    /// <summary>
    /// Creates a new patient record.
    /// </summary>
    /// <param name="patient">The patient model containing the data to be inserted.</param>
    [HttpPost(Name = "CreatePatient")]
    public async Task<ActionResult> Create([FromBody] PatientModel patient)
    {
        try
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            if (userId == null)
            {
                return Unauthorized();
            }
            patient.UserId = userId;
            await _repository.CreatePatient(patient);
            return CreatedAtAction(nameof(Get), new { id = patient.ID }, patient);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error creating patient: {ex.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

  
    /// <summary>
    /// Deletes an existing patient record.
    /// </summary>
    /// <param name="id">The ID of the patient to be deleted.</param>
    [HttpDelete("{id}", Name = "DeletePatient")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            if (userId == null)
            {
                return Unauthorized();
            }
            var patient = new PatientModel { ID = id, UserId = userId };
            await _repository.DeletePatient(patient);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error deleting patient: {ex.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }
}
