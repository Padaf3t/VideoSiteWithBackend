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
            string pseudoTropCourt = "Bla";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(pseudoTropCourt, this.motDePasseBon); }) ;

            Assert.That(erreur.Message, Is.EqualTo("Le pseudo " + pseudoTropCourt + " est trop court"));
        }
        [Test]
        public void etantDonneConstructeurUtilisateurAvecPseudoTropLong_quandCreerUtilisateur_alorsErreur()
        {
            string pseudoTropLong = "Blablablablablablablalblbblablablablablablablabla";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(pseudoTropLong, this.motDePasseBon); });
            Assert.That(erreur.Message, Is.EqualTo("Le pseudo " + pseudoTropLong + " est trop long"));
        }
        [Test]
        public void etantDonneConstructeurUtilisateurAvecPseudoCharSpecial_quandCreerUtilisateur_alorsErreur()
        {
            string pseudoCharSpecial = "testChar!/";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(pseudoCharSpecial, this.motDePasseBon); });
            Assert.That(erreur.Message, Is.EqualTo("Le pseudo " + pseudoCharSpecial + " contient un caractère spécial"));
        }
        [Test]
        public void etantDonneConstructeurUtilisateurAvecMotDePasseTropCourt_quandCreerUtilisateur_alorsErreur()
        {
            string motDePasseCourt = "court";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(this.pseudoBon, motDePasseCourt); });
            Assert.That(erreur.Message, Is.EqualTo("Le mot de passe est trop court"));
        }
        [Test]
        public void etantDonneConstructeurUtilisateurAvecMotDePasseSansChiffre_quandCreerUtilisateur_alorsErreur()
        {
            string motDePasseCourt = "SansChiffre";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(this.pseudoBon, motDePasseCourt); });
            Assert.That(erreur.Message, Is.EqualTo("Le mot de passe doit contenir au moins un chiffre"));
        }
        [Test]
        public void etantDonneConstructeurUtilisateurAvecMotDePasseSansCharSpecial_quandCreerUtilisateur_alorsErreur()
        {
            string motDePasseCourt = "SansCharSpecial1";

            var erreur = Assert.Throws<ArgumentException>(
                delegate { new Utilisateur(this.pseudoBon, motDePasseCourt); });
            Assert.That(erreur.Message, Is.EqualTo("Le mot de passe doit contenir au moins un charactère spécial"));
        }
    }
}
