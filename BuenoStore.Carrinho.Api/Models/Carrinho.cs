using BuenoStore.Carrinho.Api.Models.Validation;
using FluentValidation.Results;

namespace BuenoStore.Carrinho.Api.Models
{
    public class Carrinho
    {
        public Carrinho(Guid clienteId)
        {
            Id = Guid.NewGuid();
            ClienteId = clienteId;
        }

        public Carrinho() { }

        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }
        public decimal ValorTotal { get; set; }
        public List<CarrinhoItem> Itens { get; set; } = new List<CarrinhoItem>();

        public ValidationResult ValidationResult { get; set; }

        internal void CalcularValorCarrinho() => ValorTotal = Itens.Sum(p => p.CalcularValor());

        internal bool ItemExistenteNoCarrinho(CarrinhoItem item) => Itens.Any(p => p.ProdutoId == item.ProdutoId);

        internal CarrinhoItem ObterPorProdutoId(Guid produtoId) => Itens.FirstOrDefault(p => p.ProdutoId == produtoId);

        internal void AdicionarItemCarrinho(CarrinhoItem item)
        {
            item.VincularCarrinho(Id);

            if (ItemExistenteNoCarrinho(item))
            {
                var itemExistente = ObterPorProdutoId(item.ProdutoId);
                itemExistente.AdicionarUnidades(item.Quantidade);

                item = itemExistente;
                Itens.Remove(itemExistente);
            }

            Itens.Add(item);
            CalcularValorCarrinho();
        }

        internal void AtualizarItem(CarrinhoItem item)
        {
            item.VincularCarrinho(Id);

            var itemExistente = ObterPorProdutoId(item.ProdutoId);

            Itens.Remove(itemExistente);
            Itens.Add(item);

            CalcularValorCarrinho();
        }

        internal void AtualizarUnidades(CarrinhoItem item, int unidades)
        {
            item.AtualizarUnidades(unidades);
            AtualizarItem(item);
        }

        internal void RemoverItem(CarrinhoItem item)
        {
            Itens.Remove(ObterPorProdutoId(item.ProdutoId));
            CalcularValorCarrinho();
        }

        internal bool CarrinhoEhValido()
        {
            var erros = Itens.SelectMany(i => new CarrinhoItemValidador().Validate(i).Errors).ToList();
            erros.AddRange(new CarrinhoValidador().Validate(this).Errors);
            ValidationResult = new ValidationResult(erros);

            return ValidationResult.IsValid;
        }
    }
}
