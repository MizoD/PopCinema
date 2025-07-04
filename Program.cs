using Microsoft.EntityFrameworkCore;

namespace PopCinema
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer("Data Source=.;Initial Catalog=PopCinema; Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;"));

            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            //builder.Services.AddScoped<IActorRepository, ActorRepository>();
            //builder.Services.AddScoped<IDirectorRepository, DirectorRepository>();
            //builder.Services.AddScoped<IShowTimeRepository, ShowTimeRepository>();
            //builder.Services.AddScoped<IBookingRepository, BookingRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapGet("/", context =>
            {
                context.Response.Redirect("/Clients/Home/Index");
                return Task.CompletedTask;
            });
            app.MapControllerRoute(
                name: "default",
                pattern: "{area=Clients}/{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
