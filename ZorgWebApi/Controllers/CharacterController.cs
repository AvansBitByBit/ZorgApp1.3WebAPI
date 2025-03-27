using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZorgWebApi.Models;
using ZorgWebApi.Interfaces;

[ApiController]
[Route("Charactercreator")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;
    private readonly IAuthenticationService _authenticationService;

    public CharacterController(ICharacterService characterService, IAuthenticationService authenticationService)
    {
        _characterService = characterService;
        _authenticationService = authenticationService;
    }

    /// <summary>
    /// Gets the character for the authenticated user.
    /// </summary>
    /// <returns>The character of the authenticated user.</returns>
    [HttpGet]
    public async Task<ActionResult<Character>> GetCharacter()
    {
        var userId = _authenticationService.GetCurrentAuthenticatedUserId();
        if (userId == null)
        {
            return Unauthorized();
        }

        var character = await _characterService.GetCharacterByUserIdAsync(userId);
        if (character == null)
        {
            return NotFound("No character found for this user.");
        }

        return Ok(character);
    }

    /// <summary>
    /// Creates a new character for the authenticated user.
    /// </summary>
    /// <param name="character">The character model containing the data to be inserted.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateCharacter([FromBody] Character character)
    {
        var userId = _authenticationService.GetCurrentAuthenticatedUserId();
        if (userId == null)
        {
            return Unauthorized();
        }

        var existingCharacter = await _characterService.GetCharacterByUserIdAsync(userId);

        character.UserId = userId;
        await _characterService.CreateCharacterAsync(character);
        return Ok(character);
    }

    /// <summary>
    /// Updates the character for the authenticated user.
    /// </summary>
    /// <param name="character">The updated character model.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
    [HttpPut]
    public async Task<IActionResult> UpdateCharacter([FromBody] Character character)
    {
        var userId = _authenticationService.GetCurrentAuthenticatedUserId();
        if (userId == null)
        {
            return Unauthorized();
        }

        var existingCharacter = await _characterService.GetCharacterByUserIdAsync(userId);
        if (existingCharacter == null)
        {
            return NotFound("No character found to update.");
        }

        character.UserId = userId; // Ensure the user ID is not changed
        await _characterService.UpdateCharacterAsync(character);
        return Ok(character);
    }
}