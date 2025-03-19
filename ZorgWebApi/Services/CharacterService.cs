using System.Collections.Generic;
using System.Threading.Tasks;
using ZorgWebApi.Models;
using ZorgWebApi.Repository;

public class CharacterService : ICharacterService
{
    private readonly ICharacterRepository _characterRepository;

    public CharacterService(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository;
    }

    public async Task CreateCharacterAsync(Character character)
    {
        await _characterRepository.CreateCharacterAsync(character);
    }

    public async Task<IEnumerable<Character>> GetCharactersAsync()
    {
        return await _characterRepository.GetCharactersAsync();
    }
}