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
        public void etantDonneConstructeurVideoAvecIdEtTitre_quandAppelAvecBonnesValeurs_alorsBonnesValeursChamps()
        {
            Video videoTest = new Video(8, "Titre bidon");
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
        public void etantDonneConstructeurVideoAvecIdEtTitre_quandAppelAvecTitreStringVide_alorsException()
        {

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(1, "");
            });

        }

        [Test]
        public void etantDonneConstructeurVideoAvecIdEtTitre_quandAppelAvecTitreDe4Char_alorsException()
        {

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(1, "Test");
            });

        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecBonnesValeur_alorsBonnesValeursChamps()
        {
            Video videoTest = new Video(1, "Test titre", EnumAnimal.Chat, 3.5, null, 5.5, "Bobby", "Mr Miaws",
                "ressources/extraits/1111.mp4", "ressources/videos/1111.mp4", "ressources/images/1111.jpeg");
            Assert.That(videoTest, Is.Not.Null);
            Assert.That(videoTest.IdVideo, Is.EqualTo(1));
            Assert.That(videoTest.Titre, Is.EqualTo("Test titre"));
            Assert.That(videoTest.TypeVideo, Is.EqualTo(EnumAnimal.Chat));
            Assert.That(videoTest.CoteEvaluation, Is.EqualTo(3.5));
            Assert.That(videoTest.DateRealisation, Is.Null);
            Assert.That(videoTest.DureeVideo, Is.EqualTo(5.5));
            Assert.That(videoTest.Auteur, Is.EqualTo("Bobby"));
            Assert.That(videoTest.Acteur, Is.EqualTo("Mr Miaws"));
            Assert.That(videoTest.Extrait, Is.EqualTo("ressources/extraits/1111.mp4"));
            Assert.That(videoTest.VideoComplet, Is.EqualTo("ressources/videos/1111.mp4"));
            Assert.That(videoTest.Image, Is.EqualTo("ressources/images/1111.jpeg"));
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecCoteTropElevee_alorsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(1, "Test titre", EnumAnimal.Chat, 5.001, null, 5.5, "Bobby", "Mr Miaws",
                    "ressources/extraits/1111.mp4", "ressources/videos/1111.mp4", "ressources/images/1111.jpeg");
            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecCoteNeg_alorsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(1, "Test titre", EnumAnimal.Chat, -0.01, null, 5.5, "Bobby", "Mr Miaws",
                    "ressources/extraits/1111.mp4", "ressources/videos/1111.mp4", "ressources/images/1111.jpeg");
            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecDateFuture_alorsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(1, "Test titre", EnumAnimal.Chat, 3.5, new DateOnly(2034, 1, 1), 5.5, "Bobby", "Mr Miaws",
                    "ressources/extraits/1111.mp4", "ressources/videos/1111.mp4", "ressources/images/1111.jpeg");
            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecDureeVideoNeg_alorsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(1, "Test titre", EnumAnimal.Chat, 3.5, null, -3, "Bobby", "Mr Miaws",
                    "ressources/extraits/1111.mp4", "ressources/videos/1111.mp4", "ressources/images/1111.jpeg");
            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecDureeVideoTropElevee_alorsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(1, "Test titre", EnumAnimal.Chat, 3.5, null, 30.1, "Bobby", "Mr Miaws",
                    "ressources/extraits/1111.mp4", "ressources/videos/1111.mp4", "ressources/images/1111.jpeg");
            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecNomAuteurTropLong_alorsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(1, "Test titre", EnumAnimal.Chat, 3.5, null, 5.5, "BobbyBobbyBobbyBobbyBobbyBobbyBobbyBobbyBobbyBobbyB",
                    "Mr Miaws", "ressources/extraits/1111.mp4", "ressources/videos/1111.mp4", "ressources/images/1111.jpeg");
            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecNomActeurTropLong_alorsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(1, "Test titre", EnumAnimal.Chat, 3.5, null, 5.5, "Bobby", "01234567890123456789012345",
                    "ressources/extraits/1111.mp4", "ressources/videos/1111.mp4", "ressources/images/1111.jpeg");
            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecMauvaisPathExtrait_alorsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(1, "Test titre", EnumAnimal.Chat, 3.5, null, 5.5, "Bobby", "Mr Miaws",
                    "blabla/1111.mp4", "ressources/videos/1111.mp4", "ressources/images/1111.jpeg");
            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecPathExtraitAvecMauvaiseExtensionFichier_alorsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(1, "Test titre", EnumAnimal.Chat, 3.5, null, 5.5, "Bobby", "Mr Miaws",
                    "ressources/extraits/1111.mov", "ressources/videos/1111.mp4", "ressources/images/1111.jpeg");
            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecMauvaisPathVideoComplet_alorsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(1, "Test titre", EnumAnimal.Chat, 3.5, null, 5.5, "Bobby", "Mr Miaws",
                    "ressources/extraits/1111.mp4", "blabla/1111.mp4", "ressources/images/1111.jpeg");
            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecPathVideoCompletAvecMauvaiseExtensionFichier_alorsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(1, "Test titre", EnumAnimal.Chat, 3.5, null, 5.5, "Bobby", "Mr Miaws",
                "ressources/extraits/1111.mp4", "ressources/videos/1111.mov", "ressources/images/1111.jpeg");
            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecMauvaisPathImage_alorsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(1, "Test titre", EnumAnimal.Chat, 3.5, null, 5.5, "Bobby", "Mr Miaws",
                "ressources/extraits/1111.mp4", "ressources/videos/1111.mp4", "blabla/1111.jpeg");
            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecPathImageAvecMauvaiseExtensionFichier_alorsException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(1, "Test titre", EnumAnimal.Chat, 3.5, null, 5.5, "Bobby", "Mr Miaws",
                "ressources/extraits/1111.mp4", "ressources/videos/1111.mp4", "ressources/images/1111.bmp");
            });
        }



    }
}
