using System.Data;
using Dapper;
using ZorgWebApi.Interfaces;
using ZorgWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZorgWebApi.Repository
{
    /// <summary>
    /// Repository class for managing tip data.
    /// </summary>
    public class TipRepository : ITipRepository
    {
        private readonly IDbConnection _dbConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="TipRepository"/> class.
        /// </summary>
        /// <param name="dbConnection">The database connection to be used by the repository.</param>
        public TipRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <summary>
        /// Retrieves all tips from the database.
        /// </summary>
        /// <returns>A collection of <see cref="TipModel"/>.</returns>
        public async Task<IEnumerable<TipModel>> GetTips()
        {
            var query = "SELECT * FROM Tips";
            return await _dbConnection.QueryAsync<TipModel>(query);
        }

        /// <summary>
        /// Retrieves a tip by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the tip to retrieve.</param>
        /// <returns>The <see cref="TipModel"/> with the specified ID.</returns>
        public async Task<TipModel> GetTipById(int id)
        {
            var query = "SELECT * FROM Tips WHERE ID = @ID";
            return await _dbConnection.QueryFirstOrDefaultAsync<TipModel>(query, new { ID = id });
        }

        /// <summary>
        /// Creates a new tip record in the database.
        /// </summary>
        /// <param name="tip">The tip model containing the data to be inserted.</param>
        public async Task CreateTip(TipModel tip)
        {
            var query = "INSERT INTO Tips (Title, Description) VALUES (@Title, @Description)";
            await _dbConnection.ExecuteAsync(query, tip);
        }

        /// <summary>
        /// Updates an existing tip record in the database.
        /// </summary>
        /// <param name="tip">The tip model containing the updated data.</param>
        public async Task UpdateTip(TipModel tip)
        {
            var query = "UPDATE Tips SET Title = @Title, Description = @Description WHERE ID = @ID";
            await _dbConnection.ExecuteAsync(query, tip);
        }

        /// <summary>
        /// Deletes a tip record from the database.
        /// </summary>
        /// <param name="id">The ID of the tip to delete.</param>
        public async Task DeleteTip(int id)
        {
            var query = "DELETE FROM Tips WHERE ID = @ID";
            await _dbConnection.ExecuteAsync(query, new { ID = id });
        }
    }
}
