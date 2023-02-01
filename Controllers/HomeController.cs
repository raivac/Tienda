using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tienda.Models;

namespace Tienda.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Alta() { 
        
            return View();
        }
        [HttpPost]
        public IActionResult Alta(IFormCollection collection) {
            
            MantenimientoArticulos ma = new MantenimientoArticulos(); 
            Articulo art = new Articulo();
            
                art.Codigo = int.Parse(collection["Codigo"].ToString());
                art.Descripcion = collection["Descripcion"].ToString();
                art.Precio = float.Parse(collection["Precio"].ToString());
            

            ma.Alta(art);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}