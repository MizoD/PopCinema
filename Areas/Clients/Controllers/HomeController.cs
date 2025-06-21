using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopCinema.Models;

namespace PopCinema.Areas.Clients.Controllers;

[Area("Clients")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context = new();

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var movies = _context.Movies.Include(e=> e.ShowTimes).OrderByDescending(e=> e.Traffic).Skip(0).Take(8);
        return View(movies.ToList());
    }

    public IActionResult Actors()
    {
        var actors = _context.Actors;
        var directors = _context.Directors;

        ActorsWithDirectorsVM actorsWithDirectors = new ( actors.ToList(),directors.ToList() );
        return View(actorsWithDirectors);
    }

    public IActionResult NotFoundPage()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
