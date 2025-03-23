using ZorgWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZorgWebApi.Interfaces
{
    public interface IDagboekRepository
    {
        Task<IEnumerable<DagboekModel>> GetDagboeken(string userId);
        Task<DagboekModel> GetDagboekById(int id, string userId);
        Task CreateDagboek(DagboekModel dagboek);
        Task UpdateDagboek(DagboekModel dagboek);
        Task DeleteDagboek(int id, string userId);
    }
}
