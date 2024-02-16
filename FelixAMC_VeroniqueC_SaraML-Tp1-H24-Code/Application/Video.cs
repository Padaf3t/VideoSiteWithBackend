using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCatalogue
{
    internal class Video
    {
        static int lastId = 0;
        public enum Animal
        {
            Indetermine,
            Chat,
            Chien,
            Reptile,
            Rongeur,
            Lapin,
            Arachnide,
            Insecte,
            Oiseau,
            DeFerme,
            Renard,
            Poisson,
            Loutre,
            Furet,
            Cheval,
            Autre
        }

        private int _idVideo;
        private string _titre;
        private Animal _typeVideo;
        private double _coteEvaluation;
        private List<Evaluation> _listeEvaluations;
        private DateOnly _dateRealisation;
        private double _dureeVideo;
        private string _auteur;
        private string _acteur;
        private string _extrait;
        private string _videoComplet;
        private string _image;

        public int IdVideo { get => _idVideo; set => _idVideo = value; }
        public string Titre { get => _titre; set => _titre = value; }
        public Animal TypeVideo { get => _typeVideo; set => _typeVideo = value; }
        public double CoteEvaluation { get => _coteEvaluation; set => _coteEvaluation = value; }
        internal List<Evaluation> ListeEvaluations { get => _listeEvaluations; set => _listeEvaluations = value; }
        public DateOnly? DateRealisation { get => _dateRealisation; set => _dateRealisation = value; }
        public double DureeVideo { get => _dureeVideo; set => _dureeVideo = value; }
        public string Auteur { get => _auteur; set => _auteur = value; }
        public string Acteur { get => _acteur; set => _acteur = value; }
        public string Extrait { get => _extrait; set => _extrait = value; }
        public string VideoComplet { get => _videoComplet; set => _videoComplet = value; }
        public string Image { get => _image; set => _image = value; }
        

        public Video(string pTitre)
        {
            IdVideo = generateId(); // unique
            Titre = pTitre;
            TypeVideo = Animal.Indetermine;
            CoteEvaluation = 0;
            ListeEvaluations = new List<Evaluation>();
            DateRealisation = null;
            DureeVideo = 0;
            Auteur = "";
            Acteur = "";
            Extrait = "";
            VideoComplet = "";
            Image = "";
        }

        static int generateId()
        {
            return Interlocked.Increment(ref lastId);
        }

        public override string ToString()
        {
            //TODO: faire l'affichage
            return Titre;
        }

        public override bool Equals(object? obj)
        {
            return obj is Video video &&
                   IdVideo == video.IdVideo;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdVideo);
        }

        public static bool operator == (Video left, Video right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Video left, Video right)
        {
            return !(left == right);
        }
    }
}
