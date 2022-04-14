using BuenoStore.Catalogo.Api.Models;
using BuenoStore.Catalogo.Api.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BuenoStore.Catalogo.Api.Controllers
{
    [ApiController]
    public class CatalogoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        public CatalogoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet("catalogo/produtos")]
        public async Task<IEnumerable<Produto>> ObterTodos() => await _produtoRepository.ObterTodos();

        [HttpGet("catalogo/produtos/{id}")]
        public async Task<Produto> ObterPorId(Guid id) => await _produtoRepository.ObterPorId(id);
    }
}
