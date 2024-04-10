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

        public IActionResult LesMedias()
        {
            TempData.Keep("PseudoUtilisateur");
            Utilisateur? utilisateur = gestionUtilisateur.TrouverUtilisateur(TempData["PseudoUtilisateur"] as string);
            if (utilisateur == null || utilisateur.RoleUser != EnumRole.Admin)
            {
                return RedirectToAction("Accueil", "NonConnecte");
            }
            return View();
        }
        public IActionResult LesUtilisateurs()
        {
            TempData.Keep("PseudoUtilisateur");

            Utilisateur? utilisateur = gestionUtilisateur.TrouverUtilisateur(TempData["PseudoUtilisateur"] as string);
            if (utilisateur == null || utilisateur.RoleUser != EnumRole.Admin)
            {
                return RedirectToAction("Accueil", "NonConnecte");
            }
            TempData.Keep("RoleUtilisateur");
            return View(gestionUtilisateur.ListeUtilisateurs);
        }
    }
}
