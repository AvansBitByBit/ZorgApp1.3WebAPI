using System.Data;
using System.Threading.Tasks;
using Dapper;
using ZorgWebApi.Models;
using ZorgWebApi.Interfaces;

namespace ZorgWebApi.Repository
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly IDbConnection _dbConnection;

        public CharacterRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task CreateCharacterAsync(Character character)
        {
            var sql = "INSERT INTO Characters (HairColor, SkinColor, EyeColor, Gender, SpacesuitColor, Hat, UserId) VALUES (@HairColor, @SkinColor, @EyeColor, @Gender, @SpacesuitColor, @Hat, @UserId)";
            await _dbConnection.ExecuteAsync(sql, character);
        }

        public async Task<IEnumerable<Character>> GetCharacterByUserIdAsync(string userId)
        {
            var sql = "SELECT * FROM Characters WHERE UserId = @UserId";
            return await _dbConnection.QueryAsync<Character>(sql, new { UserId = userId });
        }

        public async Task UpdateCharacterAsync(Character character)
        {
            var sql = "UPDATE Characters SET HairColor = @HairColor, SkinColor = @SkinColor, EyeColor = @EyeColor, Gender = @Gender, SpacesuitColor = @SpacesuitColor, Hat = @Hat WHERE UserId = @UserId";
            await _dbConnection.ExecuteAsync(sql, character);
        }
    }
}