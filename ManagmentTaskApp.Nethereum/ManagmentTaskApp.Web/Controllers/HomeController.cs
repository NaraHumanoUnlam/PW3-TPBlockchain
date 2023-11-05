using ManagmentTaskApp.Nethereum;
using ManagmentTaskApp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ManagmentTaskApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlockchainServicio _blockchainServicio;
        public HomeController(ILogger<HomeController> logger, IBlockchainServicio blockchainServicio)
        {
            _logger = logger;
            _blockchainServicio = blockchainServicio;
        }

        public IActionResult Index()
        {
            ViewBag.Balances = _blockchainServicio.ObtenerBalances();
            return View();
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
    }
}