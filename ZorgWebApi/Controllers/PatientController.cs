using Microsoft.AspNetCore.Mvc;
using ZorgWebApi.Interfaces;
using ZorgWebApi.Models;

namespace ZorgWebApi.Controllers;

[ApiController]
[Route("Patient")]
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
    public async Task <ActionResult<IEnumerable<PatientModel>>> Get()
    {
        try
        {
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            if(userId == null)
            {
                return Unauthorized();
            }
            var Patients = await _repository.GetPatients(userId);
            return Ok(Patients);
        }
        catch (Exception ex)
        {
            _logger.LogError("Fout met ophalen van patienten zie cmd");
            return StatusCode(500, "Internal Server Error");
        }
    }

}
