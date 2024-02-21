using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCatalogue
{
    public class GestionEvaluation
    {
        List<Evaluation> _listeEvaluations;

        public List<Evaluation> ListeEvaluations { get => _listeEvaluations; set => _listeEvaluations = value; }

        public GestionEvaluation()
        {
            _listeEvaluations = new List<Evaluation>();
        }

        /// <summary>
        /// Permet l'ajout d'une évaluation à la liste des évaluations de l'utilisateur
        /// </summary>
        /// <param name="user">L'utilisateur qui créer l'évaluation</param>
        /// <param name="video">La vidéo évaluée</param>
        /// <param name="cote">La cote attribuée</param>
        /// <param name="texte">Le texte que l'utilisateur a écrit pour son évaluation</param>
        /// <returns>bool : true si l'évaluation a bien été ajoutée à sa liste</returns>
        public bool AjouterEvaluation(Video video, Utilisateur user, EnumCote cote, string texte)
        {
            Evaluation evaluationActuel = new Evaluation(video.IdVideo, user.Pseudo, cote, texte);

            this.ListeEvaluations.Add(evaluationActuel);

            return this.ListeEvaluations.Last() == evaluationActuel;
        }
    }
}
