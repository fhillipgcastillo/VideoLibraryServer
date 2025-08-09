using Microsoft.AspNetCore.Mvc;

namespace VideoLibraryServer.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
