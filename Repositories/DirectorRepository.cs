namespace PopCinema.Repositories
{
    public class DirectorRepository : Repository<Director> , IDirectorRepository
    {
        public DirectorRepository(ApplicationDbContext context) : base(context) { }
    }
}
