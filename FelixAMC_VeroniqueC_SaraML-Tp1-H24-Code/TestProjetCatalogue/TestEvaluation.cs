using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetCatalogue;

namespace TestProjetCatalogue
{
    internal class TestEvaluation
    {

        [Test]
        public void etantDonneConstructeurEvaluation_quandAppelAvecBonnesValeurs_alorsBonnesValeursChamps()
        {
            Evaluation evaluationTest = new Evaluation(25, "pseudoTest", Evaluation.Cote.Mediocre);
            Assert.That(evaluationTest._pseudoUtilisateur, Is.EqualTo("pseudoTest"));
            Assert.That(evaluationTest._idVideo, Is.EqualTo(25));
            Assert.That(evaluationTest._cote, Is.EqualTo(Evaluation.Cote.Mediocre));
        }

        [Test]
        public void etantDonneConstructeurEvaluation_quandAppelSansParam_alorsFonctionne()
        {
            Evaluation evaluationTest = new Evaluation();
        }
    }
}
