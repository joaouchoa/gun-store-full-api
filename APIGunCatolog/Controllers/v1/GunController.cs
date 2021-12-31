using GunCatalog.Domain.ImputModel;
using GunCatalog.Domain.ViewModel;
using GunCatalog.Service.Exceptions;
using GunCatalog.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIGunCatolog.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GunController : ControllerBase
    {
        private readonly IGunService _gunService;

        public GunController(IGunService gunService) 
        { 
            _gunService = gunService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GunViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1,50)] int quantity = 5)
        {
            var resultList = await _gunService.GetAsync(pagina, quantity);

            if (resultList.Count() == 0)
                return NoContent();

            return Ok(resultList);
        }

        [HttpGet("{idGun:guid}")]
        public async Task<ActionResult<GunViewModel>> GetGunAsync([FromRoute] Guid IdGun)
        {
            var result = await _gunService.GetAsync(IdGun);

            if(result == null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<GunImputModel>> InsertGunAsync([FromBody] GunImputModel gun)
        {
            try
            {
                var result = await _gunService.InsertAsync(gun);
                return Ok(result);
            }
            catch(GunHasSavedException ex)
            {
                return UnprocessableEntity("Esse Armamento já esta cadastrado estoque");
            }
        }

        [HttpPut("{idGun:guid}")]
        public async Task<ActionResult> UpdateGunAsync([FromRoute]Guid idGun,[FromBody] GunImputModel gun)
        {
            try
            {
                await _gunService.UpdateAsync(idGun, gun);
                return Ok();
            }
            catch (GunHasNotSavedException ex)
            {
                return UnprocessableEntity("Este armamento não consta no Catalogo");
            }
        }

        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> PatchGunAsync([FromRoute] Guid idGun, [FromRoute] double preco)
        {
            try
            {
                await _gunService.UpdateAsync(idGun, preco);
                return Ok();
            }
            catch (GunHasNotSavedException ex)
            {
                return UnprocessableEntity("Este armamento não consta no Catalogo");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteGunAsync([FromRoute] Guid idGun) 
        {
            try
            {
                await _gunService.DeleteAsync(idGun);
                return Ok();
            }
            catch (GunHasNotSavedException ex)
            {
                return UnprocessableEntity("Este armamento não consta no Catalogo");
            }
        }
    }
}
