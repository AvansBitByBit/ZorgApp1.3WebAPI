using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZorgWebApi.Models;
using ZorgWebApi.Interfaces;

[ApiController]
[Route("Charactercreator")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;
    private readonly IAuthenticationService _authenticationService;


    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterController"/> class.
    /// </summary>
    /// <param name="characterService">The character service instance.</param>
    public CharacterController(ICharacterService characterService, IAuthenticationService authenticationService)
    {
        _characterService = characterService;
        _authenticationService = authenticationService;
    }


    /// <summary>
    /// Gets the list of characters.
    /// </summary>
    /// <returns>A collection of <see cref="Character"/>.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Character>>> GetCharacters()
    {
        var userId = _authenticationService.GetCurrentAuthenticatedUserId();
        if (userId == null)
        {
            return Unauthorized();
        }
        var characters = await _characterService.GetCharactersAsync();
        return Ok(characters);
    }

    /// <summary>
    /// Creates a new character.
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
        character.UserId = userId;
        await _characterService.CreateCharacterAsync(character);
        return Ok(character);
    }

}
