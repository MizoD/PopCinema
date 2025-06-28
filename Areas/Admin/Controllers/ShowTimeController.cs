using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PopCinema.Models.MovieM;
using PopCinema.ViewModels;

namespace PopCinema.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ShowTimeController : Controller
    {
        ApplicationDbContext _context = new();
        public IActionResult Index(int page = 1)
        {
            const int pageSize = 9;
            IQueryable<ShowTime> showtimes = _context.ShowTimes
                .Include(s => s.Movie)
                .Include(s => s.CinemaHall);

            int totalShowTimes = showtimes.Count();

            showtimes = showtimes.OrderBy(s => s.StartTime)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            ShowTimesVM vm = new ShowTimesVM { ShowTimes = showtimes.ToList(), CurrentPage = page, TotalPages = (int)Math.Ceiling((double)totalShowTimes / pageSize) };

            return View(vm);
        }

        public IActionResult Create()
        {
            MoviesWithCinemasVM vm = new MoviesWithCinemasVM
            {
                ShowTime = new ShowTime
                {
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now.AddHours(2) 
                },
                Movies = _context.Movies.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Title
                }),

                CinemaHalls = _context.CinemaHalls.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };
            
            return View(vm);
        }
        [HttpPost]
        public IActionResult Create(MoviesWithCinemasVM vm)
        {
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state.Errors.Any())
                    {
                        Console.WriteLine($"Key: {key}, Error: {state.Errors.First().ErrorMessage}");
                    }
                }
                vm = new MoviesWithCinemasVM
                {
                    ShowTime = new ShowTime
                    {
                        StartTime = vm.ShowTime.StartTime,
                        EndTime = vm.ShowTime.EndTime
                    },
                    Movies = _context.Movies.Select(m => new SelectListItem
                    {
                        Value = m.Id.ToString(),
                        Text = m.Title
                    }),

                    CinemaHalls = _context.CinemaHalls.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList()
                };
                return View(vm);
            }
            _context.ShowTimes.Add(vm.ShowTime);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int Id)
        {
            var showtime = _context.ShowTimes.Include(s=> s.CinemaHall).Include(s=> s.Movie).FirstOrDefault(s=> s.Id == Id);
            if (showtime is null) return NotFound();
            MoviesWithCinemasVM vm = new MoviesWithCinemasVM
            {
                ShowTime = showtime,
                Movies = _context.Movies.Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Title
                }),

                CinemaHalls = _context.CinemaHalls.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(MoviesWithCinemasVM vm)
        {
            if (!ModelState.IsValid)
            {
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state.Errors.Any())
                    {
                        Console.WriteLine($"Key: {key}, Error: {state.Errors.First().ErrorMessage}");
                    }
                }
                vm = new MoviesWithCinemasVM
                {
                    ShowTime = new ShowTime
                    {
                        StartTime = vm.ShowTime.StartTime,
                        EndTime = vm.ShowTime.EndTime
                    },
                    Movies = _context.Movies.Select(m => new SelectListItem
                    {
                        Value = m.Id.ToString(),
                        Text = m.Title
                    }),

                    CinemaHalls = _context.CinemaHalls.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList()
                };
                return View(vm);
            }
            _context.ShowTimes.Update(vm.ShowTime);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int Id)
        {
            var showtime = _context.ShowTimes.Include(s=> s.Bookings).FirstOrDefault(s=> s.Id == Id);
            if (showtime is null) return NotFound();

            if (showtime.Bookings != null && showtime.Bookings.Any())
            {
                _context.Bookings.RemoveRange(showtime.Bookings);
            }
            _context.ShowTimes.Remove(showtime);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
