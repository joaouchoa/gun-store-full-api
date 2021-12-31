using GunCatalog.Domain.ImputModel;
using GunCatalog.Domain.Model;
using GunCatalog.Domain.ViewModel;
using GunCatalog.Repository.Interfaces;
using GunCatalog.Service.Exceptions;
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
        private readonly IGunRepository _gunRepository;

        public GunService(IGunRepository gunRepository) 
        { 
            _gunRepository = gunRepository;
        }
        public async Task DeleteAsync(Guid id)
        {
            var gunResult = _gunRepository.GetAsync(id);

            if (gunResult == null)
                throw new GunHasSavedException();

            await _gunRepository.DeleteAsync(id);
        }

        public async Task<List<GunViewModel>> GetAsync(int page, int quantity)
        {
            var guns = await _gunRepository.GetListAsync(page, quantity);
            return guns.Select(gun => new GunViewModel
            {
                id = gun.id,
                Modelo = gun.Modelo,
                Fabricante = gun.Fabricante,
                Calibre = gun.Calibre,
                Capacidade = gun.Capacidade,
                NumeroDeSerie = gun.NumeroDeSerie,
                Preco = gun.Preco
            }).ToList();
        }

        public async Task<GunViewModel> GetAsync(Guid id)
        {
            var gun = await _gunRepository.GetAsync(id);

            if(gun == null)
                return null;

            return new GunViewModel
            {
                id = gun.id,
                Modelo = gun.Modelo,
                Fabricante = gun.Fabricante,
                Calibre = gun.Calibre,
                Capacidade = gun.Capacidade,
                NumeroDeSerie = gun.NumeroDeSerie,
                Preco = gun.Preco
            };
        }

        public async Task<GunViewModel> InsertAsync(GunImputModel gun)
        {
            var result = await _gunRepository.GetAsync(gun.Modelo, gun.Fabricante);

            if (result.Count > 0) 
            {
                throw new GunHasNotSavedException();
            }

            var gunInsert = new Gun
            {
                Fabricante = gun.Fabricante,
                id = Guid.NewGuid(),
                Calibre = gun.Calibre,
                Capacidade = gun.Capacidade,
                Modelo = gun.Modelo,
                NumeroDeSerie = gun.NumeroDeSerie,
                Preco = gun.Preco
            };

            await _gunRepository.InsertAsync(gunInsert);

            return new GunViewModel
            {
                id = gunInsert.id,
                Modelo = gun.Modelo,
                Fabricante = gun.Fabricante,
                Calibre = gun.Calibre,
                Capacidade = gun.Capacidade,
                NumeroDeSerie = gun.NumeroDeSerie,
                Preco = gun.Preco
            };
        }

        public async Task UpdateAsync(Guid id, GunImputModel gun)
        {
            var gunResult = await _gunRepository.GetAsync(id);

            if(gunResult == null)
                throw new GunHasSavedException();

            gunResult.Modelo = gun.Modelo;
            gunResult.Fabricante = gun.Fabricante;
            gunResult.Calibre = gun.Calibre;
            gunResult.Capacidade = gun.Capacidade;
            gunResult.NumeroDeSerie = gun.NumeroDeSerie;
            gunResult.Preco = gun.Preco;

            await _gunRepository.UpdateAsync(gunResult);
        }

        public async Task UpdateAsync(Guid id, double preco)
        {
            var gunResul = await _gunRepository.GetAsync(id);

            if(gunResul == null)
                throw new GunHasSavedException();

            gunResul.Preco = preco;
            await _gunRepository.UpdateAsync(gunResul);
        }

        public void Dispose() 
        {
            _gunRepository.Dispose();
        }
    }
}
