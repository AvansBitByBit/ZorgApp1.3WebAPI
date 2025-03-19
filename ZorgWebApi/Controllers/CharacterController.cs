using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZorgWebApi.Models;
using ZorgWebApi.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CharacterController"/> class.
    /// </summary>
    /// <param name="characterService">The character service instance.</param>
    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    /// <summary>
    /// Creates a new character.
    /// </summary>
    /// <param name="character">The character model containing the data to be inserted.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the action.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateCharacter([FromBody] Character character)
    {
        await _characterService.CreateCharacterAsync(character);
        return Ok(character);
    }

    /// <summary>
    /// Gets the list of characters.
    /// </summary>
    /// <returns>A collection of <see cref="Character"/>.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Character>>> GetCharacters()
    {
        var characters = await _characterService.GetCharactersAsync();
        return Ok(characters);
    }
}
