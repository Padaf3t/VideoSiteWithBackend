using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetCatalogue;

namespace TestProjetCatalogue
{
    public class TestGestionFavori
    {
        private GestionFavori gestion;
        private Utilisateur utilisateur;
        private Video video;
        private Favori favori;

        [SetUp]
        public void BaseSetUp()
        {
            this.utilisateur = new Utilisateur("pseudoTest", "Mot_de_passe1!");
            this.video = new Video();
            this.favori = new Favori(video.IdVideo, utilisateur.Pseudo);
        }


        [Test]
        public void etantDonneGestionFavori_quandAppelConstructeurGestionFavori_alorsGestionFavoriCree()
        {
            this.gestion = new GestionFavori();
            Assert.That(gestion.ListeFavoris, Is.Not.Null);
        }


        [Test]
        public void etantFavoriCorrectEtGestionFavoriCorrect_quandAjouterFavori_alorsFavoriAjoute()
        {
            this.gestion = new GestionFavori();

            gestion.AjouterFavori(utilisateur, video);

            Assert.That(gestion.ListeFavoris.Last(), Is.EqualTo(favori));
        }

        [Test]
        public void etantFavoriAjouteAyantLeMemePseudoUserEtLeMemeIdVideo_quandAjouterFavori_alorsErreur()
        {
            this.gestion = new GestionFavori();

            gestion.AjouterFavori(utilisateur, video);

            Assert.That(gestion.AjouterFavori(utilisateur, video), Is.EqualTo(false));
        }



    }
}
