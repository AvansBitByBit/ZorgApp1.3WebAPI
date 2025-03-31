using System.Data;
using Dapper;
using ZorgWebApi.Interfaces;
using ZorgWebApi.Models;
namespace ZorgWebApi.Repository
{
    public class AfspraakRepository : IAfspraakRepository
    {
        private readonly IDbConnection _dbConnection;

        public AfspraakRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<AfspraakModel>> GetAfspraken(string userId)
        {
            var query = "SELECT * FROM Afspraken WHERE UserId = @UserId";
            return await _dbConnection.QueryAsync<AfspraakModel>(query, new { UserId = userId });
        }

        public async Task<AfspraakModel> GetAfspraakById(Guid id, string userId)
        {
            var query = "SELECT * FROM Afspraken WHERE ID = @ID AND UserId = @UserId";
            return await _dbConnection.QueryFirstOrDefaultAsync<AfspraakModel>(query, new { ID = id, UserId = userId });
        }

        public async Task CreateAfspraak(AfspraakModel afspraak)
        {
            var query = "INSERT INTO Afspraken (ID,Titel, NaamDokter, DatumTijd, UserId, Actief) VALUES (@ID,@Titel, @NaamDokter, @DatumTijd, @UserId, @Actief)";
            await _dbConnection.ExecuteAsync(query, afspraak);
        }

        public async Task DeleteAfspraak(Guid id, string userId)
        {
            var query = "DELETE FROM Afspraken WHERE ID = @ID AND UserId = @UserId";
            await _dbConnection.ExecuteAsync(query, new { ID = id, UserId = userId });
        }
    }
}
