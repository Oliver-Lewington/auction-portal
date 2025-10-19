namespace AuctionPortal.Services
{
    public interface IDataService
    {
        Task DeleteEntityByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}