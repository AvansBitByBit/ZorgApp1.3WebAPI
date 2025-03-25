using ZorgWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZorgWebApi.Interfaces
{
    public interface IAfspraakRepository
    {
        Task<IEnumerable<AfspraakModel>> GetAfspraken(string userId);
        Task<AfspraakModel> GetAfspraakById(int id, string userId);
        Task CreateAfspraak(AfspraakModel afspraak);
        Task DeleteAfspraak(int id, string userId);
    }
}
