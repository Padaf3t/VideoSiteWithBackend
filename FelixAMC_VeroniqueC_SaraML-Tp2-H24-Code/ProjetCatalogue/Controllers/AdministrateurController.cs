using Microsoft.AspNetCore.Mvc;

namespace ProjetCatalogue.Controllers
{
    public class AdministrateurController : Controller
    {
        public IActionResult Acceuil()
        {
            return View();
        }
        public IActionResult LesMedias()
        {
            return View();
        }
        public IActionResult LesUtilisateurs()
        {
            return View();
        }
    }
}
