using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue
{
    internal class Video
    {
        static int lastId = 0;
        enum Animal
        {
            Chat,
            Chien,
            Crocodile,
            Souris,
            Lapin
        }

        private int _idVideo;
        private string _titre;
        private double _coteEvaluation;
        private List<Evaluation> _listeEvaluations;
        private DateOnly _dateRealisation;
        private double _dureeVideo;
        private string _auteur;
        private string _acteur;
        private string _extrait;
        private string _videoComplet;
        private string _image;

        public Video(string pTitre)
        {
            _idVideo = generateId(); // unique
            _titre = pTitre;
            _coteEvaluation = 0;
            _listeEvaluations = new List<Evaluation>();
            _dateRealisation = new DateOnly();
            _dureeVideo = 0;
            _auteur = "";
            _acteur = "";
            _extrait = "";
            _videoComplet = "";
            _image = "";
        }

        static int generateId()
        {
            return Interlocked.Increment(ref lastId);
        }

        public override string ToString()
        {
            return _titre;
        }

        public override bool Equals(object? obj)
        {
            return obj is Video video &&
                   _idVideo == video._idVideo;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_idVideo);
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
