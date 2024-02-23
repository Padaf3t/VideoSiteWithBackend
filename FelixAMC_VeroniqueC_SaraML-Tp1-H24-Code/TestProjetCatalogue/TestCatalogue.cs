using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetCatalogue;

namespace TestProjetCatalogue
{
    public class Tests
    {
        private Catalogue catalogue = new Catalogue();
        private Video video = new Video();

        [Test]
        public void etantDonnerCatalogue_quandAppelConstructeurCatalogue_alorsCatalogueCreer()
        {
            Assert.That(catalogue.ListeVideos, Is.Not.Null);
        }
        [Test]
        public void etantDonnerCatalogueAvecVideoPresente_quandVideoAjouter_alorsVideoAjouterDansLaListe()
        {

            catalogue.AjouterVideo(video);

            Assert.That(video, Is.EqualTo(catalogue.ListeVideos[0]));
        }
     
        [Test]
        public void etantDonneCatalogueAvecVideoPresente_quandappelSupprimerVideo_alorsVideoSupprimer()
        {
            catalogue.AjouterVideo(video);

            bool supprimer = catalogue.SupprimerVideo(video);

            Assert.That(supprimer, Is.True);
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