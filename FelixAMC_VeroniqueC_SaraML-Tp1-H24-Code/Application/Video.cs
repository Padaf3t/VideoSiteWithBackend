using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue
{
    internal class Video
    {
        enum Animal
        {
            Chat,
            Chien,
            Crocodile,
            Souris,
            Lapin
        }
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
            _titre = pTitre;
            _coteEvaluation = 0;
            _evaluations = new List<Evaluation>();
            _dateRealisation = "";
            _dureeVideo = 0;
            _auteur = "";
            _acteur = "";
            _extrait = "";
            _videoComplet = "";
            _image = "";
        }

        public static bool operator ==(string pAuteur)
        {
            return true;
        }
        public static bool operator !=(string pAuteur)
        {
            return false;
        }

        private override string ToString()
        {
            return "";
        }
    }
}
