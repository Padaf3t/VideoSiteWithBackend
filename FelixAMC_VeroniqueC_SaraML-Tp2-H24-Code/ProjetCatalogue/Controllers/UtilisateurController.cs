using Microsoft.AspNetCore.Mvc;
using ProjetCatalogue.Models;

namespace ProjetCatalogue.Controllers
{
    public class UtilisateurController : Controller
    {
        private Catalogue catalogue;
        private readonly ILogger<UtilisateurController> _logger;

        public UtilisateurController(ILogger<UtilisateurController> logger)
        {
            _logger = logger;
            catalogue = new Catalogue();
            catalogue.DeserisalisationJSONVideo(PathFinder.PathJsonVideo);

        }

        public IActionResult Acceuil(Utilisateur utilisateur)
        {
            ViewBag.Utilisateur = utilisateur;
            return View();
        }
        public IActionResult TousLesMedias()
        {
            return View(catalogue.ListeVideos);
        }
        public IActionResult MesFavoris()
        {
            GestionFavori gestionFavori = new GestionFavori();
            gestionFavori.DeserisalisationJSONFavoris(PathFinder.PathJsonFavori);

            List<Video> videoFavorite = catalogue.ObtenirListeVideoFavoris(gestionFavori.ListeFavoris);

            return View(videoFavorite);
        }

    }
}
