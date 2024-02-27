using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ProjetCatalogue;
using static System.Collections.Specialized.BitVector32;

namespace TestProjetCatalogue
{
    public class TestCatalogue
    {
        private Catalogue catalogue;
        private Video video;

        [SetUp]
        public void BaseSetUp()
        {
            this.catalogue = new Catalogue();
            this.video = new Video(1);
        }

        [Test]
        public void etantDonneCatalogue_quandAppelConstructeurCatalogue_alorsCatalogueCreer()
        {
            Catalogue catalogueTest = new Catalogue();
            Assert.That(catalogueTest.ListeVideos, Is.Not.Null);
        }

        [Test]
        public void etantDonneCatalogueVide_quandAppelAjouterVideo_alorsRetourneTrueEtVideoAjouterDansLaListe()
        {

            this.catalogue.AjouterVideo(this.video);

            Assert.That(this.video, Is.EqualTo(this.catalogue.ListeVideos[0]));
        }

        [Test]
        public void etantDonneCatalogueAvecVideoDejaPresenteEtMemeVideoAAjouter_quandAppelAjouterVideo_alorsRetourneFalse()
        {

            this.catalogue.AjouterVideo(this.video);

            Assert.That(this.catalogue.AjouterVideo(this.video), Is.False);
        }

        [Test]
        public void etantDonneCatalogueVideEtLastIdPlusPetitQueIdVideo_quandAppelAjouterVideo_alorsLastIdChangePourIdVideo()
        {
            Assert.That(this.catalogue.LastId, Is.EqualTo(0));

            this.catalogue.AjouterVideo(this.video);

            Assert.That(this.catalogue.LastId, Is.EqualTo(1));
        }

        [Test]
        public void etantDonneCatalogueVideEtLastIdPlusGrandQueIdVideo_quandAppelAjouterVideo_alorsLastIdNeChangePas()
        {
            //Incremente le lastId de 3
            this.catalogue.GenerateId();
            this.catalogue.GenerateId();
            this.catalogue.GenerateId();
            int lastIdAvantAjouterVideo = this.catalogue.LastId;
            Video videoAyantId2 = new Video(2);

            Assert.That(this.catalogue.LastId, Is.EqualTo(lastIdAvantAjouterVideo));

            this.catalogue.AjouterVideo(videoAyantId2);

            Assert.That(this.catalogue.LastId, Is.EqualTo(lastIdAvantAjouterVideo));
            
        }

        [Test]
        public void etantDonneCatalogueVideEtLastIdEgalAIdVideo_quandAppelAjouterVideo_alorsLastIdNeChangePas()
        {
            //Incremente le lastId de 3
            this.catalogue.GenerateId();
            this.catalogue.GenerateId();
            this.catalogue.GenerateId();
            int lastIdAvantAjouterVideo = this.catalogue.LastId;
            Video videoAyantIdLastId = new Video(lastIdAvantAjouterVideo);

            Assert.That(this.catalogue.LastId, Is.EqualTo(lastIdAvantAjouterVideo));

            this.catalogue.AjouterVideo(videoAyantIdLastId);

            Assert.That(this.catalogue.LastId, Is.EqualTo(lastIdAvantAjouterVideo));

        }

        [Test]
        public void etantDonneCatalogueAvecVideoPresente_quandappelSupprimerVideo_alorsVideoSupprimer()
        {
            this.catalogue.AjouterVideo(this.video);

            bool supprimer = this.catalogue.SupprimerVideo(this.video);

            Assert.That(supprimer, Is.True);
        }

        [Test]
        public void etantDonneCatalogueAvecVideoNonPresente_quandappelSupprimerVideo_alorsRetourneFalse()
        {

            bool supprimer = this.catalogue.SupprimerVideo(this.video);

            Assert.That(supprimer, Is.False);
        }

        [Test]
        public void etantDonneCatalogueAvecVideoAyantIdDe14EtAutresVideosAyantIdInferieur_quandAppelSetLastId_alorsLastIdEst14()
        {
            int idTest = 14;
            Video videoId14 = new Video(idTest);
            for(int i = 0; i < 5; i++)
            {
                this.catalogue.AjouterVideo(new Video(i));
            }
            this.catalogue.AjouterVideo(videoId14);
            for (int i = 5; i < 10; i++)
            {
                this.catalogue.AjouterVideo(new Video(i));
            }

            this.catalogue.SetLastId();

            Assert.That(this.catalogue.LastId, Is.EqualTo(14));
        }

        [Test]
        public void etantDonneCatalogueAvecVideoPresent_quandAppelSupprimerLeCatalogue_alorsCatalogueSupprimer()
        {
            this.catalogue.AjouterVideo(this.video);

            bool supprimer = this.catalogue.SupprimerLeCatalogue();

            Assert.That(supprimer, Is.True);
        }
        [Test]
        public void etantDonneCatalogueAvecVideo1EtVideo2Correct_quandAppelRemplacerVideo_alorsVideo2RemplaceVideo1()
        {
            
            Video video2 = new Video(1);

            this.catalogue.AjouterVideo(this.video);
            this.catalogue.AjouterVideo(video2);
            bool remplacer = this.catalogue.RemplacerVideo(this.video, video2);

            Assert.That(remplacer, Is.True);
        }
      
    }
}