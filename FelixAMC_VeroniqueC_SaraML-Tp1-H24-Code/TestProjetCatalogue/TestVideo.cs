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
        public void etantDonneConstructeurVideo_quandAppelAvecBonnesValeurs_alorsBonnesValeursChamps()
        {
            Video videoTest = new Video("Titre bidon");
            Assert.That(videoTest, Is.Not.Null);
            Assert.That(videoTest.Titre, Is.EqualTo("Titre bidon"));
            Assert.That(videoTest.TypeVideo, Is.EqualTo(Animal.Indetermine));
            Assert.That(videoTest.CoteEvaluation, Is.EqualTo(0));
            Assert.That(videoTest.ListeEvaluations, Is.Not.Null);
            Assert.That(videoTest.DateRealisation, Is.Null);
            Assert.That(videoTest.DureeVideo, Is.EqualTo(0));
            Assert.That(videoTest.Auteur, Is.EqualTo(""));
            Assert.That(videoTest.Acteur, Is.EqualTo(""));
            Assert.That(videoTest.Extrait, Is.EqualTo(""));
            Assert.That(videoTest.VideoComplet, Is.EqualTo(""));
            Assert.That(videoTest.Image, Is.EqualTo(""));
        }

    }
}
