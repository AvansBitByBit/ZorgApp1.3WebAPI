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

    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCharacter([FromBody] Character character)
    {
        await _characterService.CreateCharacterAsync(character);
        return Ok(character);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Character>>> GetCharacters()
    {
        var characters = await _characterService.GetCharactersAsync();
        return Ok(characters);
    }
}