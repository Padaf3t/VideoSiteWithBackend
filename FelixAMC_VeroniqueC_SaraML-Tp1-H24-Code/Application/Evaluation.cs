using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCatalogue
{
    /// <summary>
    /// Classe qui constitue une évaluation faite par un utilisateur pour une vidéo
    /// </summary>
    public class Evaluation
    {
        private EnumCote _coteDonne;
        private int _idVideo;
        private string _pseudoUtilisateur;
        private string _texteEvaluation;
        private DateTime _dateDeCreation;
        private DateTime? _dateDeModification;

        public EnumCote CoteDonne { get => _coteDonne; set => _coteDonne = value; }
        public int IdVideo { get => _idVideo; set => _idVideo = value; }
        public string PseudoUtilisateur { get => _pseudoUtilisateur; set => _pseudoUtilisateur = value; }
        public string TexteEvaluation { get => _texteEvaluation; set => _texteEvaluation = value; }
        public DateTime DateDeCreation { get => _dateDeCreation; }
        public DateTime? DateDeModification { get => _dateDeModification; set => _dateDeModification = value; }


        public Evaluation()
        {
        }

        /// <summary>
        /// Constructeur de la classe - la date de création est automatiquement mise (date du moment où l'éval est faite)
        /// et la date de modification est null au moment où l'éval est créée.
        /// </summary>
        /// <param name="pidVideo">le id de la vidéo évaluée</param>
        /// <param name="ppseudoUtilisateur">le pseudo de l'utilisateur qui effectue l'évaluation</param>
        /// <param name="pcote">la cote que l'utilisateur attribue à la vidéeo</param>
        /// <param name="ptexte">le texte que l'utilisateur rédige dans le cadre de son évaluation</param>
        public Evaluation(int pidVideo, string ppseudoUtilisateur, EnumCote pcote, string ptexte)
        {
            IdVideo = pidVideo; // unique
            PseudoUtilisateur = ppseudoUtilisateur; // unique
            CoteDonne = pcote;
            TexteEvaluation = ptexte;
            _dateDeCreation = System.DateTime.Now;
            DateDeModification = null;

        }

        /// <summary>
        /// Methode GetHashCode de la classe, sur la base de l'id de la vidéo évaluée et du pseudo de l'utilisateur qui a évalué
        /// </summary>
        /// <returns>int : le hashcode généré</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(IdVideo, PseudoUtilisateur);
        }

        /// <summary>
        /// Methode Equals de la classe, appelée sur une évaluation qu'on va comparer à celle reçue en paramètre
        /// (après avoir vérifié que c'est bien une Évaluation) pour savoir si c'est la même, sur la base de l'id
        /// de la vidéo évaluée et du pseudo de l'utilisateur qui a évalué.
        /// </summary>
        /// <param name="obj">L'évaluation avec laquelle on fait la comparaison</param>
        /// <returns>bool : true si c'est la même évaluation</returns>
        public override bool Equals(object? obj)
        {
            return obj is Evaluation evaluation &&
                   _idVideo == evaluation._idVideo &&
                   _pseudoUtilisateur == evaluation._pseudoUtilisateur;
        }

        /// <summary>
        /// Methode de surcharge de l'opérateur ==, qui utilise la méthode Equals pour vérifier si 2 évaluations
        /// sont la même
        /// </summary>
        /// <param name="left">Evaluation : une evaluation</param>
        /// <param name="right">Evaluation : une autre evaluation</param>
        /// <returns></returns>
        public static bool operator ==(Evaluation left, Evaluation right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Methode de surcharge de l'opérateur !=, qui utilise la méthode de surcharge de l'opérateur ==
        /// pour vérifier si 2 évaluations sont différentes
        /// </summary>
        /// <param name="left">Evaluation : une evaluation</param>
        /// <param name="right">Evaluation : une autre evaluation</param>
        /// <returns></returns>
        public static bool operator !=(Evaluation left, Evaluation right)
        {
            return !(left == right);
        }
    }
}
