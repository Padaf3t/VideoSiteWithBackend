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

        int bonId = 3;
        string bonTitre = "Titre";
        EnumAnimal bonEnumAnimal = EnumAnimal.Chat;
        double bonneCote = 3.5;
        DateOnly? bonneDate = null;
        double bonneDuree = 5.5;
        string bonAuteur = "Bobby";
        string bonActeur = "Mr Miaws";
        string bonPathExtrait = "ressources/extraits/1111.mp4";
        string bonPathVideoComplet = "ressources/videos/1111.mp4";
        string bonPathImage = "ressources/images/1111.jpeg";


        [Test]
        public void etantDonneConstructeurVideoAvecIdEtTitre_quandAppelAvecBonnesValeurs_alorsBonnesValeursChamps()
        {
            Video videoTest = new Video(bonId, bonTitre);
            Assert.That(videoTest, Is.Not.Null);
            Assert.That(videoTest.Titre, Is.EqualTo(bonTitre));
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

            string titreStringVide = "";

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(bonId, titreStringVide);
            });

        }

        [Test]
        public void etantDonneConstructeurVideoAvecIdEtTitre_quandAppelAvecTitreDe4Char_alorsException()
        {
            string titre4Char = "Test";

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(bonId, titre4Char);
            });

        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecBonnesValeur_alorsBonnesValeursChamps()
        {
            Video videoTest = new Video(bonId, bonTitre, bonEnumAnimal, bonneCote, bonneDate, bonneDuree, bonAuteur, bonActeur,
                bonPathExtrait, bonPathVideoComplet, bonPathImage);
            Assert.That(videoTest, Is.Not.Null);
            Assert.That(videoTest.IdVideo, Is.EqualTo(bonId));
            Assert.That(videoTest.Titre, Is.EqualTo(bonTitre));
            Assert.That(videoTest.TypeVideo, Is.EqualTo(bonEnumAnimal));
            Assert.That(videoTest.CoteEvaluation, Is.EqualTo(bonneCote));
            Assert.That(videoTest.DateRealisation, Is.Null);
            Assert.That(videoTest.DureeVideo, Is.EqualTo(bonneDuree));
            Assert.That(videoTest.Auteur, Is.EqualTo(bonAuteur));
            Assert.That(videoTest.Acteur, Is.EqualTo(bonActeur));
            Assert.That(videoTest.Extrait, Is.EqualTo(bonPathExtrait));
            Assert.That(videoTest.VideoComplet, Is.EqualTo(bonPathVideoComplet));
            Assert.That(videoTest.Image, Is.EqualTo(bonPathImage));
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecDateFuture_alorsException()
        {
            DateOnly dateFuture = new DateOnly(2034, 1, 1);

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(bonId, bonTitre, bonEnumAnimal, bonneCote, dateFuture, bonneDuree, bonAuteur, bonActeur,
                bonPathExtrait, bonPathVideoComplet, bonPathImage);
            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecDureeVideoNeg_alorsException()
        {
            double dureeNeg = -3;

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(bonId, bonTitre, bonEnumAnimal, bonneCote, bonneDate, dureeNeg, bonAuteur, bonActeur,
                bonPathExtrait, bonPathVideoComplet, bonPathImage);
            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecDureeVideoTropElevee_alorsException()
        {
            double dureeTropElevee = 30.1;

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(bonId, bonTitre, bonEnumAnimal, bonneCote, bonneDate, dureeTropElevee, bonAuteur, bonActeur,
                bonPathExtrait, bonPathVideoComplet, bonPathImage);

            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecNomAuteurTropLong_alorsException()
        {
            string auteurTropLong = "BobbyBobbyBobbyBobbyBobbyBobbyBobbyBobbyBobbyBobbyB";

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(bonId, bonTitre, bonEnumAnimal, bonneCote, bonneDate, bonneDuree, auteurTropLong, bonActeur,
                bonPathExtrait, bonPathVideoComplet, bonPathImage);
            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecNomActeurTropLong_alorsException()
        {
            string acteurTropLong = "01234567890123456789012345";

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(bonId, bonTitre, bonEnumAnimal, bonneCote, bonneDate, bonneDuree, bonAuteur, acteurTropLong,
                bonPathExtrait, bonPathVideoComplet, bonPathImage);
            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecMauvaisPathExtrait_alorsException()
        {
            string mauvaisPathExtrait = "blabla/1111.mp4";

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(bonId, bonTitre, bonEnumAnimal, bonneCote, bonneDate, bonneDuree, bonAuteur, bonActeur,
                mauvaisPathExtrait, bonPathVideoComplet, bonPathImage);
            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecPathExtraitAvecMauvaiseExtensionFichier_alorsException()
        {
            string pathExtraitMauvaiseExtension = "ressources/extraits/1111.mov";

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(bonId, bonTitre, bonEnumAnimal, bonneCote, bonneDate, bonneDuree, bonAuteur, bonActeur,
                pathExtraitMauvaiseExtension, bonPathVideoComplet, bonPathImage);
            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecMauvaisPathVideoComplet_alorsException()
        {
            string mauvaisPathVideoComplet = "blabla/1111.mp4";

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(bonId, bonTitre, bonEnumAnimal, bonneCote, bonneDate, bonneDuree, bonAuteur, bonActeur,
                bonPathExtrait, mauvaisPathVideoComplet, bonPathImage);
            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecPathVideoCompletAvecMauvaiseExtensionFichier_alorsException()
        {
            string pathVideoCompletMauvaiseExtension = "ressources/videos/1111.mov";

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(bonId, bonTitre, bonEnumAnimal, bonneCote, bonneDate, bonneDuree, bonAuteur, bonActeur,
                bonPathExtrait, pathVideoCompletMauvaiseExtension, bonPathImage);
            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecMauvaisPathImage_alorsException()
        {
            string mauvaisPathImage = "blabla/1111.jpeg";

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(bonId, bonTitre, bonEnumAnimal, bonneCote, bonneDate, bonneDuree, bonAuteur, bonActeur,
                bonPathExtrait, bonPathVideoComplet, mauvaisPathImage);
            });
        }

        [Test]
        public void etantDonneConstructeurTousParam_quandAppelAvecPathImageAvecMauvaiseExtensionFichier_alorsException()
        {
            string pathImageMauvaiseExtension = "ressources/images/1111.bmp";

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(bonId, bonTitre, bonEnumAnimal, bonneCote, bonneDate, bonneDuree, bonAuteur, bonActeur,
                bonPathExtrait, bonPathVideoComplet, pathImageMauvaiseExtension);
            });
        }

        [Test]
        public void etantDonne2VideosAvecMemeIdMaisTitreDifferent_quandAppelMethodeEquals_alorsTrue()
        {
            string titre1 = "Titre1";
            string titre2 = "Titre2";

            Video videoTest1 = new Video(bonId, titre1);
            Video videoTest2 = new Video(bonId, titre2);
            Assert.That(videoTest1.Equals(videoTest2));
            Assert.That(videoTest1 == videoTest2);
        }

        [Test]
        public void etantDonne2VideosAvecIdDifferentMaisAutresParamIdentiques_quandAppelMethodeEquals_alorsFalse()
        {
            int id1 = 1;
            int id2 = 2;


            Video videoTest1 = new Video(id1);
            Video videoTest2 = new Video(id2);
            Assert.That(!(videoTest1.Equals(videoTest2)));
            Assert.That(videoTest1 != videoTest2);
        }
    }
}
