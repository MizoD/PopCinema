namespace PopCinema.Repositories.IRepositories
{
    public interface IBookedItemRepository : IRepository<BookedItem>
    {
        Task<bool> CreateRangeAsync(List<BookedItem> entity);
    }
}
