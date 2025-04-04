﻿using System.Data;
using Dapper;
using ZorgWebApi.Interfaces;
using ZorgWebApi.Models;
namespace ZorgWebApi.Repository
{
    public class DagboekRepository : IDagboekRepository
    {
        private readonly IDbConnection _dbConnection;

        public DagboekRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<DagboekModel>> GetDagboeken(string userId)
        {
            var query = "SELECT * FROM Dagboeken WHERE UserId = @UserId";
            return await _dbConnection.QueryAsync<DagboekModel>(query, new { UserId = userId });
        }

        public async Task<DagboekModel> GetDagboekById(int id, string userId)
        {
            var query = "SELECT * FROM Dagboeken WHERE ID = @ID AND UserId = @UserId";
            return await _dbConnection.QueryFirstOrDefaultAsync<DagboekModel>(query, new { ID = id, UserId = userId });
        }

        public async Task CreateDagboek(DagboekModel dagboek)
        {
            var query = "INSERT INTO Dagboeken (ID,Title, Contents, UserId) VALUES (@ID,@Title, @Contents, @UserId)";
            await _dbConnection.ExecuteAsync(query, dagboek);
        }

        public async Task UpdateDagboek(DagboekModel dagboek)
        {
            var query = "UPDATE Dagboeken SET Title = @Title, Contents = @Contents, WHERE ID = @ID AND UserId = @UserId";
            await _dbConnection.ExecuteAsync(query, dagboek);
        }

        public async Task DeleteDagboek(int id, string userId)
        {
            var query = "DELETE FROM Dagboeken WHERE ID = @ID AND UserId = @UserId";
            await _dbConnection.ExecuteAsync(query, new { ID = id, UserId = userId });
        }
    }
}
