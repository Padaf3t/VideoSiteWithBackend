using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using ProjetCatalogue.Models;

namespace TestProjetCatalogue
{
    internal class TestGestionUtilisateur
    {
        private Utilisateur utilisateur;
        private GestionUtilisateur gestion;

        [SetUp]
        public void BaseSetup()
        {
            this.utilisateur = new Utilisateur("TestGestionUtil", "abcd1234!");
            this.gestion = new GestionUtilisateur();
        }

        [Test]
        public void etantDonnerGestionUtilisateur_quandAppelConstructeurGestionUtilisateur_alorsGestionUtilisateurCreer()
        {
            Assert.That(this.gestion.ListeUtilisateurs, Is.Not.Null);
        }


        [Test]
        public void etantUtilisateurCorrectEtGestionUtilisateurCorrect_quandAjouterUtilisateur_alorsRetourneTrueEtUtilisateurAjoute()
        {

            Assert.That(this.gestion.AjouterUtilisateur(this.utilisateur), Is.True);
            Assert.That(this.gestion.ListeUtilisateurs.Last(), Is.EqualTo(this.utilisateur));
        }

        [Test]
        public void etantUtilisateurAjouterAyantLeMemePseudo_quandAjouterUtilisateur_alorsRetourneFalse()
        {

            this.gestion.AjouterUtilisateur(this.utilisateur);

            Assert.That(this.gestion.AjouterUtilisateur(this.utilisateur), Is.False);
        }
    }
}
