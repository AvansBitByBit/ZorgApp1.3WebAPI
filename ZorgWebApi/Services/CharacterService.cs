using System.Threading.Tasks;
using ZorgWebApi.Models;
using ZorgWebApi.Interfaces;

namespace ZorgWebApi.Services
{
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

        public async Task<Character> GetCharacterByUserIdAsync(string userId)
        {
            var characters = await _characterRepository.GetCharacterByUserIdAsync(userId);
            return characters.FirstOrDefault();
        }

        public async Task UpdateCharacterAsync(Character character)
        {
            await _characterRepository.UpdateCharacterAsync(character);
        }
    }
}