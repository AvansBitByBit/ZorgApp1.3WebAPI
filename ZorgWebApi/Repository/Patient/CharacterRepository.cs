using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ZorgWebApi.Models;

namespace ZorgWebApi.Repository
{
    public interface ICharacterRepository
    {
        Task CreateCharacterAsync(Character character);
        Task<IEnumerable<Character>> GetCharactersByUserIdAsync(string userId);
    }

    public class CharacterRepository : ICharacterRepository
    {
        private readonly IDbConnection _dbConnection;

        public CharacterRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task CreateCharacterAsync(Character character)
        {
            var sql = "INSERT INTO Characters (Name, Class, HairColor, SkinColor, EyeColor, Gender, UserId) VALUES (@Name, @Class, @HairColor, @SkinColor, @EyeColor, @Gender, @UserId)";
            await _dbConnection.ExecuteAsync(sql, character);
        }

        public async Task<IEnumerable<Character>> GetCharactersByUserIdAsync(string userId)
        {
            var sql = "SELECT * FROM Characters WHERE UserId = @UserId";
            return await _dbConnection.QueryAsync<Character>(sql, new { UserId = userId });
        }
    }
}