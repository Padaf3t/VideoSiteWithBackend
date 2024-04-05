using AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public IActionResult TousLesMedias()
        {
            return View(catalogue.ListeVideos);
        }
        public IActionResult MesFavoris()
        {
            GestionFavori gestionFavori = new GestionFavori();
            gestionFavori.DeserisalisationJSONFavoris(PathFinder.PathJsonFavori);

            List<Video> videoFavorite = catalogue.ObtenirListeVideoFavorites(gestionFavori.ListeFavoris);

            return View(videoFavorite);
        }

        public IActionResult VideoSpecifique(int id)
        {
            Video? video = catalogue.TrouverUneVideo(id);
            return View(video);
        }

    }
}
