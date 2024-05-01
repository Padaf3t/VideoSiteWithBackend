using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ProjetCatalogue.Models;

namespace TestProjetCatalogue
{
    public class TestGestionFavori
    {
        private Utilisateur utilisateur;
        private Video video;
        private Favori favori;
        private GestionFavori gestion;

        [SetUp]
        public void BaseSetUp()
        {
            this.utilisateur = new Utilisateur("pseudoTest", "Mot_de_passe1!");
            this.video = new Video();
            this.favori = new Favori(video.IdVideo, utilisateur.Pseudo);
        }
        [TearDown]
        public void Dispose()
        {
            gestion.Dispose();
        }


        [Test]
        public void etantDonneGestionFavori_quandAppelConstructeurGestionFavori_alorsGestionFavoriCree()
        {
            this.gestion = new GestionFavori();
            Assert.That(this.gestion.ListeFavoris, Is.Not.Null);
        }


        [Test]
        public void etantFavoriCorrectEtGestionFavoriCorrect_quandAjouterFavori_alorsRetourneTrueEtFavoriAjoute()
        {
            this.gestion = new GestionFavori();

            Assert.That(this.gestion.AjouterFavori(utilisateur, video), Is.True);

            Assert.That(this.gestion.ListeFavoris.Last(), Is.EqualTo(favori));
        }

        [Test]
        public void etantFavoriAjouteAyantLeMemePseudoUserEtLeMemeIdVideo_quandAjouterFavori_alorsRetourneFalse()
        {
            this.gestion = new GestionFavori();

            this.gestion.AjouterFavori(utilisateur, video);

            Assert.That(this.gestion.AjouterFavori(utilisateur, video), Is.False);
        }



    }
}
