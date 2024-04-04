using Microsoft.AspNetCore.Mvc;

namespace ProjetCatalogue.Controllers
{
    public class AdministrateurController : Controller
    {
        private readonly ILogger<AdministrateurController> _logger;
        private GestionUtilisateur gestionUtilisateur;
        public AdministrateurController(ILogger<AdministrateurController> logger)
        {
            Application application = new Application();
            _logger = logger;
            gestionUtilisateur = new GestionUtilisateur();
            gestionUtilisateur.DeserialisationJSONUtilisateur("Json/utilisateurs.JSON");
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
