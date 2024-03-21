using Microsoft.AspNetCore.Mvc;

namespace ProjetCatalogue.Controllers
{
    public class AdministrateurController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
