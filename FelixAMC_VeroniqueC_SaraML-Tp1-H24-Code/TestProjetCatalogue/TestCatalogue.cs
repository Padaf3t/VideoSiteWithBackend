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
        public void etantConstructeurCatalogue_quandCreerCatalogue_alorsCatalogueCreer()
        {
            Assert.That(catalogue.ListeVideos, Is.Not.Null);
        }
        [Test]
        public void etantAjouterVideo_quandVideoAjouter_alorsVideoAjouterDansLaListe()
        {

            catalogue.AjouterVideo(video);

            Assert.That(video, Is.EqualTo(catalogue.ListeVideos[0]));
        }
     
        [Test]
        public void etantSupprimerVideo_quandVideoSupprimer_alorsVideoSupprimer()
        {
            catalogue.AjouterVideo(video);

            bool supprimer = catalogue.SupprimerVideo(video);

            Assert.That(supprimer, Is.True);
        }
        [Test]
        public void etantSupprimerCatalogue_quandCatalogueSupprimer_alorsCatalogueSupprimer()
        {
            catalogue.AjouterVideo(video);

            bool supprimer = catalogue.SupprimerLeCatalogue();

            Assert.That(supprimer, Is.True);
        }
        [Test]
        public void TestRemplacer()
        {
            
            Video video2 = new Video(1);

            catalogue.AjouterVideo(video);
            catalogue.AjouterVideo(video2);
            bool remplacer = catalogue.RemplacerVideo(video, video2);

            Assert.That(remplacer, Is.True);
        }
      
    }
}