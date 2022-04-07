using BuenoStore.BuildingBlocks.Models;
using BuenoStore.Pedido.Api.Models.Enum;

namespace BuenoStore.Pedido.Api.Models
{
    public class Pedido : Entity, IAggregateRoot
    {
        public Pedido(Guid clienteId, decimal valorTotal, List<PedidoItem> pedidoItems)
        {
            ClienteId = clienteId;
            ValorTotal = valorTotal;
            _pedidoItems = pedidoItems;
        }

        protected Pedido() { }

        public int Codigo { get; private set; }
        public Guid ClienteId { get; private set; }
        public decimal ValorTotal { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public PedidoStatus PedidoStatus { get; private set; }
        
        private readonly List<PedidoItem> _pedidoItems;
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;

        public Endereco Endereco { get; private set; }

        public void AutorizarPedido() => PedidoStatus = PedidoStatus.Autorizado;
        public void AtribuirEndereco(Endereco endereco) => Endereco = endereco;
    }
}
