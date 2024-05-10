using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using NUnit.Framework;
using ProjetCatalogue.Models;

namespace TestProjetCatalogue
{
    public class TestGestionFavori
    {
        private Utilisateur utilisateur;
        private Video video;
        private GestionFavori gestion;
        private GestionUtilisateur gestionUtilisateur;
        private Catalogue catalogue;
        private GestionContext gestionContext;

        [OneTimeSetUp]
        public void BaseSetUp()
        {
            gestionContext = new GestionContext();
            this.utilisateur = new Utilisateur("pseudoTest", "Mot_de_passe1!");
            gestionUtilisateur = new GestionUtilisateur();
            gestionUtilisateur.AjouterUtilisateur(utilisateur, out String? message);
            
            this.video = new Video("Alloa",EnumAnimal.Autre,-1,DateTime.Now,1,"auteur","acteur", "","","");
            this.catalogue = new Catalogue();
            gestionContext.Add(this.video);
            gestionContext.SaveChanges();

            this.gestion = new GestionFavori();
        }

        [OneTimeTearDown]
        public void BaseTearDown()
        {
            GestionFavori gestionFavori = new GestionFavori();
                List<Favori> listeFavorisUtilisateur = gestionFavori.ObtenirFavorisUtilisateur(utilisateur);
                foreach (Favori favori in listeFavorisUtilisateur)
                {
                    gestionFavori.SupprimerFavori(favori);
                }
            gestionUtilisateur.SupprimerUtilisateur(utilisateur);
            gestionContext.Remove(video);
 ;
            gestionContext.Dispose();
        }

        [Test]
        public void etantFavoriCorrectEtGestionFavoriCorrect_quandAjouterFavori_alorsRetourneTrueEtFavoriAjoute()
        {
            Favori favori = new Favori(video.IdVideo, utilisateur.Pseudo);
            this.gestion.ModifierFavori(utilisateur, video);


            Assert.That(this.gestion.DbSetFavoris.ToList().Contains(favori), Is.True);
        }

        [Test]
        public void etantFavoriAjouteAyantLeMemePseudoUserEtLeMemeIdVideo_quandAjouterFavori_alorsRetourneFalse()
        {
            Favori favori = new Favori(video.IdVideo, utilisateur.Pseudo);
            this.gestion.ModifierFavori(utilisateur, video);
            this.gestion.ModifierFavori(utilisateur, video);

            Assert.That(this.gestion.DbSetFavoris.ToList().Contains(favori), Is.False);
           
        }



    }
}
