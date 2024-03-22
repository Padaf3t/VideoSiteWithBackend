using Microsoft.AspNetCore.Mvc;

namespace ProjetCatalogue.Controllers
{
    public class UtilisateurController : Controller
    {
        public IActionResult Acceuil(Utilisateur utilisateur)
        {
            ViewBag.Utilisateur = utilisateur;
            return View();
        }
        public IActionResult TousLesMedias()
        {
            return View();
        }
        public IActionResult MesFavoris()
        {
            return View();
        }

    }
}
