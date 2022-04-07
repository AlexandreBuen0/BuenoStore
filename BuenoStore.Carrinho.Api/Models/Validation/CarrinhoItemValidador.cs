using FluentValidation;

namespace BuenoStore.Carrinho.Api.Models.Validation
{
    public class CarrinhoItemValidador : AbstractValidator<CarrinhoItem>
    {
        public CarrinhoItemValidador()
        {
            RuleFor(c => c.ProdutoId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            RuleFor(c => c.Nome)
                .NotEmpty()
                .WithMessage("O nome do produto não foi informado");

            RuleFor(c => c.Quantidade)
                .GreaterThan(0)
                .WithMessage(item => $"A quantidade miníma para o {item.Nome} é 1");

            RuleFor(c => c.Valor)
                .GreaterThan(0)
                .WithMessage(item => $"O valor do {item.Nome} precisa ser maior que 0");
        }
    }
}
