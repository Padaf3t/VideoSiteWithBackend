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
            this.gestion = new GestionFavori();
        }
        [TearDown]
        public void Dispose()
        {
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
        }



    }
}
