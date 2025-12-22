using Microsoft.AspNetCore.Mvc;

namespace RouletteApi.Controllers
{
    public class LeaderboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
