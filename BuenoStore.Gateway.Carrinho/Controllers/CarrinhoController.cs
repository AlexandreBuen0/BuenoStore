using BuenoStore.BuildingBlocks.Api.Controllers;
using BuenoStore.Gateway.Api.Models;
using BuenoStore.Gateway.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuenoStore.Gateway.Api.Controllers
{
    [Authorize]
    public class CarrinhoController : BaseController
    {
        private readonly ICarrinhoService _carrinhoService;
        private readonly ICatalogoService _catalogoService;

        public CarrinhoController(ICarrinhoService carrinhoService,
                                  ICatalogoService catalogoService)
        {
            _carrinhoService = carrinhoService;
            _catalogoService = catalogoService;
        }

        [HttpGet]
        [Route("compras/carrinho")]
        public async Task<IActionResult> ObterCarrinho() => CustomResponse(await _carrinhoService.ObterCarrinho());

        [HttpPost]
        [Route("compras/carrinho")]
        public async Task<IActionResult> AdicionarItemCarrinho(ItemCarrinhoViewModel itemCarrinho)
        {
            var produto = await _catalogoService.ObterPorId(itemCarrinho.ProdutoId);

            await ValidarItemCarrinho(produto, itemCarrinho.Quantidade);
            if (!OperacaoEhValida()) return CustomResponse();

            itemCarrinho.Nome = produto.Nome;
            itemCarrinho.Valor = produto.Valor;
            itemCarrinho.Imagem = produto.Imagem;

            var resposta = await _carrinhoService.AdicionarItemCarrinho(itemCarrinho);

            return CustomResponse(resposta);
        }

        private async Task ValidarItemCarrinho(ProdutoViewModel produto, int quantidade)
        {
            if (produto is null)
                AdicionarErroProcessamento("Produto não encontrado!");

            var carrinho = await _carrinhoService.ObterCarrinho();
            var itemCarrinho = carrinho.Itens.FirstOrDefault(p => p.ProdutoId == produto.Id);

            if (itemCarrinho is not null && itemCarrinho.Quantidade + quantidade > produto.QuantidadeEstoque)
            {
                AdicionarErroProcessamento($"O produto {produto.Nome} possui {produto.QuantidadeEstoque} unidades em estoque, você selecionou {itemCarrinho.Quantidade + quantidade}");
                return;
            }

            if (quantidade > produto.QuantidadeEstoque)
                AdicionarErroProcessamento($"O produto {produto.Nome} possui {produto.QuantidadeEstoque} unidades em estoque, você selecionou {quantidade}");
        }
    }
}
