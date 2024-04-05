using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetCatalogue.Models;
using System.Linq;

namespace ProjetCatalogue.Controllers
{
    public class UtilisateurController : Controller
    {
        private Catalogue catalogue;
        private readonly ILogger<UtilisateurController> _logger;
        private GestionFavori gestionFavori;
        private GestionUtilisateur gestionUtilisateur;
        Utilisateur user;

        public UtilisateurController(ILogger<UtilisateurController> logger)
        {
            _logger = logger;
            catalogue = new Catalogue();
            catalogue.DeserisalisationJSONVideo(PathFinder.PathJsonVideo);
            gestionFavori = new GestionFavori();
            gestionFavori.DeserisalisationJSONFavoris(PathFinder.PathJsonFavori);

            //todo: temp
            gestionUtilisateur = new GestionUtilisateur();
            gestionUtilisateur.DeserialisationJSONUtilisateur(PathFinder.PathJsonUtilisateur);
            user = gestionUtilisateur.ListeUtilisateurs[1];

        }

        public IActionResult TousLesMedias()
        {
            return View(catalogue.ListeVideos);
        }
        public IActionResult MesFavoris()
        {
            List<Video> videoFavorite = catalogue.ObtenirListeVideoFavorites(gestionFavori.ListeFavoris);

            return View(videoFavorite);
        }

        public IActionResult VideoSpecifique(int id, bool? estAjouteFavori, string? pseudo)
        {
            ViewBag.PseudoUtilisateur = user.Pseudo;

            Video? video = catalogue.TrouverUneVideo(id);

            Utilisateur utilisateur = gestionUtilisateur.TrouverUtilisateur(ViewBag.PseudoUtilisateur);

            if (estAjouteFavori.HasValue && video != null)
            {
                if ((bool)estAjouteFavori)
                {
                    gestionFavori.AjouterFavori(utilisateur, video);
                }
                else
                {
                    gestionFavori.RetirerFavori(utilisateur, video);
                }
            }

            List<Favori> listeFavorisUser = gestionFavori.ObtenirFavorisUtilisateur(utilisateur);

            IEnumerable<Favori> query =
            from favoriTemp in listeFavorisUser
            where favoriTemp.IdVideo.Equals(id)
            select favoriTemp;

            ViewBag.EstFavori = false;
            if (query.ToList().Count > 0)
            {
                ViewBag.EstFavori = true;
            }

            return View(video);
        }

        public IActionResult ResultatRetraitFavori(int id)
        {
            Video? video = catalogue.TrouverUneVideo(id);

            Utilisateur utilisateur = gestionUtilisateur.TrouverUtilisateur(ViewBag.PseudoUtilisateur);

            if (video != null)
            {
                gestionFavori.RetirerFavori(utilisateur, video);
                ViewBag.EstFavori = false;
            }

            return View("VideSpecifique", video);
        }

        public IActionResult ResultatAjoutFavori(int id)
        {
            Video? video = catalogue.TrouverUneVideo(id);

            Utilisateur utilisateur = gestionUtilisateur.TrouverUtilisateur(ViewBag.PseudoUtilisateur);

            if (video != null)
            {
                gestionFavori.AjouterFavori(utilisateur, video);
                ViewBag.EstFavori = true;
            }
            
            return View("VideSpecifique", video);
        }

    }
}
