namespace PopCinema.Repositories
{
    public class BookingRepository : Repository<Booking> , IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context) { }
    }
}
