using GunCatalog.Domain.Enumerable;
using GunCatalog.Domain.Model;
using GunCatalog.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GunCatalog.Repository
{
    public class GunRepository : IGunRepository
    {

        private static Dictionary<Guid, Gun> gunList = new Dictionary<Guid, Gun>()
        {
            {Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), new Gun{ id = Guid.Parse("0ca314a5-9282-45d8-92c3-2985f2a9fd04"), Modelo = "1911", Fabricante = "Taurus", Calibre = ECalibre.c380ETOG, Capacidade = 20, NumeroDeSerie = "12334321", Preco = 1300} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Gun{ id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Modelo = "Puma", Fabricante = "Rossi", Calibre = ECalibre.c45AUTO, Capacidade = 12, NumeroDeSerie = "12337654", Preco = 2000} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Gun{ id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Modelo = "G19", Fabricante = "Glock", Calibre = ECalibre.c9mm, Capacidade = 19, NumeroDeSerie = "12332121", Preco = 7000} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Gun{ id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Modelo = "G43", Fabricante = "Glock", Calibre = ECalibre.c40SeW, Capacidade = 16, NumeroDeSerie = "12336123", Preco = 10000} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Gun{ id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Modelo = "M16A1", Fabricante = "Colt", Calibre = ECalibre.c357MAGNUM, Capacidade = 30, NumeroDeSerie = "18764321", Preco = 40000} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Gun{ id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Modelo = "UZI", Fabricante = "IMI", Calibre = ECalibre.c9mm, Capacidade = 30, NumeroDeSerie = "21344321", Preco = 9000} }
        };


        public Task DeleteAsync(Guid id)
        {
            gunList.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            // close connection
        }

        public Task<Gun> GetAsync(Guid id)
        {
            if (!gunList.ContainsKey(id))
                return Task.FromResult<Gun>(null);

            return Task.FromResult(gunList[id]);
        }

        public Task<List<Gun>> GetAsync(string nome, string fabricante)
        {
            return Task.FromResult(gunList.Values.Where(gunList => gunList.Modelo.Equals(nome) && gunList.Fabricante.Equals(fabricante)).ToList());
        }

        public Task<List<Gun>> GetListAsync(int pagina, int quantity)
        {
            return Task.FromResult(gunList.Values.Skip((pagina - 1) * quantity).Take(quantity).ToList());
        }

        public Task InsertAsync(Gun gun)
        {
            gunList.Add(gun.id, gun);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Gun gun)
        {
            gunList[gun.id] = gun;
            return Task.CompletedTask;
        }
    }
}
