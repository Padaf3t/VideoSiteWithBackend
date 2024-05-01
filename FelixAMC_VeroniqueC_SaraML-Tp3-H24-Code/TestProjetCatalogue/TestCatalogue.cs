using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ProjetCatalogue.Models;
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

        [TearDown]
        public void BaseTearDown()
        {
            this.catalogue.Dispose();
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

            //List<Video> listeTemp = this.catalogue.ListeVideos.ToList();

            //Assert.That(this.video, Is.EqualTo(listeTemp[0]));

            Assert.That(this.catalogue.ListeVideos.Contains(this.video));

        }

        [Test]
        public void etantDonneCatalogueAvecVideoDejaPresenteEtMemeVideoAAjouter_quandAppelAjouterVideo_alorsRetourneFalse()
        {

            this.catalogue.AjouterVideo(this.video);

            Assert.That(this.catalogue.AjouterVideo(this.video), Is.False);
        }
      
    }
}