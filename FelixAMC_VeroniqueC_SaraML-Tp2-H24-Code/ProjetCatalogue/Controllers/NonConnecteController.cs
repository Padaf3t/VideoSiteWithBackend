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
            TempData.Clear();
            return View();
        }

        [HttpPost]
        public IActionResult ResultatFormulaireConnection()
        {
            string pseudoUtilisateur = Request.Form["PseudoConnection"];
            string motDePasse = Request.Form["MotDePasseConnection"];
            Utilisateur? utilisateur;
            string messageErreur;

            if (gestionUtilisateur.ValiderUtilisateur(pseudoUtilisateur, motDePasse, out utilisateur, out messageErreur)) {

                TempData["PseudoUtilisateur"] = utilisateur.Pseudo;
                TempData.Keep();
                string viewRetournee = "";
                if (utilisateur.RoleUser == EnumRole.UtilisateurSimple)
                {
                    return RedirectToAction("TousLesMedias", "Utilisateur");
                    
                }else if(utilisateur.RoleUser == EnumRole.Admin)
                {
                    return RedirectToAction("LesUtilisateurs", "Administrateur");
                }
                return View(viewRetournee);
            }
            ViewBag.MessageErreur = messageErreur;
            return View("Accueil");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}