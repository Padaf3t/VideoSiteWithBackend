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
        private Favori favori;
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

            this.favori = new Favori(video.IdVideo, utilisateur.Pseudo);
            this.gestion = new GestionFavori();
        }

        [OneTimeTearDown]
        public void BaseTearDown()
        {
            gestionUtilisateur.SupprimerUtilisateur(utilisateur);
            gestionContext.Remove(video);
 ;
            gestionContext.Dispose();
            gestion.SupprimerFavori(favori);
        }


        [Test]
        public void etantDonneGestionFavori_quandAppelConstructeurGestionFavori_alorsGestionFavoriCree()
        {
            Assert.That(this.gestion.DbSetFavoris, Is.Not.Null);
        }


        [Test]
        public void etantFavoriCorrectEtGestionFavoriCorrect_quandAjouterFavori_alorsRetourneTrueEtFavoriAjoute()
        {
            this.gestion.ModifierFavori(utilisateur, video);
            //Assert.That(this.gestion.Favoris.Last(), Is.EqualTo(favori));

            Assert.That(this.gestion.DbSetFavoris.Contains(favori), Is.True);
        }

        [Test]
        public void etantFavoriAjouteAyantLeMemePseudoUserEtLeMemeIdVideo_quandAjouterFavori_alorsRetourneFalse()
        {
            this.gestion.ModifierFavori(utilisateur, video);

            Assert.That(this.gestion.DbSetFavoris.Contains(favori), Is.False);
           
        }



    }
}
