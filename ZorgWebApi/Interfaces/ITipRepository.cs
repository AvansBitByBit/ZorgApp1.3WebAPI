using ZorgWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZorgWebApi.Interfaces
{
    public interface ITipRepository
    {
        Task<IEnumerable<TipModel>> GetTips();
        Task<TipModel> GetTipById(int id);
        Task CreateTip(TipModel tip);
        Task UpdateTip(TipModel tip);
        Task DeleteTip(int id);
    }
}
