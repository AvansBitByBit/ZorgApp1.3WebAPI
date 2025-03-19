using System.Collections.Generic;
using System.Threading.Tasks;
using ZorgWebApi.Models;

namespace ZorgWebApi.Interfaces
{
    public interface ICharacterService
    {
        Task CreateCharacterAsync(Character character);
        Task<IEnumerable<Character>> GetCharactersAsync();
    }
}