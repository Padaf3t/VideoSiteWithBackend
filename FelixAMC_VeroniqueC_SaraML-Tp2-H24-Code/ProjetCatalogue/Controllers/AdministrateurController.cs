using Microsoft.AspNetCore.Mvc;
using ProjetCatalogue.Models;

namespace ProjetCatalogue.Controllers
{
    public class AdministrateurController : Controller
    {
        private readonly ILogger<AdministrateurController> _logger;
        private GestionUtilisateur gestionUtilisateur;
        public AdministrateurController(ILogger<AdministrateurController> logger)
        {
            _logger = logger;
            gestionUtilisateur = new GestionUtilisateur();
            gestionUtilisateur.DeserialisationJSONUtilisateur(PathFinder.PathJsonUtilisateur);
        }

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
            return View(gestionUtilisateur.ListeUtilisateurs);
        }
    }
}
