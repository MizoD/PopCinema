using Microsoft.EntityFrameworkCore;
using PopCinema.Models.CinemaM;
using PopCinema.Models.MovieM;
using PopCinema.Models.personM;

namespace PopCinema.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Movie> Movies { set; get; }
        public DbSet<Actor> Actors { set; get; }
        public DbSet<Genre> Genres { set; get; }
        public DbSet<Director> Directors { set; get; }
        public DbSet<Booking> Bookings { set; get; }
        public DbSet<Seat> Seats { set; get; }
        public DbSet<Promotion> Promotions { set; get; }
        public DbSet<Payment> Payments { set; get; }
        public DbSet<CinemaHall> CinemaHalls { set; get; }
        public DbSet<ShowTime> ShowTimes { set; get; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=PopCinema; Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ActorMovie>().HasKey(e => new { e.MovieId, e.ActorId});

            modelBuilder.Entity<ActorMovie>()
            .HasOne(am => am.Movie)
            .WithMany(m => m.Actors)
            .HasForeignKey(am => am.MovieId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ActorMovie>()
            .HasOne(am => am.Actor)
            .WithMany(a => a.Movies)
            .HasForeignKey(am => am.ActorId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ShowTime>()
            .HasOne(st => st.Movie)
            .WithMany(m => m.ShowTimes)
            .HasForeignKey(st => st.MovieId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
            .HasOne(b => b.ShowTime)
            .WithMany(st => st.Bookings)
            .HasForeignKey(b => b.ShowTimeId)
            .OnDelete(DeleteBehavior.NoAction);


        }
    }
}
