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
            Assert.That(catalogueTest.Videos, Is.Not.Null);
        }
    }
}