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

        public UtilisateurController(ILogger<UtilisateurController> logger)
        {
            
            _logger = logger;
            catalogue = new Catalogue();
            catalogue.DeserisalisationJSONVideo(PathFinder.PathJsonVideo);
            gestionFavori = new GestionFavori();
            gestionFavori.DeserisalisationJSONFavoris(PathFinder.PathJsonFavori);
            gestionUtilisateur = new GestionUtilisateur();
            gestionUtilisateur.DeserialisationJSONUtilisateur(PathFinder.PathJsonUtilisateur);
        }

        public IActionResult TousLesMedias()
        {
            TempData.Keep("PseudoUtilisateur");
            Utilisateur? utilisateur = gestionUtilisateur.TrouverUtilisateur(TempData["PseudoUtilisateur"] as string);
            if (utilisateur == null || utilisateur.RoleUser != EnumRole.UtilisateurSimple)
            {
                return RedirectToAction("Accueil", "NonConnecte");
            }
            return View(catalogue.ListeVideos);
        }
        public IActionResult MesFavoris()
        {
            Utilisateur? utilisateur = gestionUtilisateur.TrouverUtilisateur(TempData["PseudoUtilisateur"] as string);
            if (utilisateur == null || utilisateur.RoleUser != EnumRole.UtilisateurSimple)
            {
                return RedirectToAction("Accueil", "NonConnecte");
            }
            TempData.Keep("PseudoUtilisateur");
            
            List<Favori>? listeFavoriUtilisateur = gestionFavori.ObtenirFavorisUtilisateur(gestionUtilisateur.TrouverUtilisateur(TempData["PseudoUtilisateur"] as string));

            List<Video> videoFavorite = catalogue.ObtenirListeVideoFavorites(listeFavoriUtilisateur);

            return View(videoFavorite);
        }

        public IActionResult VideoSpecifique(int id, bool? favoriEstModifie)
        {
            TempData.Keep("PseudoUtilisateur");
            ViewBag.EstFavori = false;

            Video? video = catalogue.TrouverUneVideo(id);
            Utilisateur? utilisateur = gestionUtilisateur.TrouverUtilisateur(TempData["PseudoUtilisateur"] as string);
            if (utilisateur == null || utilisateur.RoleUser != EnumRole.UtilisateurSimple)
            {
                return RedirectToAction("Accueil", "NonConnecte");
            }

            if (video != null && utilisateur != null)
            {
                if (favoriEstModifie.HasValue)
                {
                    gestionFavori.ModifierFavori(utilisateur, video);
                    gestionFavori.SerialisationFavoris(PathFinder.PathJsonFavori);
                }
                
                if (gestionFavori.FavoriPresent(utilisateur, video))
                {
                    ViewBag.EstFavori = true;
                }

            }

            return View(video);
        }
     
    }
}
