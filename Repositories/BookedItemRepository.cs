namespace PopCinema.Repositories
{
    public class BookedItemRepository : Repository<BookedItem> , IBookedItemRepository
    {
        public BookedItemRepository(ApplicationDbContext context) : base(context) { }

        public async Task<bool> CreateRangeAsync(List<BookedItem> entity)
        {
            try
            {
                 _context.BookedItems.AddRange(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

        }
    }
}
