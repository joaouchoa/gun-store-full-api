using GunCatalog.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunCatalog.Repository.Interfaces
{
    public interface IGunRepository : IDisposable
    {
        Task<List<Gun>> GetListAsync(int pagina, int quantity);
        Task<Gun> GetAsync(Guid id);
        Task<List<Gun>> GetAsync(string nome, string produtora);
        Task InsertAsync(Gun gun);
        Task UpdateAsync(Gun gun);
        Task DeleteAsync(Guid id);
    }
}
