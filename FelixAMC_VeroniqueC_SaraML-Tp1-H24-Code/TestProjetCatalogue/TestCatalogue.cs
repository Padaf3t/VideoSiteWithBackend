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
        //TODO: reste a gérer erreur de méthode
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
        public void etantDonnerCatalogueVide_quandAppelAjouterVideo_alorsVideoAjouterDansLaListe()
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
            catalogue.AjouterVideo(video);

            bool supprimer = catalogue.SupprimerVideo(video);

            Assert.That(supprimer, Is.True);
        }

        [Test]
        public void etantDonneCatalogueAvecVideoNonPresente_quandappelSupprimerVideo_alorsRetourneFalse()
        {

            bool supprimer = catalogue.SupprimerVideo(video);

            Assert.That(supprimer, Is.False);
        }

        [Test]
        public void etantDonneCatalogueAvecVideoPresent_quandAppelSupprimerLeCatalogue_alorsCatalogueSupprimer()
        {
            catalogue.AjouterVideo(video);

            bool supprimer = catalogue.SupprimerLeCatalogue();

            Assert.That(supprimer, Is.True);
        }
        [Test]
        public void etantDonneCatalogueAvecVideo1EtVideo2Correct_quandAppelRemplacerVideo_alorsVideo2RemplaceVideo1()
        {
            
            Video video2 = new Video(1);

            catalogue.AjouterVideo(video);
            catalogue.AjouterVideo(video2);
            bool remplacer = catalogue.RemplacerVideo(video, video2);

            Assert.That(remplacer, Is.True);
        }
      
    }
}