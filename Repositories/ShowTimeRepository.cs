namespace PopCinema.Repositories
{
    public class ShowTimeRepository : Repository<ShowTime> , IShowTimeRepository
    {
        public ShowTimeRepository(ApplicationDbContext context) : base(context) { }
    }
}
