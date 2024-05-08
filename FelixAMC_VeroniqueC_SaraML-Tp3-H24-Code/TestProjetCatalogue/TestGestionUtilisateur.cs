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
        private Utilisateur? utilisateur;
        private GestionUtilisateur gestion;

        [SetUp]
        public void BaseSetup()
        {
            this.utilisateur = new Utilisateur("TestGestionUtil", "abcd1234!");
            this.gestion = new GestionUtilisateur();
        }
        [TearDown]
        public void BaseTearDown(){
            this.gestion.SupprimerUtilisateur(utilisateur);
            this.gestion.Dispose();
        }

        [Test]
        public void etantDonnerGestionUtilisateur_quandAppelConstructeurGestionUtilisateur_alorsGestionUtilisateurCreer()
        {
            Assert.That(this.gestion.ListeUtilisateurs, Is.Not.Null);
        }


        [Test]
        public void etantUtilisateurCorrectEtGestionUtilisateurCorrect_quandAjouterUtilisateur_alorsRetourneTrueEtUtilisateurAjoute()
        {
            string? message = null;
            gestion.SupprimerUtilisateur(this.utilisateur);
            Assert.That(this.gestion.AjouterUtilisateur(this.utilisateur, out message), Is.True);
            Assert.That(this.gestion.TrouverUtilisateur(this.utilisateur.Pseudo), Is.EqualTo(this.utilisateur));
        }

        [Test]
        public void etantUtilisateurAjouterAyantLeMemePseudo_quandAjouterUtilisateur_alorsRetourneFalse()
        {
            string? message = null;
            this.gestion.AjouterUtilisateur(this.utilisateur, out message);

            Assert.That(this.gestion.AjouterUtilisateur(this.utilisateur, out message), Is.False);
        }
    }
}
