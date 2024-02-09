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
        double coteEvaluation;
        List<Evaluation> evaluations = new List<Evaluation>();
        DateOnly dateRealisation;
        double dureeVideo;
        string auteur;
        string acteur;
        string extrait;
        string complet;
        string image;


    }
}
