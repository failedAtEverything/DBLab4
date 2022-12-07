using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 240 + 5 * 12, Location = ResponseCacheLocation.Any)]
        public ActionResult Employees()
        {
            var model = Program._app.Services.GetService<TvChannelContext>()?.Employees.Take(40);

            return View(model);
        }

        [ResponseCache(Duration = 240 + 5 * 12, Location = ResponseCacheLocation.Any)]
        public ActionResult Genres()
        {
            var model = Program._app.Services.GetService<TvChannelContext>()?.Genres.Take(40);

            return View(model);
        }

        [ResponseCache(Duration = 240 + 5 * 12, Location = ResponseCacheLocation.Any)]
        public ActionResult Programs()
        {
            var model = from program in Program._app.Services.GetService<TvChannelContext>()?.Programs
                        join genre in Program._app.Services.GetService<TvChannelContext>()?.Genres on program.GenreId equals genre.Id
                        where program.Id < 40
                        select new { Id = program.Id, Name = program.Name, Length = program.Length, Rating = program.Rating, Genre = genre.Name, Description = program.Description };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}