using AssinaturaDigital.Exceptions;
using AssinaturaDigital.Model;
using AssinaturaDigital.Services;
using AssinaturaDigital.View;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssinaturaDigital.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class AssinaturasController : ControllerBase
    {
        private readonly IAssinaturaService _assinaturaService;

        public AssinaturasController(IAssinaturaService assinaturaService)
        {
            _assinaturaService = assinaturaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssinaturaViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var assinaturas = await _assinaturaService.Obter(pagina, quantidade);

            if (assinaturas.Count() == 0)
                return NoContent();

            return Ok(assinaturas);
        }

        [HttpGet("{idAssinatura:guid}")]
        public async Task<ActionResult<IEnumerable<AssinaturaViewModel>>> Obter([FromRoute]Guid idAssinatura)
        {
            var assinatura = await _assinaturaService.Obter(idAssinatura);

            if (assinatura == null)
                return NoContent();

            return Ok(assinatura);
        }

        [HttpPost]
        public async Task<ActionResult<AssinaturaViewModel>> InserirAssinatura([FromBody]AssinaturaInputModel assinaturaInputModel)
        {
            try
            {
                var assinatura = await _assinaturaService.Inserir(assinaturaInputModel);

                return Ok(assinatura);
            }
            catch (AssinaturaJaCadastradaException ex)
            {
                return UnprocessableEntity("Já existe uma assinatura com este nome de Usuario");
            }
        }

        [HttpPut("{idAssinatura:guid}")]
        public async Task<ActionResult> AtualizarAssinatura([FromRoute] Guid idAssinatura, AssinaturaInputModel assinaturaInputModel)
        {
            try
            {
                await _assinaturaService.Atualizar(idAssinatura, assinaturaInputModel);
                return Ok();
            }
            catch (AssinaturaNaoCadastradaException ex)
            {
                return NotFound("Não existe essa assinatura");
            }
        }

        [HttpPatch("{idAssinatura:guid}/url/{url}")]
        public async Task<ActionResult> AtualizarAssinatura([FromRoute] Guid idAssinatura,[FromRoute] string url)
        {
            try
            {
                await _assinaturaService.Atualizar(idAssinatura, url);
                return Ok();
            }
            catch (AssinaturaNaoCadastradaException ex)
            {
                return NotFound("Essa assinatura não existe");
            }
        }

        [HttpDelete("{idAssinatura:Guid}")]
        public async Task<ActionResult> ApagarAssinatura([FromRoute] Guid idAssinatura)
        {
            try
            {
                await _assinaturaService.Remover(idAssinatura);
                return Ok();
            }
            catch(AssinaturaNaoCadastradaException ex)
            {
                return NotFound("Essa assinatura não existe");
            }
        }
    }
}
