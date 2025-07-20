using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PopCinema.Models.CinemaM;
using Stripe.Checkout;

namespace PopCinema.Areas.Clients.Controllers
{
    [Area("Clients")]
    [Route("Clients/CheckOut")]
    [Authorize]
    public class CheckOutController : Controller
    {
        private readonly IBookingRepository bookingRepository;
        private readonly ICartRepository cartRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IBookedItemRepository bookedItemRepository;
        private readonly IShowTimeRepository showTimeRepository;

        public CheckOutController(IBookingRepository bookingRepository, ICartRepository cartRepository,
                    UserManager<ApplicationUser> userManager, IBookedItemRepository bookedItemRepository, IShowTimeRepository showTimeRepository)
        {
            this.bookingRepository = bookingRepository;
            this.cartRepository = cartRepository;
            this.userManager = userManager;
            this.bookedItemRepository = bookedItemRepository;
            this.showTimeRepository = showTimeRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Success")]
        public async Task<IActionResult> Success([FromQuery]List<int>? Ids)
        {
            if (Ids == null || Ids.Count == 0)
                return BadRequest("No booking IDs were provided.");

            var service = new SessionService();
            

            foreach (int id in Ids) {
                var booking = await bookingRepository.GetOneAsync(b => b.Id == id);
                if (booking is null) return BadRequest();

                var session = service.Get(booking.SessionId);
                booking.PaymentId = session.PaymentIntentId;   
            }
            await bookingRepository.CommitAsync();

            var user = await userManager.GetUserAsync(User);
            if (user is null) return BadRequest();

            var carts = await cartRepository.GetAsync(c => c.ApplicationUserId == user.Id, include: b=> b.Include(b=> b.ShowTime).ThenInclude(s=> s.Movie));

            List<BookedItem> bookedItems = new();
            int loopIds = 0;
            foreach(var item in carts)
            {
                if (item.ShowTime is null) return BadRequest();
                item.ShowTime.Movie.Traffic++;
                bookedItems.Add(new()
                {
                    ShowTimeId = item.ShowTimeId,
                    BookingId = Ids[loopIds++],
                    TotalPrice = item.ShowTime.TicketPrice * item.Count
                   
                });
            }
            await bookedItemRepository.CreateRangeAsync(bookedItems);
            foreach(var item in carts)
            {
                await cartRepository.DeleteAsync(item);
            }

            return RedirectToAction(nameof(Index), "Home", new { area = "Clients" });
        }
        [HttpGet("Cancel")]
        public IActionResult Cancel()
        {
            return RedirectToAction(nameof(Index), "Cart", new { area = "Clients" });
        }
    }
}
