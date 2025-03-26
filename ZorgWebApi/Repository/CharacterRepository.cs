using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ZorgWebApi.Models;
using ZorgWebApi.Interfaces;

namespace ZorgWebApi.Repository
{
    /// <summary>
    /// Repository class for managing character data.
    /// </summary>
    public class CharacterRepository : ICharacterRepository
    {
        private readonly IDbConnection _dbConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterRepository"/> class.
        /// </summary>
        /// <param name="dbConnection">The database connection to be used by the repository.</param>
        public CharacterRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <summary>
        /// Creates a new character record in the database.
        /// </summary>
        /// <param name="character">The character model containing the data to be inserted.</param>
        /// <param name="userId">The user ID associated with the character.</param>
        public async Task CreateCharacterAsync(Character character, string userId)
        {
            var sql = "INSERT INTO Characters (HairColor, SkinColor, EyeColor, Gender, SpacesuitColor, Hat, UserId) VALUES (@HairColor, @SkinColor, @EyeColor, @Gender, @SpacesuitColor, @Hat, @UserId)";
            await _dbConnection.ExecuteAsync(sql, new
            {
                character.HairColor,
                character.SkinColor,
                character.EyeColor,
                character.Gender,
                character.SpacesuitColor,
                character.Hat,
                UserId = userId
            });
        }

        /// <summary>
        /// Retrieves all characters associated with a specific user ID from the database.
        /// </summary>
        /// <param name="userId">The user ID to filter characters by.</param>
        /// <returns>A collection of <see cref="Character"/>.</returns>
        public async Task<IEnumerable<Character>> GetCharactersByUserIdAsync(string userId)
        {
            var sql = "SELECT * FROM Characters WHERE UserId = @UserId";
            return await _dbConnection.QueryAsync<Character>(sql, new { UserId = userId });
        }
    }
}
