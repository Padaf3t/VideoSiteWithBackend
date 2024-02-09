using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue
{
    internal struct Evaluation
    {
        public enum Cote
        {
            Mediocre = 0, Mauvais = 1, Moyen = 2, Bon = 3, Excellent = 4, Extraordinaire = 5
        }
        public int _idVideo;
        public int _pseudoUtilisateur;
    }
}
