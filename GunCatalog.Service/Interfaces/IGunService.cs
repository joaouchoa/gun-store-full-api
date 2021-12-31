using GunCatalog.Domain.ImputModel;
using GunCatalog.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunCatalog.Service.Interfaces
{
    public interface IGunService
    {
        Task<List<GunViewModel>> GetAsync(int page, int quantity);
        Task<GunViewModel> GetAsync(Guid id);
        Task<GunViewModel> InsertAsync(GunImputModel gun);
        Task UpdateAsync(Guid id, GunImputModel gun);
        Task UpdateAsync(Guid id, double preco);
        Task DeleteAsync(Guid guid);
    }
}
