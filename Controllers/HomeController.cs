using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VideoLibraryServer.Models;
using VideoLibraryServer.services;

namespace VideoLibraryServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly VideoScanner _scanner;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, VideoScanner scanner, IConfiguration config)
        {
            _logger = logger;
            _scanner = scanner;
            _config = config;
        }

        public IActionResult Index()
        {
            var folders = _config.GetSection("VideoFolders").Get<string[]>();
            var videos = _scanner.ScanFolders(folders);
            return View(videos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("stream/{*filePath}")]
        public IActionResult Stream(string filePath)
        {
            var folders = _config.GetSection("VideoFolders").Get<string[]>();
            foreach (var folder in folders)
            {
                var fullPath = Path.Combine(folder, filePath);
                if (System.IO.File.Exists(fullPath))
                {
                    var stream = System.IO.File.OpenRead(fullPath);
                    return File(stream, "video/mp4", enableRangeProcessing: true);
                }
            }
            return NotFound();
        }
    }
}
