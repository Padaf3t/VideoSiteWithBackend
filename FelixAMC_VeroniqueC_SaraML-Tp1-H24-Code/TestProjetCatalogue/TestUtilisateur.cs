using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetCatalogue;

namespace TestProjetCatalogue
{
    internal class TestUtilisateur
    {
        private string pseudoBon = "TestUtilisateur";
        private string motDePasseBon = "abcd1234!";
        [Test]
        public void etantConstrucUilisateur_quandUtilisateurAvecBonneDonne_alorsUtilisateurAjouter()
        {
            
            Utilisateur utilisateur = new Utilisateur(pseudoBon, motDePasseBon);

        }

        [Test]
        public void etantConstrucUilisateur_quandUtilisateurAvecMauvaiseDonne_alorsUtilisateurPasAjouter()
        {
            string pseudoMauvais = "Bla";
            string motDePasseMauvais = "";
            Utilisateur utilisateur = new Utilisateur(pseudoMauvais, motDePasseMauvais);
        
        }
        [Test]
        public void etantAjouterFavori_quandUtilisateurAjouteFavori_alorsVideoAjouterDansListeFavori()
        {
            Utilisateur utilisateur = new Utilisateur(pseudoBon, motDePasseBon);
            Video video = new Video("test");
            utilisateur.AjouterFavori(video);
            Video videoFavori = utilisateur.ListeFavoris[0];

            Assert.That(video, Is.EqualTo(videoFavori) );
        }

        [Test]
        public void etanttAjouterEvaluation_quandUtilisateurAjouteEvaluation_alorsVideoAjouterDansListeEvaluation()
        {

        }
        [Test]
        public void etantGetHashCode_quandUtilisateurUtiliseGetHashCode_alorsRetournePseudoEnHashCode()
        {

        }
        [Test]
        public void etantEqual_quandPseudoPareil_alorsRetourneTrue()
        {

        }
        [Test]
        public void etantEqual_quandPseudoPasPareil_alorsRetourneFalse()
        {

        }

    }
}
