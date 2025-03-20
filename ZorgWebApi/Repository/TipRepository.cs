using System.Data;
using Dapper;
using ZorgWebApi.Interfaces;
using ZorgWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZorgWebApi.Repository
{
    public class TipRepository : ITipRepository
    {
        private readonly IDbConnection _dbConnection;

        public TipRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<TipModel>> GetTips()
        {
            var query = "SELECT * FROM Tips";
            return await _dbConnection.QueryAsync<TipModel>(query);
        }

        public async Task<TipModel> GetTipById(int id)
        {
            var query = "SELECT * FROM Tips WHERE ID = @ID";
            return await _dbConnection.QueryFirstOrDefaultAsync<TipModel>(query, new { ID = id });
        }

        public async Task CreateTip(TipModel tip)
        {
            var query = "INSERT INTO Tips (Title, Description) VALUES (@Title, @Description)";
            await _dbConnection.ExecuteAsync(query, tip);
        }

        public async Task UpdateTip(TipModel tip)
        {
            var query = "UPDATE Tips SET Title = @Title, Description = @Description WHERE ID = @ID";
            await _dbConnection.ExecuteAsync(query, tip);
        }

        public async Task DeleteTip(int id)
        {
            var query = "DELETE FROM Tips WHERE ID = @ID";
            await _dbConnection.ExecuteAsync(query, new { ID = id });
        }
    }
}
