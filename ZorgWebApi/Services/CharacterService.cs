using System.Collections.Generic;
using System.Threading.Tasks;
using ZorgWebApi.Models;
using ZorgWebApi.Interfaces;

namespace ZorgWebApi.Services
{
    /// <summary>
    /// Service class for managing character data.
    /// </summary>
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IAuthenticationService _authenticationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterService"/> class.
        /// </summary>
        /// <param name="characterRepository">The character repository instance.</param>
        /// <param name="authenticationService">The authentication service instance.</param>
        public CharacterService(ICharacterRepository characterRepository, IAuthenticationService authenticationService)
        {
            _characterRepository = characterRepository;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Creates a new character asynchronously.
        /// </summary>
        /// <param name="character">The character model containing the data to be inserted.</param>
        public async Task CreateCharacterAsync(Character character)
        {
            // Get the UserId of the currently authenticated user
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            await _characterRepository.CreateCharacterAsync(character, userId);
        }

        /// <summary>
        /// Retrieves all characters associated with the authenticated user asynchronously.
        /// </summary>
        /// <returns>A collection of <see cref="Character"/>.</returns>
        public async Task<IEnumerable<Character>> GetCharactersAsync()
        {
            // Get the ID of the currently authenticated user
            var userId = _authenticationService.GetCurrentAuthenticatedUserId();
            return await _characterRepository.GetCharactersByUserIdAsync(userId);
        }
    }
}
