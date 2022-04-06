using BuenoStore.BuildingBlocks.Data;
using BuenoStore.Catalogo.Api.Models;
using BuenoStore.Catalogo.Api.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BuenoStore.Catalogo.Api.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CatalogoContext _context;

        public ProdutoRepository(CatalogoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<IEnumerable<Produto>> ObterTodos() => await _context.Produtos.AsNoTracking().ToListAsync();

        public async Task<Produto> ObterPorId(Guid id) => await _context.Produtos.FindAsync(id);

        public void Adicionar(Produto produto) => _context.Produtos.Add(produto);

        public void Atualizar(Produto produto) => _context.Produtos.Update(produto);

        public void Dispose() => _context?.Dispose();
    }
}
