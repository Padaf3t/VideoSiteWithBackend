﻿using System;
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
        private string pseudoBon = "Test_Utilisateur";
        private string motDePasseBon = "abcd1234!";
        [Test]
        public void etantDonneConstructUtilisateurAvecBonneDonne_quandCreerUtilisateur_alorsUtilisateurAjouter()
        {
            Utilisateur utilisateur = new Utilisateur(pseudoBon, motDePasseBon);

            Assert.That(utilisateur, Is.EqualTo(new Utilisateur(pseudoBon, motDePasseBon)));

        }

        [Test]
        public void etantDonneConstructUtilisateurAvecPseudoTropCourt_quandCreerUtilisateur_alorsErreur()
        {
            string pseudoTropCourt = "Bla";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(pseudoTropCourt, motDePasseBon); }) ;

            Assert.That(erreur.Message, Is.EqualTo("Le pseudo Bla est trop court"));
        }
        [Test]
        public void etantDonneConstructUtilisateurAvecPseudoTropLong_quandCreerUtilisateur_alorsErreur()
        {
            string pseudoTropLong = "Blablablablablablablalblbblablablablablablablabla";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(pseudoTropLong, motDePasseBon); });
            Assert.That(erreur.Message, Is.EqualTo("Le pseudo Blablablablablablablalblbblablablablablablablabla est trop long"));
        }
        [Test]
        public void etantDonneConstructUtilisateurAvecPseudoCharSpecial_quandCreerUtilisateur_alorsErreur()
        {
            string pseudoCharSpecial = "testChar!/";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(pseudoCharSpecial, motDePasseBon); });
            Assert.That(erreur.Message, Is.EqualTo("Le pseudo testChar!/ contient un caractère spécial"));
        }
        [Test]
        public void etantDonneConstructUtilisateurAvecMotDePasseTropCourt_quandCreerUtilisateur_alorsErreur()
        {
            string motDePasseCourt = "court";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(pseudoBon, motDePasseCourt); });
            Assert.That(erreur.Message, Is.EqualTo("Le mot de passe est trop court"));
        }
        [Test]
        public void etantDonneConstructUtilisateurAvecMotDePasseSansChiffre_quandCreerUtilisateur_alorsErreur()
        {
            string motDePasseCourt = "SansChiffre";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(pseudoBon, motDePasseCourt); });
            Assert.That(erreur.Message, Is.EqualTo("Le mot de passe doit contenir au moins un chiffre"));
        }
        [Test]
        public void etantDonneConstructUtilisateurAvecMotDePasseSansCharSpecial_quandCreerUtilisateur_alorsErreur()
        {
            string motDePasseCourt = "SansCharSpecial1";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(pseudoBon, motDePasseCourt); });
            Assert.That(erreur.Message, Is.EqualTo("Le mot de passe doit contenir au moins un charactère spécial"));
        }
    }
}
