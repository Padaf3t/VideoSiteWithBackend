using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using ProjetCatalogue;

namespace TestProjetCatalogue
{
    internal class TestGestionUtilisateur
    {
        private Utilisateur utilisateur = new Utilisateur("TestGestionUtil", "abcd1234!");

        [Test]
        public void etantDonnerGestionUtilisateur_quandAppelConstructeurGestionUtilisateur_alorsGestionUtilisateurCreer()
        {
            GestionUtilisateur gestion = new GestionUtilisateur();
            Assert.That(gestion.ListeUtilisateurs, Is.Not.Null);
        }


        [Test]
        public void etantUtilisateurCorrectEtGestionUtilisateurCorrect_quandAjouterUtilisateur_alorsUtilisateurAjoute()
        {
            GestionUtilisateur gestion = new GestionUtilisateur();

            gestion.AjouterUtilisateur(utilisateur);

            Assert.That(gestion.ListeUtilisateurs.Last(), Is.EqualTo(utilisateur));
        }

        [Test]
        public void etantUtilisateurAjouterAyantLeMemePseudo_quandAjouterUtilisateur_alorsErreur()
        {
            GestionUtilisateur gestion = new GestionUtilisateur();

            gestion.AjouterUtilisateur(utilisateur);

            var erreur = Assert.Throws<ArgumentException>(
               delegate { gestion.AjouterUtilisateur(utilisateur); ; });

            Assert.That(erreur.Message, Is.EqualTo("L'utilisateur TestGestionUtil existe déjà"));
        }
    }
}
