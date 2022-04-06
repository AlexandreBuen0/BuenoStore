namespace BuenoStore.BuildingBlocks.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
