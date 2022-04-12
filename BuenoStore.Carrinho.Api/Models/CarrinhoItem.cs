using BuenoStore.Carrinho.Api.Models.Validation;
using System.Runtime.Serialization;

namespace BuenoStore.Carrinho.Api.Models
{
    public class CarrinhoItem
    {
        public CarrinhoItem()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
        public string Imagem { get; set; }

        public Guid CarrinhoId { get; set; }

        public Carrinho? Carrinho { get; set; }

        internal void VincularCarrinho(Guid carrinhoId) => CarrinhoId = carrinhoId;

        internal decimal CalcularValor() => Quantidade * Valor;

        internal void AdicionarUnidades(int unidades) => Quantidade += unidades;

        internal void AtualizarUnidades(int unidades) => Quantidade = unidades;

        internal bool CarrinhoItemEhValido() => new CarrinhoItemValidador().Validate(this).IsValid;
    }
}
