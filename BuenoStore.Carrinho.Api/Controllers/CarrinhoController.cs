using BuenoStore.BuildingBlocks.Api.Controllers;
using BuenoStore.BuildingBlocks.Api.Usuario.Interface;
using BuenoStore.Carrinho.Api.Data;
using BuenoStore.Carrinho.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuenoStore.Carrinho.Api.Controllers
{
    [Authorize]
    public class CarrinhoController : BaseController
    {
        private readonly IAspNetUser _user;
        private readonly CarrinhoContext _context;

        public CarrinhoController(IAspNetUser user, CarrinhoContext context)
        {
            _user = user;
            _context = context;
        }

        [HttpGet("carrinho")]
        public async Task<Models.Carrinho> ObterCarrinho()
        {
            var carrinho = await ObterCarrinhoUsuario();
            
            if (carrinho is null)
                return new Models.Carrinho();

            return carrinho;
        }

        [HttpPost("carrinho")]
        public async Task<IActionResult> AdicionarItemCarrinho(CarrinhoItem carrinhoItem)
        {
            var carrinho = await ObterCarrinhoUsuario();

            if (carrinho is null)
                NovoCarrinho(carrinhoItem);
            else
                CarrinhoExistente(carrinho, carrinhoItem);

            if (!OperacaoEhValida())
                return CustomResponse();

            await SalvarCarrinho();
            return CustomResponse();
        }

        [HttpPut("carrinho/{produtoId}")]
        public async Task<IActionResult> AtualizarItemCarrinho(Guid produtoId, CarrinhoItem item)
        {
            var carrinho = await ObterCarrinhoUsuario();
            
            var itemCarrinho = await ObterItemCarrinhoValidado(produtoId, carrinho, item);
            if (itemCarrinho is null) return CustomResponse();

            carrinho.AtualizarUnidades(itemCarrinho, item.Quantidade);

            ValidarCarrinho(carrinho);
            if (!OperacaoEhValida()) return CustomResponse();

            _context.CarrinhoItens.Update(itemCarrinho);
            _context.Carrinho.Update(carrinho);

            await SalvarCarrinho();
            return CustomResponse();
        }

        [HttpDelete("carrinho/{produtoId}")]
        public async Task<IActionResult> RemoverItemCarrinho(Guid produtoId)
        {
            var carrinho = await ObterCarrinhoUsuario();

            var itemCarrinho = await ObterItemCarrinhoValidado(produtoId, carrinho);
            if (itemCarrinho == null) return CustomResponse();

            ValidarCarrinho(carrinho);
            if (!OperacaoEhValida()) return CustomResponse();

            carrinho.RemoverItem(itemCarrinho);

            _context.CarrinhoItens.Remove(itemCarrinho);
            _context.Carrinho.Update(carrinho);

            await SalvarCarrinho();
            return CustomResponse();
        }

        private async Task<Models.Carrinho> ObterCarrinhoUsuario()
        {
            return await _context.Carrinho
                .Include(c => c.Itens)
                .FirstOrDefaultAsync(c => c.ClienteId == _user.ObterUsuarioId());
        }

        private void NovoCarrinho(CarrinhoItem item)
        {
            var usuarioId = _user.ObterUsuarioId();

            var carrinho = new Models.Carrinho(usuarioId);
            carrinho.AdicionarItemCarrinho(item);

            ValidarCarrinho(carrinho);
            _context.Carrinho.Add(carrinho);
        }

        private void CarrinhoExistente(Models.Carrinho carrinho, CarrinhoItem item)
        {
            var produtoItemExistente = carrinho.ItemExistenteNoCarrinho(item);

            carrinho.AdicionarItemCarrinho(item);
            ValidarCarrinho(carrinho);

            if (produtoItemExistente)
                _context.CarrinhoItens.Update(carrinho.ObterPorProdutoId(item.ProdutoId));
            else
                _context.CarrinhoItens.Add(item);

            _context.Carrinho.Update(carrinho);
        }

        private async Task<CarrinhoItem> ObterItemCarrinhoValidado(Guid produtoId, Models.Carrinho carrinho, CarrinhoItem item = null)
        {
            if (item is not null && produtoId != item.ProdutoId)
            {
                AdicionarErroProcessamento("Item Inválido.");
                return null;
            }

            if (carrinho is null)
            {
                AdicionarErroProcessamento("Carrinho não encontrado");
                return null;
            }

            var itemCarrinho = await _context.CarrinhoItens.FirstOrDefaultAsync(x => x.CarrinhoId == carrinho.Id &&
                                                                                     x.ProdutoId == produtoId);

            if (itemCarrinho is null || !carrinho.ItemExistenteNoCarrinho(itemCarrinho))
            {
                AdicionarErroProcessamento("O item não está no carrinho");
                return null;
            }

            return itemCarrinho;
        }

        private async Task SalvarCarrinho()
        {
            var result = await _context.SaveChangesAsync();
            if (result <= 0) AdicionarErroProcessamento("Não foi possível persistir os dados no banco");
        }

        private bool ValidarCarrinho(Models.Carrinho carrinho)
        {
            if (carrinho.CarrinhoEhValido())
                return true;

            carrinho.ValidationResult.Errors.ToList().ForEach(x => AdicionarErroProcessamento(x.ErrorMessage));
            return false;
        }
    }
}
