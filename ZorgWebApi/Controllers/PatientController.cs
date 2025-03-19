using Microsoft.AspNetCore.Mvc;
using ZorgWebApi.Models;

namespace ZorgWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private readonly ILogger<PatientController> _logger;
    private readonly IAuthenticationService _authenticationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PatientController"/> class.
    /// </summary>
    /// <param name="logger">The logger instance.</param>
    /// <param name="authenticationService">The authentication service instance.</param>
    public PatientController(ILogger<PatientController> logger, IAuthenticationService authenticationService)
    {
        _logger = logger;
        _authenticationService = authenticationService;
    }

    /// <summary>
    /// Gets the list of patients associated with the authenticated user.
    /// </summary>
    /// <returns>A collection of <see cref="PatientModel"/>.</returns>
    [HttpGet(Name = "GetPatient")]
    public IEnumerable<PatientModel> Get()
    {
        // Get the ID of the currently authenticated user
        var userId = _authenticationService.GetCurrentAuthenticatedUserId();
        _logger.LogInformation($"User ID: {userId}");

        // Simulate fetching patient records from a data source
        var allPatients = Enumerable.Range(1, 5).Select(index => new PatientModel
        {
            ID = index,
            Voornaam = "Voornaam",
            Achternaam = "Achternaam",
            OuderVoogd_ID = index,
            TrajectID = index,
            ArtsID = index,
            UserId = userId // Assign the user ID to each patient record
        }).ToArray();

        // Filter patients by the authenticated user's ID
        var userPatients = allPatients.Where(p => p.UserId == userId);

        return userPatients;
    }
}