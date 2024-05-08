﻿using Microsoft.AspNetCore.Mvc;
using ProjetCatalogue.Models;
using System.Diagnostics;

namespace ProjetCatalogue.Controllers
{
    /// <summary>
    /// Contrôleur pour ce qui concerne les pages non connecté
    /// </summary>
    public class NonConnecteController : Controller
    {
        private readonly ILogger<NonConnecteController> _logger;
        GestionUtilisateur gestionUtilisateur;

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="logger"></param>
        public NonConnecteController(ILogger<NonConnecteController> logger)
        {
            _logger = logger;
            gestionUtilisateur = new GestionUtilisateur();
        }

        /// <summary>
        /// Action qui déclenche la vue Accueil; vide le TempData
        /// </summary>
        /// <returns>la vue Accueil</returns>
        public IActionResult Accueil()
        {
            TempData.Clear();
            return View();
        }

        /// <summary>
        /// Action qui déclenche la vue Accueil; traite via HTTPPost le contenu des formulaires de connection et d'inscription;
        /// utilise des méthodes de validation pour en vérifier le contenu; va connecter un utilisateur si formulaire de connection
        /// (boutonConnection), ou bien créer un utilisateur puis le connecter si formulaire de création d'un compte (boutonInscription).
        /// Va placer le pseudo utilisateur dans un TempData. Si la connection de l'utilisateur/admin se fait effectivement,
        /// Va retourner un RedirectAction sur la page d'accueil connectée respective (utilisateur ou admin, selon); sinon, va retourner
        /// la vue Accueil.
        /// </summary>
        /// <param name="idButton"></param>
        /// <returns>la vue Accueil, ou bien un RedirectAction sur la vue accueil utilisateur ou la vue accueil administrateur</returns>
        [HttpPost]
        public IActionResult ResultatFormulaireConnection(Utilisateur utilisateur)
        {

            if (!gestionUtilisateur.ValiderUtilisateur(utilisateur, out Utilisateur? utilisateurEnregistre, out string? messageErreur))
            {
                ViewData["pseudoConnection"] = utilisateur.Pseudo;
            }
            ViewBag.MessageErreurConnection = messageErreur;

            if (utilisateurEnregistre != null)
            {
                TempData["PseudoUtilisateur"] = utilisateurEnregistre.Pseudo;
                TempData.Keep("PseudoUtilisateur");
                if (utilisateurEnregistre.RoleUser == EnumRole.UtilisateurSimple)
                {
                    return RedirectToAction("TousLesMedias", "Utilisateur");
                }
                else if (utilisateurEnregistre.RoleUser == EnumRole.Admin)
                {
                    return RedirectToAction("LesUtilisateurs", "Administrateur");
                }
            }
            return View("Accueil");
        }

        /// <summary>
        /// Action qui déclenche la vue Accueil; traite via HTTPPost le contenu des formulaires de connection et d'inscription;
        /// utilise des méthodes de validation pour en vérifier le contenu; va connecter un utilisateur si formulaire de connection
        /// (boutonConnection), ou bien créer un utilisateur puis le connecter si formulaire de création d'un compte (boutonInscription).
        /// Va placer le pseudo utilisateur dans un TempData. Si la connection de l'utilisateur/admin se fait effectivement,
        /// Va retourner un RedirectAction sur la page d'accueil connectée respective (utilisateur ou admin, selon); sinon, va retourner
        /// la vue Accueil.
        /// </summary>
        /// <param name="idButton"></param>
        /// <returns>la vue Accueil, ou bien un RedirectAction sur la vue accueil utilisateur ou la vue accueil administrateur</returns>

        [HttpPost]
        public IActionResult ResultatFormulaireInscription(Utilisateur utilisateur)
        {

            if (utilisateur.Nom == null)
            {
                utilisateur.Nom = "";
            }
            if (utilisateur.Prenom == null)
            {
                utilisateur.Prenom = "";
            }

            if (gestionUtilisateur.CreationUtilisateur(utilisateur, out Utilisateur? utilisateurCree, out string? messageErreur))
            {
                if(!gestionUtilisateur.AjouterUtilisateur(utilisateurCree, out messageErreur))
                {
                    utilisateurCree = null;
                }
            }
            ViewData["pseudoInscription"] = utilisateur.Pseudo;
            ViewData["nomInscription"] = utilisateur.Nom;
            ViewData["prenomInscription"] = utilisateur.Prenom;
            ViewBag.MessageErreurInscription = messageErreur;

            if (utilisateurCree != null)
            {
                TempData["PseudoUtilisateur"] = utilisateurCree.Pseudo;
                TempData.Keep("PseudoUtilisateur");
                return RedirectToAction("TousLesMedias", "Utilisateur");
                
            }
            return View("Accueil");
        }

        /// <summary>
        /// Action utilisée si erreur ; retourne une vue d'erreur
        /// </summary>
        /// <returns>Une vue d'erreur</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}