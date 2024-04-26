using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetCatalogue.Models;
//using System.IO.IsolatedStorage;
//using System.Linq;

namespace ProjetCatalogue.Controllers
{
    /// <summary>
    /// Contrôleur pour ce qui concerne les pages Utilisateur connecté
    /// </summary>
    public class UtilisateurController : Controller
    {
        private Catalogue catalogue;
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
        /// Action qui déclenche la vue TousLesMedias (accueil lorsque connecté Utilisateur) - sauf si le pseudo utilisateur
        /// ne correspond à aucun utilisateur, va RedirectToAction vers action Accueil dans le contrôleur NonConnecte.
        /// Si utilissateur bien présent, va aller chercher la liste des vidéos du catalogue, ainsi que la liste des favoris
        /// de l'utilisateur pour les passer à la vue TousLesMedias
        /// </summary>
        /// <returns>un IActionResult : soit un RedirectToAction vers l'accueil non connecté si pas d'utilisateur trouvé;
        /// soit la vue TousLesMedias si utilisateur légitime</returns>
        public IActionResult TousLesMedias()
        {
            TempData.Keep("PseudoUtilisateur");
            Utilisateur? utilisateur = gestionUtilisateur.TrouverUtilisateur(TempData["PseudoUtilisateur"] as string);
            if (utilisateur == null || utilisateur.RoleUser != EnumRole.UtilisateurSimple)
            {
                return RedirectToAction("Accueil", "NonConnecte");
            }

            List<List<Object>> listeVideosIncluantSiFavori = new List<List<Object>>();
            
            for (int i = 0; i < catalogue.ListeVideos.Count ; i++)
            {
                //verification si video est favorite
                bool estFavori = gestionFavori.FavoriPresent(utilisateur, catalogue.ListeVideos[i]);

                listeVideosIncluantSiFavori.Add(new List<Object> {catalogue.ListeVideos[i], estFavori});
            }

            //Ici le best serait éventuellement de faire un objet ayant un video et un bool favori en attributs
            return View(listeVideosIncluantSiFavori);
        }

        /// <summary>
        /// Action qui déclenche la vue MesFavoris en lui passant la liste des videos favorites de l'utilisateur connecté -
        /// sauf si le pseudo utilisateur ne correspond à aucun utilisateur, va RedirectToAction vers action Accueil dans le
        /// contrôleur NonConnecte. Si utilissateur bien présent, va aller chercher la liste des favoris de l'utilisateur pour
        /// la passer à la vue MesFavoris
        /// </summary>
        /// <returns>un IActionResult : soit un RedirectToAction vers l'accueil non connecté si pas d'utilisateur trouvé;
        /// soit la vue MesFavoris si utilisateur légitime</returns>
        public IActionResult MesFavoris()
        {
            TempData.Keep("PseudoUtilisateur");
            Utilisateur? utilisateur = gestionUtilisateur.TrouverUtilisateur(TempData["PseudoUtilisateur"] as string);
            if (utilisateur == null || utilisateur.RoleUser != EnumRole.UtilisateurSimple)
            {
                return RedirectToAction("Accueil", "NonConnecte");
            }

            List<Favori>? listeFavoriUtilisateur = gestionFavori.ObtenirFavorisUtilisateur(utilisateur);

            List<Video> videosFavorites = catalogue.ObtenirListeVideoFavorites(listeFavoriUtilisateur);

            List<List<Object>> listeVideosFavorites = new List<List<Object>>();

            for (int i = 0; i < videosFavorites.Count; i++)
            {
                listeVideosFavorites.Add(new List<Object> {videosFavorites[i], false});
            }

            return View(listeVideosFavorites);

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
        public IActionResult VideoSpecifique(int id, bool? favoriEstModifie)
        {
            TempData.Keep("PseudoUtilisateur");
            ViewBag.EstFavori = false;

            Video? video = catalogue.TrouverUneVideo(id);
            Utilisateur? utilisateur = gestionUtilisateur.TrouverUtilisateur(TempData["PseudoUtilisateur"] as string);
            
            //verification que l'utilisateur est un utilisateur simple
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
                    gestionFavori.SerialisationFavoris(PathFinder.PathJsonFavori);
                }
                
                bool estFavori = false;

                if (gestionFavori.FavoriPresent(utilisateur, video))
                {
                    ViewBag.EstFavori = true;
                    estFavori = true;
                }


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
