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
        public IActionResult ResultatFormulaire(string idButton)
        {
            string pseudoUtilisateur;
            string motDePasse;
            Utilisateur? utilisateur = null;
            string? messageErreur = null;

            if (Request.Form["boutonConnection"].Equals(""))
            {
                pseudoUtilisateur = Request.Form["PseudoConnection"];
                motDePasse = Request.Form["MotDePasseConnection"];

                if (!gestionUtilisateur.ValiderUtilisateur(pseudoUtilisateur, motDePasse, out utilisateur, out messageErreur))
                {
                    ViewData["pseudoConnection"] = pseudoUtilisateur;
                    utilisateur = null;
                }
                ViewBag.MessageErreurConnection = messageErreur;

            }
            else if (Request.Form["boutonInscription"].Equals(""))
            {
                pseudoUtilisateur = Request.Form["PseudoInscription"];
                motDePasse = Request.Form["MotDePasseInscription"];
                string nom = Request.Form["PrenomInscription"];
                string prenom = Request.Form["NomInscription"];
                string checkboxAdministrateur = Request.Form["RoleUserInscription"];

                bool estAdministrateur = false;

                if (checkboxAdministrateur != null)
                {
                    estAdministrateur = true;
                }

                if (gestionUtilisateur.CreationUtilisateur(pseudoUtilisateur, motDePasse, prenom, nom, estAdministrateur, out utilisateur, out messageErreur))
                {
                    if (!gestionUtilisateur.AjouterUtilisateur(utilisateur, out messageErreur))
                    {
                        utilisateur = null;
                        

                    }
                    gestionUtilisateur.SerialisationUtilisateurs(PathFinder.PathJsonUtilisateur);
                }
                ViewData["pseudoInscription"] = pseudoUtilisateur;
                ViewData["nomInscription"] = nom;
                ViewData["prenomInscription"] = prenom;
                ViewBag.MessageErreurInscription = messageErreur;
            }

            if (utilisateur != null)
            {
                TempData["PseudoUtilisateur"] = utilisateur.Pseudo;
                TempData.Keep("PseudoUtilisateur");
                if (utilisateur.RoleUser == EnumRole.UtilisateurSimple)
                {
                    return RedirectToAction("TousLesMedias", "Utilisateur");

                }
                else if (utilisateur.RoleUser == EnumRole.Admin)
                {
                    return RedirectToAction("LesUtilisateurs", "Administrateur");
                }
            }
            return View("Accueil");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}