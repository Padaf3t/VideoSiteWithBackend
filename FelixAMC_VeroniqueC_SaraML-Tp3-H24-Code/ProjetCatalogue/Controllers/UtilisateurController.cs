using Microsoft.AspNetCore.Mvc;
using ProjetCatalogue.Models;

namespace ProjetCatalogue.Controllers
{
    /// <summary>
    /// Contrôleur pour ce qui concerne les pages Utilisateur connecté
    /// </summary>
    public class UtilisateurController : Controller
    {
        private readonly Catalogue catalogue;
        private readonly ILogger<UtilisateurController> _logger;
        private GestionFavori gestionFavori;
        private GestionUtilisateur gestionUtilisateur;

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="logger"></param>
        public UtilisateurController(ILogger<UtilisateurController> logger)
        {
            _logger = logger;
            catalogue = new Catalogue();
            gestionFavori = new GestionFavori();
            gestionUtilisateur = new GestionUtilisateur();
        }

        /// <summary>
        /// Action qui déclenche la vue TousLesMedias (accueil lorsque connecté Utilisateur)
        /// </summary>
        /// <returns>un IActionResult : soit un RedirectToAction vers l'accueil non connecté si pas d'utilisateur trouvé;
        /// soit la vue TousLesMedias si utilisateur légitime</returns>
        public IActionResult TousLesMedias()
        {
            TempData.Keep("PseudoUtilisateur");

            Utilisateur? utilisateur = gestionUtilisateur.TrouverUtilisateur(TempData["PseudoUtilisateur"] as string);

            //vérifie si utiliseur n'est pas simple; redirige vers accueil non conntecté
            if (utilisateur == null || utilisateur.RoleUser != EnumRole.UtilisateurSimple)
            {
                return RedirectToAction("Accueil", "NonConnecte");
            }
            
            //création d'une liste de listes d'objets pour pouvoir conserver chaque vidéo du catalogue et si elle est un favori de
            //  l'utilisateur actuellement connecté
            List<List<Object>> listeVideosIncluantSiFavori = new List<List<Object>>();
            List<Video> listeTemp = catalogue.DbSetVideos.ToList();

            for (int i = 0; i < listeTemp.Count ; i++)
            {
                //verification si video est favorite
                bool estFavori = gestionFavori.FavoriPresent(utilisateur, listeTemp[i]);
                listeVideosIncluantSiFavori.Add(new List<Object> {listeTemp[i], estFavori});
            }

            return View(listeVideosIncluantSiFavori);
        }

        /// <summary>
        /// Action qui déclenche la vue MesFavoris
        /// </summary>
        /// <returns>un IActionResult : soit un RedirectToAction vers l'accueil non connecté si pas d'utilisateur trouvé;
        /// soit la vue MesFavoris si utilisateur légitime</returns>
        public IActionResult MesFavoris()
        {
            TempData.Keep("PseudoUtilisateur");
            Utilisateur? utilisateur = gestionUtilisateur.TrouverUtilisateur(TempData["PseudoUtilisateur"] as string);

            //vérifie si utiliseur n'est pas simple; redirige vers accueil non conntecté
            if (utilisateur == null || utilisateur.RoleUser != EnumRole.UtilisateurSimple)
            {
                return RedirectToAction("Accueil", "NonConnecte");
            }

            List<Favori>? listeFavoriUtilisateur = gestionFavori.ObtenirFavorisUtilisateur(utilisateur);

            //obtenir les vidéos à partir des favoris de l'utilisateur
            List<Video> videosFavorites = catalogue.ObtenirListeVideoFavorites(listeFavoriUtilisateur);

            //création d'une liste de listes d'objets qui conserve les vidéos qui sont des favoris pour l'utilisateur
            //connecté et un booleen false qui servira à ne pas afficher l'icône signalant que vidéo est favori
            //pour éviter surcharge du visuel
            List<List<Object>> listeVideosFavorites = new List<List<Object>>();

            for (int i = 0; i < videosFavorites.Count; i++)
            {
                listeVideosFavorites.Add(new List<Object> {videosFavorites[i], false});
            }

            return View(listeVideosFavorites);
        }

        /// <summary>
        /// Action qui déclenche la vue VideoSpecifique
        /// </summary>
        /// <param name="id">L'id de la vidéo à utiliser</param>
        /// <param name="favoriEstModifie">booléen, s'il y a lieu, qui dit si on modifie un favori ou non</param>
        /// <returns>un IActionResult : soit un RedirectToAction vers l'accueil non connecté si pas d'utilisateur trouvé;
        /// soit la vue VideoSpecifique si utilisateur légitime</returns>
        public IActionResult VideoSpecifique(int id, bool? favoriEstModifie)
        {
            TempData.Keep("PseudoUtilisateur");
            ViewBag.EstFavori = false;

            Video? video = catalogue.TrouverUneVideo(id);
            Utilisateur? utilisateur = gestionUtilisateur.TrouverUtilisateur(TempData["PseudoUtilisateur"] as string);
            
            //verification si l'utilisateur n'est pas simple; redirige vers accueil non connecté le cas échéant
            if (utilisateur == null || utilisateur.RoleUser != EnumRole.UtilisateurSimple)
            {
                return RedirectToAction("Accueil", "NonConnecte");
            }

            if (video != null && utilisateur != null)
            {
                //verification si favori a été modifié dans la vue
                if (favoriEstModifie.HasValue)
                {
                    gestionFavori.ModifierFavori(utilisateur, video);
                }
                
                bool estFavori = false;

                if (gestionFavori.FavoriPresent(utilisateur, video))
                {
                    ViewBag.EstFavori = true;
                    estFavori = true;
                }

                //si favori est modifié: message de confirmation selon le cas
                if (favoriEstModifie.HasValue)
                {
                    if ((bool)favoriEstModifie && estFavori)
                    {
                        ViewBag.MessageConfirmation = "Le favori a bien été ajouté";
                    }
                    else if ((bool)favoriEstModifie && !estFavori)
                    {
                        ViewBag.MessageConfirmation = "Le favori a bien été retiré";
                    }
                    else
                    {
                        ViewBag.MessageConfirmation = "Le favori n'a pas pu être modifié en raison d'une erreur inattendue";
                    }
                }
            }

            return View(video);
        }
     
    }
}
