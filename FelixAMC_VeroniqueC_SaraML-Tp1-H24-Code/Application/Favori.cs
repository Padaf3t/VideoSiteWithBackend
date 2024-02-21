using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCatalogue
{
    public class Favori
    {
        int _idVideo;
        string _pseudoUtilisateur;
        DateTime _dateAjout;

        public int IdVideo { get => _idVideo; set => _idVideo = value; }
        public string PseudoUtilisateur { get => _pseudoUtilisateur; set => _pseudoUtilisateur = value; }
        public DateTime DateAjout { get => _dateAjout;}

        public Favori()
        {
        }

        public Favori(int idVideo, string pseudoUtilisateur)
        {
            _idVideo = idVideo;
            _pseudoUtilisateur = pseudoUtilisateur;
            _dateAjout = System.DateTime.Now;
        }

        public override bool Equals(object? obj)
        {
            return obj is Favori favori &&
                   _idVideo == favori._idVideo &&
                   _pseudoUtilisateur == favori._pseudoUtilisateur;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_idVideo, _pseudoUtilisateur);
        }
    }
}
