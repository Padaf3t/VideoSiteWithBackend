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
        private readonly Catalogue catalogue;

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="logger"></param>
        public AdministrateurController(ILogger<AdministrateurController> logger)
        {
            _logger = logger;
            gestionUtilisateur = new GestionUtilisateur();
            catalogue = new Catalogue();
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

            List<Video> listeVideo = catalogue.DbSetVideos.ToList();

            return View(listeVideo);
        }

        /// <summary>
        /// Action qui déclenche la vue VideoSpecifique
        /// </summary>
        /// <param name="id">L'id de la vidéo à utiliser</param>
        /// <returns>soit un RedirectToAction vers l'accueil non connecté si pas d'utilisateur trouvé;
        /// soit la vue VideoSpecifique si utilisateur légitime</returns>
        public IActionResult VideoSpecifiqueAdmin(int id)
        {
            TempData.Keep("PseudoUtilisateur");
            ViewBag.EstFavori = false;

            Video? video = catalogue.TrouverUneVideo(id);
            Utilisateur? utilisateur = gestionUtilisateur.TrouverUtilisateur(TempData["PseudoUtilisateur"] as string);

            //verification que l'utilisateur est un utilisateur simple
            if (utilisateur == null || utilisateur.RoleUser != EnumRole.Admin)
            {
                return RedirectToAction("Accueil", "NonConnecte");
            }

            return View(video);
        }

        /// <summary>
        /// Action qui déclenche la vue LesUtilisateurs (accueil lorsque connecté Administrateur)
        /// </summary>
        /// <returns>soit un RedirectToAction vers l'accueil non connecté si pas d'utilisateur trouvé;
        /// soit la vue LesUtilisateurs si utilisateur légitime</returns>
        public IActionResult LesUtilisateurs()
        {
            TempData.Keep("PseudoUtilisateur");

            Utilisateur? utilisateur = gestionUtilisateur.TrouverUtilisateur(TempData["PseudoUtilisateur"] as string);

            //verification que l'utilisateur est un utilisateur simple
            if (utilisateur == null || utilisateur.RoleUser != EnumRole.Admin)
            {
                return RedirectToAction("Accueil", "NonConnecte");
            }

            TempData.Keep("RoleUtilisateur");
            return View(gestionUtilisateur.DbSetUtilisateurs.ToList());
        }

        /// <summary>
        /// Action qui déclenche la vue LesUtilisateurs (Un message de confirmation est aussi créé et passé à la vue)
        /// après avoir déclenché la suppression de celui-ci et de ses favoris
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
                //supprime les favoris de l'utilisateur avant de le supprimer
                GestionFavori gestionFavori = new GestionFavori();
                List<Favori> listeFavorisUtilisateur = gestionFavori.ObtenirFavorisUtilisateur(utilisateur);
                foreach (Favori favori in listeFavorisUtilisateur)
                {
                    gestionFavori.SupprimerFavori(favori);
                }

                //supprime l'utilisateur
                gestionUtilisateur.SupprimerUtilisateur(utilisateur);
                ViewBag.MessageConfirmation = "L'utilisateur " + pseudo + " a bien été supprimé.";
            }


            return View("LesUtilisateurs", gestionUtilisateur.DbSetUtilisateurs.ToList());
        }

        /// <summary>
        /// Action qui modifie le rôle d'un utilisateur et retourne la vue LesUtilisateurs
        /// (Un message de confirmation est aussi créé et passé à la vue)
        /// </summary>
        /// <param name="pseudo">Le pseudo de l'utilisateur dont il faut modifier le rôle</param>
        /// <returns>la vue LesUtilisateurs</returns>
        public IActionResult ModifierRoleUtilisateur(string pseudo)
        {
            TempData.Keep("PseudoUtilisateur");
            TempData.Keep("RoleUtilisateur");
            Utilisateur? utilisateur = gestionUtilisateur.TrouverUtilisateur(pseudo);

            if (utilisateur != null)
            {
                //si l'utilisateur est simple, il devient admin, et vice versa
                if(utilisateur.RoleUser == EnumRole.UtilisateurSimple)
                {
                    gestionUtilisateur.ModifierRoleUtilisateur(utilisateur, EnumRole.Admin);
                }
                else if (utilisateur.RoleUser == EnumRole.Admin)
                {
                    gestionUtilisateur.ModifierRoleUtilisateur(utilisateur, EnumRole.UtilisateurSimple);
                }
                ViewBag.MessageConfirmation = "Le role de l'utilisateur " + pseudo + " a bien été modifié.";
            }

            return View("LesUtilisateurs", gestionUtilisateur.DbSetUtilisateurs.ToList());
        }
    }
}
