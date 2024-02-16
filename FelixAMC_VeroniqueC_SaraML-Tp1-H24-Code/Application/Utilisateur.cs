using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCatalogue
{
    public class Utilisateur
    {
        public enum Role
        {
            UtilisateurSimple = 0,
            Technicien = 1,
            Admin = 2
        }
        
        private string _pseudo;
        private string _motDePasse;
        private string _nom;
        private string _prenom;
        private Role _roleUser;
        private List<Video> _listeFavoris;
        private SortedSet<Evaluation> _listeEvaluations;

        public string Pseudo { get => _pseudo; set => _pseudo = value; }
        public string MotDePasse { get => _motDePasse; set => _motDePasse = value; }
        public string Nom { get => _nom; set => _nom = value; }
        public string Prenom { get => _prenom; set => _prenom = value; }
        public Role RoleUser { get => _roleUser; set => _roleUser = value; }
        public List<Video> ListeFavoris { get => _listeFavoris; set => _listeFavoris = value; }
        public SortedSet<Evaluation> ListeEvaluations { get => _listeEvaluations; set => _listeEvaluations = value; }

        public Utilisateur(string pPseudo, string pMotDePasse)
        {
            Pseudo = pPseudo; // unique
            MotDePasse = pMotDePasse;
            Nom = "";
            Prenom = "";
            RoleUser = Role.UtilisateurSimple;
            ListeFavoris = new List<Video>();
            ListeEvaluations = new SortedSet<Evaluation>();
        }

        public bool AjouterFavori(Video video)
        {
            this.ListeFavoris.Add(video);
            return this.ListeFavoris.Last() == video;
        }

        public bool AjouterEvaluation(Video video, Evaluation.Cote cote, string texte)
        {
            Evaluation evaluationActuel = new Evaluation(video.IdVideo, this.Pseudo, cote, texte);

            this.ListeEvaluations.Add(evaluationActuel);
            video.ListeEvaluations.Add(evaluationActuel);

            return this.ListeEvaluations.Last() == evaluationActuel && video.ListeEvaluations.Last() == evaluationActuel;
        }

        public override bool Equals(object? obj)
        {
            return obj is Utilisateur utilisateur &&
                   Pseudo == utilisateur.Pseudo;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Pseudo);
        }


    }
}
