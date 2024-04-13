using Microsoft.AspNetCore.Mvc;
using ProjetCatalogue.Models;

namespace ProjetCatalogue.Controllers
{
    /// <summary>
    /// contrôleur pour ce qui concerne les pages Administrateur connecté
    /// </summary>
    public class AdministrateurController : Controller
    {
        private readonly ILogger<AdministrateurController> _logger;
        private GestionUtilisateur gestionUtilisateur;

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="logger"></param>
        public AdministrateurController(ILogger<AdministrateurController> logger)
        {
            _logger = logger;
            gestionUtilisateur = new GestionUtilisateur();
            gestionUtilisateur.DeserialisationJSONUtilisateur(PathFinder.PathJsonUtilisateur);
        }

        /// <summary>
        /// Action qui déclenche la vue LesMedias
        /// </summary>
        /// <returns>la vue LesMedias</returns>
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

        /// <summary>
        /// Action qui déclenche la vue LesUtilisateurs en lui passant un modèle (la liste des utilisateurs du site)
        /// </summary>
        /// <returns>la vue LesUtilisateurs</returns>
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

        /// <summary>
        /// Action qui déclenche la vue LesUtilisateurs en lui passant la liste des utilisateurs du site,
        /// après avoir fait la suppression d'un utilisateur dont le pseudo a été reçu en paramètre
        /// (si l'utilisateur est effectivement trouvé). Un message de confirmation est aussi créé
        /// et passé à la vue)
        /// </summary>
        /// <param name="pseudo">le pseudo de l'utilisateur à supprimer</param>
        /// <returns>la vue LesUtilisateurs</returns>
        public IActionResult SupprimerUtilisateur(string pseudo)
        {
            TempData.Keep("PseudoUtilisateur");
            TempData.Keep("RoleUtilisateur");
            Utilisateur? utilisateur = gestionUtilisateur.TrouverUtilisateur(pseudo);

            if (utilisateur != null)
            {
                gestionUtilisateur.SupprimerUtilisateur(utilisateur);
                gestionUtilisateur.SerialisationUtilisateurs(PathFinder.PathJsonUtilisateur);
                ViewBag.MessageConfirmation = "L'utilisateur " + pseudo + " a bien été supprimé.";
            }


            return View("LesUtilisateurs", gestionUtilisateur.ListeUtilisateurs);
        }
    }
}
