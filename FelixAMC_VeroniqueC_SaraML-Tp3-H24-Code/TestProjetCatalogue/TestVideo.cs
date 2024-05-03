using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using ProjetCatalogue.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TestProjetCatalogue
{
    internal class TestVideo
    {
        private static string pathExtrait = "ressources/extraits/";
        private static string pathVideoComplet = "ressources/videos/";
        private static string pathImage = "ressources/images/";

        private int bonId;
        private string bonTitre;
        private EnumAnimal bonEnumAnimal;
        private double bonneCote;
        private DateTime bonneDate;
        private double bonneDuree;
        private string bonAuteur;
        private string bonActeur;
        private string bonPathExtrait;
        private string bonPathVideoComplet;
        private string bonPathImage;

        [SetUp]
        public void BaseSetup()
        {
            this.bonId = 3;
            this.bonTitre = "Titre";
            this.bonEnumAnimal = EnumAnimal.Chat;
            this.bonneCote = 3.5;
            this.bonneDate = new DateTime(2010, 04, 04);
            this.bonneDuree = 5.5;
            this.bonAuteur = "Bobby";
            this.bonActeur = "Mr Miaws";
            this.bonPathExtrait = "1111.mp4";
            this.bonPathVideoComplet = "1111.mp4";
            this.bonPathImage = "1111.jpeg";
        }


        [Test]
        public void etantDonneConstructeurVideoAvecIdEtTitreAvecBonneValeur_quandAppelConstructeur_alorsBonnesValeursChamps()
        {
            Video videoTest = new Video(this.bonId, this.bonTitre);
            Assert.That(videoTest, Is.Not.Null);
            Assert.That(videoTest.Titre, Is.EqualTo(this.bonTitre));
            Assert.That(videoTest.TypeVideo, Is.EqualTo(EnumAnimal.Indetermine));
            Assert.That(videoTest.CoteEvaluation, Is.EqualTo(-1));
            Assert.That(videoTest.DateMiseEnLigne, Is.EqualTo(DateTime.Now));
            Assert.That(videoTest.DureeVideo, Is.EqualTo(0));
            Assert.That(videoTest.Auteur, Is.EqualTo(""));
            Assert.That(videoTest.Acteur, Is.EqualTo(""));
            Assert.That(videoTest.Extrait, Is.EqualTo("~/ressource/extraits/vide.mp4"));
            Assert.That(videoTest.VideoComplet, Is.EqualTo("~/ressource/videos/vide.mp4"));
            Assert.That(videoTest.Image, Is.EqualTo("~/ressource/images/vide.jpeg"));
        }

        [Test]
        public void etantDonneConstructeurVideoAvecIdCorrectEtTitreVide_quandAppelConstructeur_alorsException()
        {

            string titreStringVide = "";

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(this.bonId, titreStringVide);
            });

        }

        [Test]
        public void etantDonneConstructeurVideoAvecIdEtTitreDe4Char_quandAppelConstructeur_alorsException()
        {
            string titre4Char = "Test";

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(this.bonId, titre4Char);
            });

        }

        [Test]
        public void etantDonneConstructeurVideoCompletAvecBonneValeur_quandAppelConstructeur_alorsBonnesValeursChamps()
        {
            Video videoTest = new Video(this.bonId, this.bonTitre, this.bonEnumAnimal, this.bonneCote, this.bonneDate, this.bonneDuree, 
                this.bonAuteur, this.bonActeur, this.bonPathExtrait, this.bonPathVideoComplet, this.bonPathImage);
            Assert.That(videoTest, Is.Not.Null);
            Assert.That(videoTest.IdVideo, Is.EqualTo(this.bonId));
            Assert.That(videoTest.Titre, Is.EqualTo(this.bonTitre));
            Assert.That(videoTest.TypeVideo, Is.EqualTo(this.bonEnumAnimal));
            Assert.That(videoTest.CoteEvaluation, Is.EqualTo(this.bonneCote));
            Assert.That(videoTest.DateMiseEnLigne, Is.EqualTo(new DateTime(2010, 04, 04)));
            Assert.That(videoTest.DureeVideo, Is.EqualTo(this.bonneDuree));
            Assert.That(videoTest.Auteur, Is.EqualTo(this.bonAuteur));
            Assert.That(videoTest.Acteur, Is.EqualTo(this.bonActeur));
            Assert.That(videoTest.Extrait, Is.EqualTo("~/ressource/extraits/" + this.bonPathExtrait));
            Assert.That(videoTest.VideoComplet, Is.EqualTo("~/ressource/videos/" + this.bonPathVideoComplet));
            Assert.That(videoTest.Image, Is.EqualTo("~/ressource/images/" + this.bonPathImage));
        }

        [Test]
        public void etantDonneConstructeurVideoCompletAvecDateFuture_quandAppelContructeur_alorsException()
        {
            DateOnly dateFuture = new DateOnly(2034, 1, 1);

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(this.bonId, this.bonTitre, this.bonEnumAnimal, this.bonneCote, dateFuture, this.bonneDuree, 
                    this.bonAuteur, this.bonActeur, this.bonPathExtrait, this.bonPathVideoComplet, this.bonPathImage);
            });
        }

        [Test]
        public void etantDonneConstructeurVideoCompletAvecDureeVideoNeg_quandAppelConstructeur_alorsException()
        {
            double dureeNeg = -3;

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(this.bonId, this.bonTitre, this.bonEnumAnimal, this.bonneCote, this.bonneDate, dureeNeg,
                    this.bonAuteur, this.bonActeur, this.bonPathExtrait, this.bonPathVideoComplet, this.bonPathImage);
            });
        }

        [Test]
        public void etantDonneConstructeurVideoCompletAvecDureeVideoTropElevee_quandAppelConstructeur_alorsException()
        {
            double dureeTropElevee = 30.1;

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(this.bonId, this.bonTitre, this.bonEnumAnimal, this.bonneCote, this.bonneDate, dureeTropElevee,
                    this.bonAuteur, this.bonActeur, this.bonPathExtrait, this.bonPathVideoComplet, this.bonPathImage);

            });
        }

        [Test]
        public void etantDonneConstructeurVideoCompletAvecNomAuteurTropLong_quandAppelConstructeur_alorsException()
        {
            string auteurTropLong = "BobbyBobbyBobbyBobbyBobbyBobbyBobbyBobbyBobbyBobbyB";

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(this.bonId, this.bonTitre, this.bonEnumAnimal, this.bonneCote, this.bonneDate, this.bonneDuree,
                    auteurTropLong, this.bonActeur, this.bonPathExtrait, this.bonPathVideoComplet, this.bonPathImage);
            });
        }

        [Test]
        public void etantDonneConstructeurVideoCompletAvecNomActeurTropLong_quandAppelConstructeur_alorsException()
        {
            string acteurTropLong = "01234567890123456789012345";

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(this.bonId, this.bonTitre, this.bonEnumAnimal, this.bonneCote, this.bonneDate, this.bonneDuree,
                    this.bonAuteur, acteurTropLong, this.bonPathExtrait, this.bonPathVideoComplet, this.bonPathImage);
            });
        }

        [Test]
        public void etantDonneConstructeurVideoCompletAvecMauvaisPathExtrait_quandAppelConstructeur_alorsException()
        {
            string mauvaisPathExtrait = "blabla/1111.mp4";

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(this.bonId, this.bonTitre, this.bonEnumAnimal, this.bonneCote, this.bonneDate, this.bonneDuree,
                    this.bonAuteur, this.bonActeur, @mauvaisPathExtrait, this.bonPathVideoComplet, this.bonPathImage);
            });
        }

        [Test]
        public void etantDonneConstructeurVideoCompletAvecPathExtraitAvecMauvaiseExtensionFichier_quandAppelConstructeur_alorsException()
        {
            string pathExtraitMauvaiseExtension = "1111.mov";

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(this.bonId, this.bonTitre, this.bonEnumAnimal, this.bonneCote, this.bonneDate, this.bonneDuree,
                    this.bonAuteur, this.bonActeur, @pathExtraitMauvaiseExtension, this.bonPathVideoComplet, this.bonPathImage);
            });
        }

        [Test]
        public void etantDonneConstructeurVideoCompletAvecMauvaisPathVideoComplet_quandAppelConstructeur_alorsException()
        {
            string mauvaisPathVideoComplet = "blabla/1111.mp4";

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(this.bonId, this.bonTitre, this.bonEnumAnimal, this.bonneCote, this.bonneDate, this.bonneDuree,
                    this.bonAuteur, this.bonActeur, this.bonPathExtrait, @mauvaisPathVideoComplet, this.bonPathImage);
            });
        }

        [Test]
        public void etantDonneConstructeurVideoCompletAvecPathVideoCompletAvecMauvaiseExtensionFichier_quandAppelConstructeur_alorsException()
        {
            string pathVideoCompletMauvaiseExtension = "1111.mov";

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(this.bonId, this.bonTitre, this.bonEnumAnimal, this.bonneCote, this.bonneDate, this.bonneDuree,
                    this.bonAuteur, this.bonActeur, this.bonPathExtrait, @pathVideoCompletMauvaiseExtension, this.bonPathImage);
            });
        }

        [Test]
        public void etantDonneConstructeurVideoCompletAvecMauvaisPathImage_quandAppelConstructeur_alorsException()
        {
            string mauvaisPathImage = "blabla/1111.jpeg";

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(this.bonId, this.bonTitre, this.bonEnumAnimal, this.bonneCote, this.bonneDate, this.bonneDuree,
                    this.bonAuteur, this.bonActeur, this.bonPathExtrait, this.bonPathVideoComplet, @mauvaisPathImage);
            });
        }

        [Test]
        public void etantDonneConstructeurVideoCompletAvecPathImageAvecMauvaiseExtensionFichier_quandAppelConstructeur_alorsException()
        {
            string pathImageMauvaiseExtension = "1111.bmp";

            Assert.Throws<ArgumentException>(() =>
            {
                Video videoTest = new Video(this.bonId, this.bonTitre, this.bonEnumAnimal, this.bonneCote, this.bonneDate, this.bonneDuree,
                    this.bonAuteur, this.bonActeur, this.bonPathExtrait, this.bonPathVideoComplet, @pathImageMauvaiseExtension);
            });
        }

        [Test]
        public void etantDonne2VideosAvecMemeIdMaisTitreDifferent_quandAppelMethodeEquals_alorsTrue()
        {
            string titre1 = "Titre1";
            string titre2 = "Titre2";

            Video videoTest1 = new Video(this.bonId, titre1);
            Video videoTest2 = new Video(this.bonId, titre2);
            Assert.That(videoTest1.Equals(videoTest2),Is.EqualTo(true));
        }

        [Test]
        public void etantDonne2VideosAvecMemeIdMaisTitreDifferent_quandAppelOperatorEqual_alorsTrue()
        {
            string titre1 = "Titre1";
            string titre2 = "Titre2";

            Video videoTest1 = new Video(this.bonId, titre1);
            Video videoTest2 = new Video(this.bonId, titre2);
            Assert.That(videoTest1 == videoTest2, Is.EqualTo(true));
        }

        [Test]
        public void etantDonne2VideosAvecIdDifferentMaisAutresParamIdentiques_quandAppelMethodeEquals_alorsFalse()
        {
            int id1 = 1;
            int id2 = 2;


            Video videoTest1 = new Video(id1);
            Video videoTest2 = new Video(id2);
            Assert.That(videoTest1.Equals(videoTest2), Is.EqualTo(false));
        }

        [Test]
        public void etantDonne2VideosAvecIdDifferentMaisAutresParamIdentiques_quandAppelOperateurNotEqual_alorsTrue()
        {
            int id1 = 1;
            int id2 = 2;


            Video videoTest1 = new Video(id1);
            Video videoTest2 = new Video(id2);
            Assert.That(videoTest1 != videoTest2, Is.EqualTo(true));
        }
    }
}
