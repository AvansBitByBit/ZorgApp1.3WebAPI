using Microsoft.AspNetCore.Mvc;
using ZorgWebApi.Models;

namespace ZorgWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientController : ControllerBase
{
    private readonly ILogger<PatientController> _logger;
    private readonly IAuthenticationService _authenticationService;

    public PatientController(ILogger<PatientController> logger, IAuthenticationService authenticationService)
    {
        _logger = logger;
        _authenticationService = authenticationService;
    }

    [HttpGet(Name = "GetPatient")]
    public IEnumerable<PatientModel> Get()
    {
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