using GunCatalog.Domain.ImputModel;
using GunCatalog.Domain.ViewModel;
using GunCatalog.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunCatalog.Service
{
    public class GunService : IGunService
    {
        public Task DeleteAsync(Guid guid)
        {
            throw new NotImplementedException();
        }

        public Task<List<GunViewModel>> GetAsync(int page, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task<GunViewModel> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GunViewModel> InsertAsync(GunImputModel gun)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, GunImputModel gun)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid id, double preco)
        {
            throw new NotImplementedException();
        }
    }
}
