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

    }
}
