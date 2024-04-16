using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ProjetCatalogue.Models;

namespace TestProjetCatalogue
{
    internal class TestUtilisateur
    {
        private string pseudoBon;
        private string motDePasseBon;

        [SetUp]
        public void BaseSetup()
        {
            this.pseudoBon = "Test_Utilisateur";
            this.motDePasseBon = "abcd1234!";
        }
        [Test]
        public void etantDonneConstructeurUtilisateurAvecBonneDonne_quandCreerUtilisateur_alorsUtilisateurAjouter()
        {
            Utilisateur utilisateur = new Utilisateur(this.pseudoBon, this.motDePasseBon);

            Assert.That(utilisateur, Is.EqualTo(new Utilisateur(this.pseudoBon, this.motDePasseBon)));

        }

        [Test]
        public void etantDonneConstructeurUtilisateurAvecPseudoTropCourt_quandCreerUtilisateur_alorsErreur()
        {
            string pseudoTropCourt = "Blab";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(pseudoTropCourt, this.motDePasseBon); }) ;

            Assert.That(erreur.Message, Is.EqualTo("Le pseudo doit être entre 5 et 20 caractères inclusivement et ne doit pas contenir de caractère spécial. "));
        }
        [Test]
        public void etantDonneConstructeurUtilisateurAvecPseudoTropLong_quandCreerUtilisateur_alorsErreur()
        {
            string pseudoTropLong = "mhbmxybsrvwvsnvzilcrazna";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(pseudoTropLong, this.motDePasseBon); });
            Assert.That(erreur.Message, Is.EqualTo("Le pseudo doit être entre 5 et 20 caractères inclusivement et ne doit pas contenir de caractère spécial. "));
        }
        [Test]
        public void etantDonneConstructeurUtilisateurAvecPseudoCharSpecial_quandCreerUtilisateur_alorsErreur()
        {
            string pseudoCharSpecial = "testChar!/";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(pseudoCharSpecial, this.motDePasseBon); });
            Assert.That(erreur.Message, Is.EqualTo("Le pseudo doit être entre 5 et 20 caractères inclusivement et ne doit pas contenir de caractère spécial. "));
        }

        [Test]
        public void etantDonneConstructeurUtilisateurAvecPseudoNull_quandCreerUtilisateur_alorsErreur()
        {
            string pseudoNull = null;

            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(pseudoNull, this.motDePasseBon); });
            Assert.That(erreur.Message, Is.EqualTo("Le pseudo doit être entre 5 et 20 caractères inclusivement et ne doit pas contenir de caractère spécial. "));
        }

        [Test]
        [TestCase("c0urt!?")]
        [TestCase("nhcb50x08DAYxHqaUZJd7OP2P2LyWOz4m6WmMP2R0rvdnUSw1O6KTry1MMHTnBxbzBx!")]
        [TestCase("")]
        public void etantDonneConstructeurUtilisateurAvecMotDePasseTropCourtOuTropLong_quandCreerUtilisateur_alorsErreur(string motDePasseMauvaiseLongueur)
        {
            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(this.pseudoBon, motDePasseMauvaiseLongueur); });
            Assert.That(erreur.Message, Is.EqualTo("Le mot de passe doit avoir une longueur de 8 à 60 caractères inclusivement, contenir un chiffre et un caractère spécial. "));
        }
        [Test]
        public void etantDonneConstructeurUtilisateurAvecMotDePasseSansChiffre_quandCreerUtilisateur_alorsErreur()
        {
            string motDePasseSansChiffre = "SansChiffre!";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(this.pseudoBon, motDePasseSansChiffre); });
            Assert.That(erreur.Message, Is.EqualTo("Le mot de passe doit avoir une longueur de 8 à 60 caractères inclusivement, contenir un chiffre et un caractère spécial. "));
        }
        [Test]
        public void etantDonneConstructeurUtilisateurAvecMotDePasseSansCharSpecial_quandCreerUtilisateur_alorsErreur()
        {
            string motDePasseCharSpecial = "SansCharSpecial1";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(this.pseudoBon, motDePasseCharSpecial); });
            Assert.That(erreur.Message, Is.EqualTo("Le mot de passe doit avoir une longueur de 8 à 60 caractères inclusivement, contenir un chiffre et un caractère spécial. "));
        }

        [Test]
        public void etantDonneConstructeurUtilisateurAvecMotDePasseNull_quandCreerUtilisateur_alorsErreur()
        {
            string motDePasseNull = null;

            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(this.pseudoBon, motDePasseNull); });
            Assert.That(erreur.Message, Is.EqualTo("Le mot de passe doit avoir une longueur de 8 à 60 caractères inclusivement, contenir un chiffre et un caractère spécial. "));
        }

        [Test]
        public void etantDonneConstructeurUtilisateurAvecPrenomLong_quandCreerUtilisateur_alorsErreur()
        {
            string prenomlong = "prenomlongprenomlongprenomlongprenomlongprenomlongprenomlongprenomlong";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(this.pseudoBon, motDePasseBon, "allo", prenomlong, EnumRole.UtilisateurSimple); });
            Assert.That(erreur.Message, Is.EqualTo("Le prénom doit avoir 50 caractère ou moins. Il doit être composer de caractère alphanumérique ou '-'. "));
        }

        [Test]
        public void etantDonneConstructeurUtilisateurAvecNomLong_quandCreerUtilisateur_alorsErreur()
        {
            string nomlong = "nomlongnomlongnomlongnomlongnomlongnomlongnomlongnomlongnomlongnomlongnomlong";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(this.pseudoBon, motDePasseBon, nomlong, "allo", EnumRole.UtilisateurSimple); });
            Assert.That(erreur.Message, Is.EqualTo("Le nom doit avoir 50 caractère ou moins. Il doit être composer de caractère alphanumérique ou '-'. "));
        }

        [Test]
        [TestCase("!allo")]
        [TestCase("allo*")]
        [TestCase("al lo")]
        [TestCase("&allo&")]
        [TestCase("%%%%%%")]
        [TestCase("%%44444")]
        [TestCase("%%%eee666")]
        [TestCase("/|\\%%eee66$6")]
        public void etantDonneConstructeurUtilisateurAvecNomAvecCaracteresSpeciaux_quandCreerUtilisateur_alorsErreur(string nom)
        {
            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(this.pseudoBon, motDePasseBon, nom, "allo", EnumRole.UtilisateurSimple); });
            Assert.That(erreur.Message, Is.EqualTo("Le nom doit avoir 50 caractère ou moins. Il doit être composer de caractère alphanumérique ou '-'. "));
        }

        [Test]
        [TestCase("!allo")]
        [TestCase("allo*")]
        [TestCase("al lo")]
        [TestCase("&allo&")]
        [TestCase("%%%%%%")]
        [TestCase("%%44444")]
        [TestCase("%%%eee666")]
        [TestCase("/|\\%%eee66$6")]
        public void etantDonneConstructeurUtilisateurAvecPrenomAvecCaracteresSpeciaux_quandCreerUtilisateur_alorsErreur(string prenom)
        {
            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(this.pseudoBon, motDePasseBon, "allo", prenom, EnumRole.UtilisateurSimple); });
            Assert.That(erreur.Message, Is.EqualTo("Le prénom doit avoir 50 caractère ou moins. Il doit être composer de caractère alphanumérique ou '-'. "));
        }
    }

}
