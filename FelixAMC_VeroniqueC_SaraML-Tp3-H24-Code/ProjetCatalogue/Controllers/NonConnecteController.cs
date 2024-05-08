using Microsoft.AspNetCore.Mvc;
using ProjetCatalogue.Models;
using System.Diagnostics;

namespace ProjetCatalogue.Controllers
{
    /// <summary>
    /// Contrôleur pour ce qui concerne les pages non connecté
    /// </summary>
    public class NonConnecteController : Controller
    {
        private readonly ILogger<NonConnecteController> _logger;
        GestionUtilisateur gestionUtilisateur;

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="logger"></param>
        public NonConnecteController(ILogger<NonConnecteController> logger)
        {
            _logger = logger;
            gestionUtilisateur = new GestionUtilisateur();
        }

        /// <summary>
        /// Action qui déclenche la vue Accueil; vide le TempData
        /// </summary>
        /// <returns>la vue Accueil</returns>
        public IActionResult Accueil()
        {
            TempData.Clear();
            return View();
        }

        /// <summary>
        /// Action qui déclenche la vue Accueil; traite via HTTPPost le contenu des formulaires de connection et d'inscription;
        /// utilise des méthodes de validation pour en vérifier le contenu; va connecter un utilisateur si formulaire de connection
        /// (boutonConnection), ou bien créer un utilisateur puis le connecter si formulaire de création d'un compte (boutonInscription).
        /// Va placer le pseudo utilisateur dans un TempData. Si la connection de l'utilisateur/admin se fait effectivement,
        /// Va retourner un RedirectAction sur la page d'accueil connectée respective (utilisateur ou admin, selon); sinon, va retourner
        /// la vue Accueil.
        /// </summary>
        /// <param name="idButton"></param>
        /// <returns>la vue Accueil, ou bien un RedirectAction sur la vue accueil utilisateur ou la vue accueil administrateur</returns>
        [HttpPost]
        public IActionResult ResultatFormulaireConnection(Utilisateur utilisateur)
        {

            if (!gestionUtilisateur.ValiderUtilisateur(utilisateur, out string? messageErreur))
            {
                ViewData["pseudoConnection"] = utilisateur.Pseudo;
                utilisateur = null;
            }
            ViewBag.MessageErreurConnection = messageErreur;

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

        /// <summary>
        /// Action qui déclenche la vue Accueil; traite via HTTPPost le contenu des formulaires de connection et d'inscription;
        /// utilise des méthodes de validation pour en vérifier le contenu; va connecter un utilisateur si formulaire de connection
        /// (boutonConnection), ou bien créer un utilisateur puis le connecter si formulaire de création d'un compte (boutonInscription).
        /// Va placer le pseudo utilisateur dans un TempData. Si la connection de l'utilisateur/admin se fait effectivement,
        /// Va retourner un RedirectAction sur la page d'accueil connectée respective (utilisateur ou admin, selon); sinon, va retourner
        /// la vue Accueil.
        /// </summary>
        /// <param name="idButton"></param>
        /// <returns>la vue Accueil, ou bien un RedirectAction sur la vue accueil utilisateur ou la vue accueil administrateur</returns>

        [HttpPost]
        public IActionResult ResultatFormulaireInscription(Utilisateur pUtilisateur)
        {
            string pseudoUtilisateur = Request.Form["PseudoInscription"];
            string motDePasse = Request.Form["MotDePasseInscription"];
            string nom = Request.Form["NomInscription"];
            string prenom = Request.Form["PrenomInscription"];
            string checkboxAdministrateur = Request.Form["RoleUserInscription"];

            bool estAdministrateur = false;

            if (checkboxAdministrateur != null)
            {
                estAdministrateur = true;
            }

            if (gestionUtilisateur.CreationUtilisateur(pseudoUtilisateur, motDePasse, prenom, nom, estAdministrateur, out Utilisateur? utilisateur, out string? messageErreur))
            {
                if (!gestionUtilisateur.AjouterUtilisateur(utilisateur, out messageErreur))
                {
                    utilisateur = null;
                }
            }
            ViewData["pseudoInscription"] = pseudoUtilisateur;
            ViewData["nomInscription"] = nom;
            ViewData["prenomInscription"] = prenom;
            ViewBag.MessageErreurInscription = messageErreur;

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

        /// <summary>
        /// Action utilisée si erreur ; retourne une vue d'erreur
        /// </summary>
        /// <returns>Une vue d'erreur</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}