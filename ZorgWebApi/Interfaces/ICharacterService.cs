using System.Collections.Generic;
using System.Threading.Tasks;
using ZorgWebApi.Models;

namespace ZorgWebApi.Interfaces
{
    /// <summary>
    /// Interface for managing character data.
    /// </summary>
    public interface ICharacterService
    {
        /// <summary>
        /// Creates a new character asynchronously.
        /// </summary>
        /// <param name="character">The character model containing the data to be inserted.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
       public Task CreateCharacterAsync(Character character);

        /// <summary>
        /// Retrieves all characters asynchronously.
        /// </summary>
        /// <returns>A collection of <see cref="Character"/>.</returns>
       public Task<Character> GetCharacterByUserIdAsync(string userId);


      public Task UpdateCharacterAsync(Character character);
    }
}

