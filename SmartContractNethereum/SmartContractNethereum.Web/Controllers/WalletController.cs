using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartContractNethereum.Data.EF;
using SmartContractNethereum.Servicios;

namespace SmartContractNethereum.Web.Controllers
{
    public class WalletController : Controller
    {

        private readonly ITransaccionServicio _transaccionServicio;
        private readonly INethereumServicio _nethereumServicio;

        public WalletController (ITransaccionServicio transaccionServicio, INethereumServicio nethereumServicio) 
        {
            _transaccionServicio = transaccionServicio;
            _nethereumServicio = nethereumServicio; 
         }

        public ActionResult Listar()
        {
            ViewBag.Transacciones = _transaccionServicio.ListarTransacciones();
            return View();
        }

       
        public ActionResult Crear()
        {
            ViewBag.Transacciones = _transaccionServicio.ListarTransacciones();
            return View(new Transaction());
        }

        // POST: WalletController/Create
        [HttpPost]
        public ActionResult Crear(string? sernderAdress, string? receiberAdress, decimal quantityTokens)
        {
            try
            {
                _nethereumServicio.TransactionEther(sernderAdress, receiberAdress, quantityTokens);
                return RedirectToAction("Listar");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return View();
            }
        }


    }
}

