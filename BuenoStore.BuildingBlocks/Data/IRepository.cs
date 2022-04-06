using BuenoStore.BuildingBlocks.Models;

namespace BuenoStore.BuildingBlocks.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {

    }
}
