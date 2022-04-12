namespace BuenoStore.Carrinho.Api.Models
{
    public class CarrinhoItemViewModel
    {
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal Valor { get; set; }
    }
}
