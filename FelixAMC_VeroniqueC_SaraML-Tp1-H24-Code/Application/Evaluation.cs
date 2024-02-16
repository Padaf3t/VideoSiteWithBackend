using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCatalogue
{
    public class Evaluation : IEquatable<Evaluation>
    {
        public enum Cote
        {
            Mediocre = 0, Mauvais = 1, Moyen = 2, Bon = 3, Excellent = 4, Extraordinaire = 5
        }

        private Cote _coteDonne;
        private int _idVideo;
        private string _pseudoUtilisateur;
        private string _texteEvaluation;
        private DateTime _dateDeCreation;
        private DateTime? _dateDeModification;

        public Cote CoteDonne { get => _coteDonne; set => _coteDonne = value; }
        public int IdVideo { get => _idVideo; set => _idVideo = value; }
        public string PseudoUtilisateur { get => _pseudoUtilisateur; set => _pseudoUtilisateur = value; }
        public string TexteEvaluation { get => _texteEvaluation; set => _texteEvaluation = value; }
        public DateTime DateDeCreation { get => _dateDeCreation; }
        public DateTime? DateDeModification { get => _dateDeModification; set => _dateDeModification = value; }

        public Evaluation(int pidVideo, string ppseudoUtilisateur, Cote pcote, string ptexte)
        {
            IdVideo = pidVideo; // unique
            PseudoUtilisateur = ppseudoUtilisateur; // unique
            CoteDonne = pcote;
            TexteEvaluation = ptexte;
            _dateDeCreation = System.DateTime.Now;
            DateDeModification = null;

        }

        public override bool Equals(object? obj)
        {
            return obj is Evaluation evaluation && Equals(evaluation);
        }

        public bool Equals(Evaluation other)
        {
            return IdVideo == other.IdVideo &&
                   PseudoUtilisateur == other.PseudoUtilisateur;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdVideo, PseudoUtilisateur);
        }

        public static bool operator ==(Evaluation left, Evaluation right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Evaluation left, Evaluation right)
        {
            return !(left == right);
        }
    }
}
