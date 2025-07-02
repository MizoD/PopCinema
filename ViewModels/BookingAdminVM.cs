using Microsoft.AspNetCore.Mvc.Rendering;

namespace PopCinema.ViewModels
{
    public class BookingAdminVM
    {
        public List<SelectListItem> ShowTimes { get; set; } = new List<SelectListItem>();
        public Booking Booking { get; set; } = new Booking();
    }
}
