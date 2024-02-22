using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProjetCatalogue;

namespace TestProjetCatalogue
{
    internal class TestUtilisateur
    {
        private string pseudoBon = "TestUtilisateur";
        private string motDePasseBon = "abcd1234!";
        private string pseudoMauvais = "Bla";
        private string motDePasseMauvais = "";
        [Test]
        public void etantConstrucUilisateur_quandUtilisateurAvecBonneDonne_alorsUtilisateurAjouter()
        {
            
            Utilisateur utilisateur = new Utilisateur(pseudoBon, motDePasseBon);
            

        }

        [Test]
        public void etantConstrucUilisateur_quandUtilisateurAvecPseudoTropCour_alorsUtilisateurPasAjouter()
        {
            
            
            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(pseudoMauvais, motDePasseBon); }) ;
            Assert.That(erreur.Message, Is.EqualTo());
        }
        [Test]
        public void etantConstrucUilisateur_quandUtilisateurAvecPseudoTropLong_alorsUtilisateurPasAjouter()
        {


            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(pseudoMauvais, motDePasseBon); });
            Assert.That(erreur.Message, Is.EqualTo());
        }
        [Test]
        public void etantConstrucUilisateur_quandUtilisateurAvecPseudoCaractereSpecial_alorsUtilisateurPasAjouter()
        {


            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(pseudoMauvais, motDePasseBon); });
            Assert.That(erreur.Message, Is.EqualTo());
        }
    }
}
