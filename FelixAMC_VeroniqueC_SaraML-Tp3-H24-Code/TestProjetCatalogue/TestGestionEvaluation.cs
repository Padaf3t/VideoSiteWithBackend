using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetCatalogue.Models;

namespace TestProjetCatalogue
{
    public class TestGestionEvaluation
    {
        private Utilisateur user;
        private Video video;
        private GestionEvaluation gestion;
        private Evaluation evaluation;

        [SetUp]
        public void BaseSetup()
        {
            this.user = new Utilisateur("pierre", "Soleil01!");
            this.video = new Video(1);
            this.gestion = new GestionEvaluation();
            this.evaluation = new Evaluation(this.video.IdVideo, this.user.Pseudo, EnumCote.Excellent, "Ce video était trop mignon");

        }

        [Test]
        public void etantDonneGestionEvaluation_quandAppelConstructeurGestionEvaluation_alorsGestionEvaluationCree()
        {
            this.gestion = new GestionEvaluation();
            Assert.That(this.gestion.ListeEvaluations, Is.Not.Null);
        }

        [Test]
        public void etantDonnerGestionEvaluationVide_quandAppelAjouterEvaluation_alorsRetourneTrueEtEvaluationAjouterDansLaListe()
        {

            Assert.That(this.gestion.AjouterEvaluation(this.video, this.user, EnumCote.Excellent, "Ce video était trop mignon"), Is.True);
            Assert.That(this.evaluation, Is.EqualTo(this.gestion.ListeEvaluations[0]));
        }

        [Test]
        public void etantDonnerGestionEvaluationAvecEvaluationDejaPresenteEtMemeEvaluationAAjouter_quandAppelAjouterEvaluation_alorsRetourneFalse()
        {
            this.gestion.AjouterEvaluation(this.video, this.user, EnumCote.Excellent, "Ce video était trop mignon");

            Assert.That(this.gestion.AjouterEvaluation(this.video, this.user, EnumCote.Excellent, "Ce video était trop mignon"), Is.False);
        }

    }
}
