﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCatalogue
{
    internal class Utilisateur
    {
        private enum Role
        {
            UtilisateurSimple = 0,
            Technicien = 1,
            Admin = 2
        }
        
        private string _pseudo;
        private string _motDePasse;
        private string _nom;
        private string _prenom;
        private Role _role;
        private List<Video> _listeFavoris;
        private List<Evaluation> _listeEvaluations;

        public string Pseudo { get => _pseudo; set => _pseudo = value; }
        public string MotDePasse { get => _motDePasse; set => _motDePasse = value; }
        public string Nom { get => _nom; set => _nom = value; }
        public string Prenom { get => _prenom; set => _prenom = value; }
        private Role Role1 { get => _role; set => _role = value; }
        internal List<Video> ListeFavoris { get => _listeFavoris; set => _listeFavoris = value; }
        internal List<Evaluation> ListeEvaluations { get => _listeEvaluations; set => _listeEvaluations = value; }

        public Utilisateur(string pPseudo, string pMotDePasse)
        {
            Pseudo = pPseudo; // unique
            MotDePasse = pMotDePasse;
            Nom = "";
            Prenom = "";
            Role1 = Role.UtilisateurSimple;
            ListeFavoris = new List<Video>();
            ListeEvaluations = new List<Evaluation>();
        }

        private bool AjouterFavori(Video video)
        {
            //TODO: faire la logique
            return true;
        }

        private bool AjouterEvaluation(Video video, Evaluation.Cote cote)
        {
            Evaluation evaluationActuel = new Evaluation(video.IdVideo, this.Pseudo, cote);

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
