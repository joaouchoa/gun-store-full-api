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

        /// <summary>
        /// Buscar todos as armas de forma paginada
        /// </summary>
        /// <remarks>
        /// Não é possível retornar as armas sem paginação
        /// </remarks>
        /// <param name="pagina">Indica qual página está sendo consultada. Mínimo 1</param>
        /// <param name="quantity">Indica a quantidade de reistros por página. Mínimo 1 e máximo 50</param>
        /// <response code="200">Retorna a lista de armas</response>
        /// <response code="204">Caso não haja arnas</response> 
        [HttpGet]
        public async Task<ActionResult<List<GunViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1,50)] int quantity = 5)
        {
            var resultList = await _gunService.GetAsync(pagina, quantity);

            if (resultList.Count() == 0)
                return NoContent();

            return Ok(resultList);
        }

        /// <summary>
        /// Buscar uma arma pelo seu Id
        /// </summary>
        /// <param name="idJogo">Id da arma buscado</param>
        /// <response code="200">Retorna a arma filtrado</response>
        /// <response code="204">Caso não haja arma com este id</response>   
        [HttpGet("{idGun:guid}")]
        public async Task<ActionResult> GetGunAsync([FromRoute] Guid IdGun)
        {
            var result = await _gunService.GetAsync(IdGun);

            if(result == null)
                return NoContent();

            return Ok(result);
        }

        /// <summary>
        /// Inserir uma arma no catálogo
        /// </summary>
        /// <param name="jogoInputModel">Dados da arma a ser inserido</param>
        /// <response code="200">Caso a arma seja inserido com sucesso</response>  
        [HttpPost]
        public async Task<ActionResult<GunViewModel>> InsertGunAsync([FromBody] GunImputModel gun)
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

        /// <summary>
        /// Atualizar uma Arma no catálogo
        /// </summary>
        /// /// <param name="idJogo">Id da arma a ser atualizado</param>
        /// <param name="jogoInputModel">Novos dados para atualizar o jogo indicado</param>
        /// <response code="200">Cao a arma seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista uma arma com este Id</response>  
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

        /// <summary>
        /// Atualizar o preço de uma arma
        /// </summary>
        /// /// <param name="idJogo">Id da arma a ser atualizado</param>
        /// <param name="preco">Novo preço do jogo</param>
        /// <response code="200">Cao a arma seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista uma arma com este Id</response> 
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

        /// <summary>
        /// Excluir uma arma
        /// </summary>
        /// /// <param name="idJogo">Id da arma a ser excluído</param>
        /// <response code="200">Cao o preço seja atualizado com sucesso</response>
        /// <response code="404">Caso não exista uma arma com este Id</response>  
        [HttpDelete("{idGun:guid}")]
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
