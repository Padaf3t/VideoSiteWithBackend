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

            //Ici le best serait éventuellement de faire un objet ayant un video et un bool favori en attributs
            return View(listeVideo);
        }

        /// <summary>
        /// Action qui déclenche la vue VideoSpecifique en lui passant la vidéo à utiliser. Va recevoir un id de vidéo
        /// et potentiellement un bool signalant si la vidéo à utiliser fait partie d'un favori qui vient d'être modifié.
        /// Va donc valider que l'utilisateur connecté n'est pas null (sinon va faire un RedirectToAction vers l'accueil
        /// non connecté (action accueil du controller nonConnecte), et si l'utilisateur est ok, va valider que la vidéo,
        /// selon l'id reçu en paramètre, existe bien dans le catalogue. Va ensuite voir si cette vidéo est un favori pour
        /// l'utilisateur, et si le favori est modifié (s'il l'est, va faire la modification du favori, donc l'ajouter ou
        /// le retirer des favoris et mettre un message en conséquence dans le ViewBag). Va finalement retourner la vue
        /// VideoSpecifique en lui passant la vidéo à utiliser.
        /// </summary>
        /// <param name="id">L'id de la vidéo à utiliser</param>
        /// <param name="favoriEstModifie">booléen, s'il y a lieu, qui dit si on modifie un favori ou non</param>
        /// <returns>un IActionResult : soit un RedirectToAction vers l'accueil non connecté si pas d'utilisateur trouvé;
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
        /// Action qui déclenche la vue LesUtilisateurs (accueil lorsque connecté Administrateur) en lui passant un modèle 
        /// (la liste des utilisateurs du site) si L'utilisateur est valide; sinon va faire un RedirectToAction vers l'accueil
        /// non connecté
        /// </summary>
        /// <returns>la vue LesUtilisateurs ou un RedirectToAction vers l'accueil non connecté</returns>
        public IActionResult LesUtilisateurs()
        {
            TempData.Keep("PseudoUtilisateur");

            Utilisateur? utilisateur = gestionUtilisateur.TrouverUtilisateur(TempData["PseudoUtilisateur"] as string);
            if (utilisateur == null || utilisateur.RoleUser != EnumRole.Admin)
            {
                return RedirectToAction("Accueil", "NonConnecte");
            }
            TempData.Keep("RoleUtilisateur");
            return View(gestionUtilisateur.DbSetUtilisateurs.ToList());
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
                GestionFavori gestionFavori = new GestionFavori();
                List<Favori> listeFavorisUtilisateur = gestionFavori.ObtenirFavorisUtilisateur(utilisateur);
                foreach (Favori favori in listeFavorisUtilisateur)
                {
                    gestionFavori.SupprimerFavori(favori);
                }

                gestionUtilisateur.SupprimerUtilisateur(utilisateur);
                ViewBag.MessageConfirmation = "L'utilisateur " + pseudo + " a bien été supprimé.";
            }


            return View("LesUtilisateurs", gestionUtilisateur.DbSetUtilisateurs.ToList());
        }

        public IActionResult ModifierRoleUtilisateur(string pseudo)
        {
            TempData.Keep("PseudoUtilisateur");
            TempData.Keep("RoleUtilisateur");
            Utilisateur? utilisateur = gestionUtilisateur.TrouverUtilisateur(pseudo);

            if (utilisateur != null)
            {
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
