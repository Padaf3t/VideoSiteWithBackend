using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjetCatalogue.Models;
using System.IO.IsolatedStorage;
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

            string pseudo = (string)TempData["PseudoUtilisateur"];

            Utilisateur user = gestionUtilisateur.TrouverUtilisateur(pseudo);

            List<List<Object>> listeVideosIncluantSiFavori = new List<List<Object>>();
            
            for (int i = 0; i < catalogue.ListeVideos.Count ; i++)
            {
                List<Object> videoEtSiFavori = new List<object>();

                videoEtSiFavori.Add(catalogue.ListeVideos[i]);

                bool estFavori = false;

                if (user != null)
                {
                    estFavori = gestionFavori.FavoriPresent(user, catalogue.ListeVideos[i]);
                }


                videoEtSiFavori.Add(estFavori);

                listeVideosIncluantSiFavori.Add(videoEtSiFavori);
            }

            //todo: ici le best serait éventuellement de faire un objet ayant un video et un bool favori en attributs
            return View(listeVideosIncluantSiFavori);
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

            List<Video> videosFavorites = catalogue.ObtenirListeVideoFavorites(listeFavoriUtilisateur);

            List<List<Object>> listeVideosFavorites = new List<List<Object>>();

            for (int i = 0; i < videosFavorites.Count; i++)
            {
                List<Object> videoEtSiFavori = new List<object>();

                videoEtSiFavori.Add(videosFavorites[i]);

                bool estFavori = false;

                videoEtSiFavori.Add(estFavori);

                listeVideosFavorites.Add(videoEtSiFavori);


            }

            return View(listeVideosFavorites);

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
