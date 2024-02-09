using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue
{
    internal class Utilisateur
    {
        private string _pseudo;
        private string _motDePasse;
        private string _nom;
        private string _prenom;
        private enum Role
        {
            UtilisateurSimple,
            Admin,
            Technicien
        }
        private List<Video> _listeFavoris;
        private List<Evaluation> _listeEvaluations;

        public Utilisateur(string pPseudo, string pMotDePasse)
        {
            _pseudo = pPseudo;
            _motDePasse = pMotDePasse;
            _nom = "";
            _prenom = "";
            _listeFavoris = new List<Video>();
            _listeEvaluations = new List<Evaluation>();
        }

        private bool AjouterFavori(Video video)
        {
            return true;
        }

        private bool AjouterEvaluation(Video video, Evaluation evaluation)
        {
            return true;
        }

    }
}
