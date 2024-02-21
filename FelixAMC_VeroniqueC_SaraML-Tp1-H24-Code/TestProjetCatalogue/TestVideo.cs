using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using ProjetCatalogue;
using static ProjetCatalogue.Video;
using static System.Net.Mime.MediaTypeNames;

namespace TestProjetCatalogue
{
    internal class TestVideo
    {

        [Test]
        public void etantDonneConstructeurVideoAvecTitre_quandAppelAvecBonnesValeurs_alorsBonnesValeursChamps()
        {
            Video videoTest = new Video("Titre bidon");
            Assert.That(videoTest, Is.Not.Null);
            Assert.That(videoTest.Titre, Is.EqualTo("Titre bidon"));
            Assert.That(videoTest.TypeVideo, Is.EqualTo(EnumAnimal.Indetermine));
            Assert.That(videoTest.CoteEvaluation, Is.EqualTo(0));
            Assert.That(videoTest.DateRealisation, Is.Null);
            Assert.That(videoTest.DureeVideo, Is.EqualTo(0));
            Assert.That(videoTest.Auteur, Is.EqualTo(""));
            Assert.That(videoTest.Acteur, Is.EqualTo(""));
            Assert.That(videoTest.Extrait, Is.EqualTo(""));
            Assert.That(videoTest.VideoComplet, Is.EqualTo(""));
            Assert.That(videoTest.Image, Is.EqualTo(""));
        }

        [Test]
        public void etantDonneConstructeurVideoAvecTitre_quandAppelAvecTitreStringVide_alorsException()
        {

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video("");
            });

        }

        [Test]
        public void etantDonneConstructeurVideoAvecTitre_quandAppelAvecTitreDe4Char_alorsException()
        {

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video("Test");
            });

        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecBonnesValeur_alorsBonnesValeursChamps()
        {
            Video videoTest = new Video("Test titre", EnumAnimal.Chat, 3.5, null, 5.5, "Bobby", "Mr Miaws", "ressources/extrait/1111.mp4", "ressources/videos/1111.mp4", "ressources/images/1111.mp4");
            Assert.That(videoTest, Is.Not.Null);
            Assert.That(videoTest.Titre, Is.EqualTo("Test titre"));
            Assert.That(videoTest.TypeVideo, Is.EqualTo(EnumAnimal.Chat));
            Assert.That(videoTest.CoteEvaluation, Is.EqualTo(3.5));
            Assert.That(videoTest.DateRealisation, Is.Null);
            Assert.That(videoTest.DureeVideo, Is.EqualTo(5.5));
            Assert.That(videoTest.Auteur, Is.EqualTo("Bobby"));
            Assert.That(videoTest.Acteur, Is.EqualTo("Mr Miaws"));
            Assert.That(videoTest.Extrait, Is.EqualTo("ressources/extrait/1111.mp4"));
            Assert.That(videoTest.VideoComplet, Is.EqualTo("ressources/videos/1111.mp4"));
            Assert.That(videoTest.Image, Is.EqualTo("ressources/images/1111.mp4"));
        }




    }
}
