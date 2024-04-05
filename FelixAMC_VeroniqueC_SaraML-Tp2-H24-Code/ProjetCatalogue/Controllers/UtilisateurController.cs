﻿using Microsoft.AspNetCore.Mvc;
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

        public IActionResult VideoSpecifique(int id)
        {
            ViewBag.PseudoUtilisateur = user.Pseudo;
            ViewBag.GestionFavori = gestionFavori;

            Video? video = catalogue.TrouverUneVideo(id);

            Utilisateur utilisateur = gestionUtilisateur.TrouverUtilisateur(ViewBag.PseudoUtilisateur);

            List<Favori> listeFavorisUser = ViewBag.GestionFavori.ObtenirFavorisUtilisateur(utilisateur);

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

        public IActionResult ResultatRetraitFavori()
        { 
            return View();
        }

        public IActionResult ResultatAjoutFavori()
        {
            return View();
        }

    }
}
