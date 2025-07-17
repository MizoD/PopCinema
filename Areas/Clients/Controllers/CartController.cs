using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopCinema.Models.MovieM;
using System.Threading.Tasks;

namespace PopCinema.Areas.Clients.Controllers
{
    [Authorize]
    [Area("Clients")]
    public class CartController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IShowTimeRepository showTimeRepository;
        private readonly ICartRepository cartRepository;

        public CartController(UserManager<ApplicationUser> userManager, IShowTimeRepository showTimeRepository, ICartRepository cartRepository)
        {
            this.userManager = userManager;
            this.showTimeRepository = showTimeRepository;
            this.cartRepository = cartRepository;
        }
        public async Task<IActionResult> Index()
        {
            var carts = await cartRepository.GetAsync(include: c=> c.Include(c=> c.ShowTime).ThenInclude(s=> s.Movie)
                                            .ThenInclude(s=> s.CinemaHall)); 
            return View(carts);
        }

        public async Task<IActionResult> Remove(string userId, int showTimeId)
        {
            var cartItem = await cartRepository.GetOneAsync(c => c.ApplicationUserId == userId && c.ShowTimeId == showTimeId);
            if (cartItem is not null)
            {
                await cartRepository.DeleteAsync(cartItem);
            }

            return RedirectToAction(nameof(Index));
           
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(int ShowTimeId, int Count, string? PromoCode)
        {
            
            var user = await userManager.GetUserAsync(User);
            
            if (user is null) return NotFound();
            var showtime = await showTimeRepository.GetOneAsync(e=> e.Id == ShowTimeId, include: s=> s.Include(s=> s.Movie)
                                .Include(s=> s.CinemaHall).Include(s=> s.Bookings));
            if (showtime is not null && showtime.Bookings is not null)
            {
                int availableSeats = showtime.CinemaHall.Capacity - showtime.Bookings.Where(b => b.ShowTimeId == ShowTimeId)
                    .Sum(b => b.NumberOfSeats);


                if (Count < availableSeats && Count > 0)
                {
                    var ticketInCart = await cartRepository.GetOneAsync(e=> e.ApplicationUserId == user.Id && e.ShowTimeId == showtime.Id);
                    if(ticketInCart is not null)
                    {
                        ticketInCart.Count += Count;
                        await cartRepository.UpdateAsync(ticketInCart);
                    }
                    else
                    {
                        showtime.Movie.Traffic++;
                        decimal ticketsTotal = showtime.TicketPrice * Count;
                        await cartRepository.CreateAsync(new  Cart()
                        {
                            ApplicationUserId = user.Id,
                            Count = Count,
                            ShowTimeId = ShowTimeId
                        });
                    }   
                    
                }
                else
                {
                    var vm = new BookingVM
                    {
                        ShowTimeId = ShowTimeId,
                        ShowTimes = showtime.Movie.ShowTimes.OrderBy(st => st.StartTime).ToList(),
                        MovieTitle = showtime.Movie.Title,
                        CinemaHallId = showtime.CinemaHallId,
                        AvailableSeats = availableSeats,
                        TicketPrice = showtime.TicketPrice
                    };
                    ModelState.AddModelError("", $"Only {availableSeats} seats are available.");
                    return RedirectToAction(nameof(Index), "Booking", new {area = "Clients", id = showtime.MovieId });
                }

                   
            }
            return  RedirectToAction(nameof(Index), "Cart", new { area = "Clients" });
        }
    }
}
