using System.Collections.Generic;
using System.Threading.Tasks;
using ZorgWebApi.Models;

namespace ZorgWebApi.Interfaces
{
    /// <summary>
    /// Interface for managing character data in the repository.
    /// </summary>
    public interface ICharacterRepository
    {
        /// <summary>
        /// Creates a new character asynchronously.
        /// </summary>
        /// <param name="character">The character model containing the data to be inserted.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task CreateCharacterAsync(Character character, string userId);

        /// <summary>
        /// Retrieves all characters associated with a specific user ID asynchronously.
        /// </summary>
        /// <param name="userId">The user ID to filter characters by.</param>
        /// <returns>A collection of <see cref="Character"/>.</returns>
        Task<IEnumerable<Character>> GetCharactersByUserIdAsync(string userId);
    }
}


