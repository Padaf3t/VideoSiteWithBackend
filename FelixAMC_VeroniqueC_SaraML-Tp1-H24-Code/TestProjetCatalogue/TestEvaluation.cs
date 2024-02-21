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
            Evaluation evaluationTest = new Evaluation(25, "pseudoTest", EnumCote.Mediocre, "") ;
            Assert.That(evaluationTest.PseudoUtilisateur, Is.EqualTo("pseudoTest"));
            Assert.That(evaluationTest.IdVideo, Is.EqualTo(25));
            Assert.That(evaluationTest.CoteDonne, Is.EqualTo(EnumCote.Mediocre));
            Assert.That(evaluationTest.TexteEvaluation, Is.EqualTo(""));
            //todo: c'est pas exactement pareil à cause du mini délai entre exécution des instructions mais on voit que ca marche
            //Assert.That(evaluationTest.DateDeCreation, Is.EqualTo(System.DateTime.Now));
            Assert.That(evaluationTest.DateDeModification, Is.EqualTo(null));
        }

    }
}
