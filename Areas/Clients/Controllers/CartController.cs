using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopCinema.Models.MovieM;
using Stripe.Checkout;
using Stripe;
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
        private readonly IBookingRepository bookingRepository;

        public CartController(UserManager<ApplicationUser> userManager, IShowTimeRepository showTimeRepository,
            ICartRepository cartRepository, IBookingRepository bookingRepository)
        {
            this.userManager = userManager;
            this.showTimeRepository = showTimeRepository;
            this.cartRepository = cartRepository;
            this.bookingRepository = bookingRepository;
        }
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            if (user is not null) {
                var carts = await cartRepository.GetAsync(c => c.ApplicationUserId == user.Id, include: c => c.Include(c => c.ShowTime).ThenInclude(s => s.Movie)
                                            .ThenInclude(s => s.CinemaHall));
                return View(carts);
            }
            var cart = new List<Cart>();

            return View(cart);
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

        public async Task<IActionResult> Pay()
        {
            var user = await userManager.GetUserAsync(User);

            if (user is null) return NotFound();

            var itemsInCart = await cartRepository.GetAsync(e => e.ApplicationUserId == user.Id, include: c => c.Include(c => c.ShowTime)
                                                        .ThenInclude(s => s.Movie));
            int getIds = 0;
            if (itemsInCart is not null)
            { //StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
                foreach(var item in itemsInCart) {
                    if (item.ShowTime is null) return BadRequest();
                    await bookingRepository.CreateAsync(new()
                    {
                        ApplicationUserId = user.Id,
                        TotalPrice = item.ShowTime.TicketPrice * item.Count,
                        ShowTimeId = item.ShowTimeId,
                        CinemaHallId = item.ShowTime.CinemaHallId,
                        NumberOfSeats = item.Count,
                    });
                    getIds++;
                }

                await bookingRepository.CommitAsync();
                var bookingsCount = (await bookingRepository.GetAsync(b => b.ApplicationUserId == user.Id)).Count();
                var bookings = (await bookingRepository.GetAsync(b => b.ApplicationUserId == user.Id))
                    .OrderBy(b=> b.Id).Skip(bookingsCount - getIds).Take(getIds);

                if (bookings is null) return BadRequest();

                List<int> Ids = new();
                foreach(var item in bookings)
                {
                    Ids.Add(item.Id);
                }
                //var booking = (await bookingRepository.GetAsync(b => b.ApplicationUserId == user.Id)).OrderBy(b => b.Id).LastOrDefault();
                

                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = $"{Request.Scheme}://{Request.Host}/Clients/CheckOut/Success?{string.Join("&", Ids.Select(id => $"Ids={id}"))}",
                    CancelUrl = $"{Request.Scheme}://{Request.Host}/Clients/CheckOut/Cancel",
                };
                foreach (var item in itemsInCart)
                {
                    
                        options.LineItems.Add(new SessionLineItemOptions
                        {
                            PriceData = new SessionLineItemPriceDataOptions
                            {
                                Currency = "egp",
                                ProductData = new SessionLineItemPriceDataProductDataOptions
                                {
                                    Name = item.ShowTime.Movie.Title,
                                    Description = item.ShowTime.StartTime.ToString("f"),
                                },
                                UnitAmount = (long)item.ShowTime.TicketPrice *100,
                            },
                            Quantity = item.Count,
                        });
                }
                var service = new SessionService();
                var session = service.Create(options);
                foreach(var item in bookings) {item.SessionId = session.Id; }
                await bookingRepository.CommitAsync();
                return Redirect(session.Url);
            }
            return NotFound();
        }

        
    }
    
}
