using System.Collections.Generic;
using System.Threading.Tasks;
using ZorgWebApi.Models;
using ZorgWebApi.Repository;

public class CharacterService : ICharacterService
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IAuthenticationService _authenticationService;

    public CharacterService(ICharacterRepository characterRepository, IAuthenticationService authenticationService)
    {
        _characterRepository = characterRepository;
        _authenticationService = authenticationService;
    }

    public async Task CreateCharacterAsync(Character character)
    {
        character.UserId = _authenticationService.GetCurrentAuthenticatedUserId();
        await _characterRepository.CreateCharacterAsync(character);
    }

    public async Task<IEnumerable<Character>> GetCharactersAsync()
    {
        var userId = _authenticationService.GetCurrentAuthenticatedUserId();
        return await _characterRepository.GetCharactersByUserIdAsync(userId);
    }
}