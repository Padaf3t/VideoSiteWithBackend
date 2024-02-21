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
        [Test]
        public void etantConstrucUilisateur_quandUtilisateurAvecBonneDonne_alorsUtilisateurAjouter()
        {
            string pseudo = "First!";
            string motDePasse = "abc123!";
            Utilisateur utilisateur = new Utilisateur(pseudo, motDePasse);

        }

        [Test]
        public void etantConstrucUilisateur_quandUtilisateurAvecMauvaiseDonne_alorsUtilisateurPasAjouter()
        {
            string pseudo = "Bla";
            string motDePasse = "";
            Utilisateur utilisateur = new Utilisateur(pseudo, motDePasse);
        
        }
        [Test]
        public void etantAjouterFavori_quandUtilisateurAjouteFavori_alorsVideoAjouterDansListeFavori()
        {

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
