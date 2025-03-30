using ZorgWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZorgWebApi.Interfaces
{
    public interface IAfspraakRepository
    {
        Task<IEnumerable<AfspraakModel>> GetAfspraken(string userId);
        Task<AfspraakModel> GetAfspraakById(Guid id, string userId);
        Task CreateAfspraak(AfspraakModel afspraak);
        Task DeleteAfspraak(Guid id, string userId);
    }
}
