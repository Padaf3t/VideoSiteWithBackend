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
        public void etantDonnerCatalogue_quandAppelConstructeurCatalogue_alorsCatalogueCreer()
        {
            Catalogue catalogueTest = new Catalogue();
            Assert.That(catalogueTest.ListeVideos, Is.Not.Null);
        }

        [Test]
        public void etantDonnerCatalogueVide_quandAppelAjouterVideo_alorsRetourneTrueEtVideoAjouterDansLaListe()
        {

            this.catalogue.AjouterVideo(this.video);

            Assert.That(this.video, Is.EqualTo(this.catalogue.ListeVideos[0]));
        }

        [Test]
        public void etantDonnerCatalogueAvecVideoDejaPresenteEtMemeVideoAAjouter_quandAppelAjouterVideo_alorsRetourneFalse()
        {

            this.catalogue.AjouterVideo(this.video);

            Assert.That(this.catalogue.AjouterVideo(this.video), Is.False);
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