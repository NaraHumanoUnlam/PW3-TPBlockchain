using Microsoft.AspNetCore.Mvc;
using Nethereum.Model;
using SmartContractNethereum.Servicios;
using System.Diagnostics;
using System.Transactions;

namespace SmartContractNethereum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISmartContractServicio _smartContractServicio;
        //
        public HomeController(ILogger<HomeController> logger, ISmartContractServicio smartContractServicio)
        {
            _logger = logger;
           _smartContractServicio = smartContractServicio;
        }

        public IActionResult Index()
        {
           ViewBag.Transacciones = _smartContractServicio.ListarTransacciones();
            return View();
        }

        public IActionResult CrearTransaccion()
        {
            ViewBag.Cuentas = _smartContractServicio.ListarCuentas();
            return View(new Data.DTO.Transaction());
        }

        [HttpPost]
        public IActionResult CrearTransaccion(Data.DTO.Transaction transaction, int IdCuenta)
        {
            Data.DTO.Account account = _smartContractServicio.ObtenerCuenta(IdCuenta);
            _smartContractServicio.CrearTransaccion(transaction,account);
            return RedirectToAction("Index");
        }
       
    }
}