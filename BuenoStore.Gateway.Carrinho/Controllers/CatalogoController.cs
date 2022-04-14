using BuenoStore.BuildingBlocks.Api.Controllers;
using BuenoStore.Gateway.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BuenoStore.Gateway.Api.Controllers
{
    public class CatalogoController : BaseController
    {
        private readonly ICatalogoService _catalogoService;

        public CatalogoController(ICatalogoService catalogoService)
        {
            _catalogoService = catalogoService;
        }

        [HttpGet]
        [Route("compras/catalogo")]
        public async Task<IActionResult> ObterTodos() => CustomResponse(await _catalogoService.ObterItens());

    }
}
