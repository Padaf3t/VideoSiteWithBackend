using Microsoft.AspNetCore.Mvc;
using ProjetCatalogue.Models;
using System.Diagnostics;

namespace ProjetCatalogue.Controllers
{
    public class NonConnecteController : Controller
    {
        private readonly ILogger<NonConnecteController> _logger;
        GestionUtilisateur gestionUtilisateur;

        public NonConnecteController(ILogger<NonConnecteController> logger)
        {
            _logger = logger;
            gestionUtilisateur = new GestionUtilisateur();
            gestionUtilisateur.DeserialisationJSONUtilisateur(PathFinder.PathJsonUtilisateur);
        }

        public IActionResult Accueil()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResultatFormulaireConnection()
        {
            string pseudoUtilisateur = Request.Form["Pseudo"];
            string motDePasse = Request.Form["MotDePasse"];

            Utilisateur utilisateur = new Utilisateur(pseudoUtilisateur, motDePasse);

            if (gestionUtilisateur.ValiderUtilisateur(utilisateur) {
                
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}