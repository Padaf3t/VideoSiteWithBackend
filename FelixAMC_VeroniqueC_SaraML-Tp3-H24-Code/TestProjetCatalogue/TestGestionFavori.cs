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

        [OneTimeSetUp]
        public void BaseSetUp()
        {
            this.utilisateur = new Utilisateur("pseudoTest", "Mot_de_passe1!");
            gestionUtilisateur = new GestionUtilisateur();
            gestionUtilisateur.AjouterUtilisateur(utilisateur, out String? message);
            
            this.video = new Video(99999,"Alloa");
            this.catalogue = new Catalogue();
            this.catalogue.Add(new Video());
            this.catalogue.SaveChanges();

            this.favori = new Favori(video.IdVideo, utilisateur.Pseudo);
            this.gestion = new GestionFavori();
        }

        [OneTimeTearDown]
        public void BaseTearDown()
        {
            gestionUtilisateur.SupprimerUtilisateur(utilisateur);
            catalogue.Remove(video);
            

            gestionUtilisateur.Dispose();
            catalogue.Dispose();
            gestion.Dispose();

        }


        [Test]
        public void etantDonneGestionFavori_quandAppelConstructeurGestionFavori_alorsGestionFavoriCree()
        {
            Assert.That(this.gestion.Favoris, Is.Not.Null);
        }


        [Test]
        public void etantFavoriCorrectEtGestionFavoriCorrect_quandAjouterFavori_alorsRetourneTrueEtFavoriAjoute()
        {
            Assert.That(this.gestion.AjouterFavori(utilisateur, video), Is.True);

            //Assert.That(this.gestion.Favoris.Last(), Is.EqualTo(favori));

            Assert.That(this.gestion.Favoris.Contains(favori));
        }

        [Test]
        public void etantFavoriAjouteAyantLeMemePseudoUserEtLeMemeIdVideo_quandAjouterFavori_alorsRetourneFalse()
        {
            this.gestion.AjouterFavori(utilisateur, video);

            Assert.That(this.gestion.AjouterFavori(utilisateur, video), Is.False);
            gestion.SupprimerFavori(favori);
        }



    }
}
