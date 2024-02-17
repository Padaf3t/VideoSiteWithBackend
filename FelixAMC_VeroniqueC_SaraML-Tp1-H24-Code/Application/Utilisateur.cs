﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCatalogue
{

    /// <summary>
    /// Classe qui constitue un utilisateur, avec son rôle, son pseudo, son mot de passe, son nom et prénom,
    /// sa liste de vidéos favoris et la liste de ses évaluations qu'il a faites
    /// </summary>
    public class Utilisateur
    {

        /// <summary>
        /// Les rôles que peut prendre un utilisateur; 3 options
        /// </summary>
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

        //todo: validation du pseudo en validant que pas deja ds liste pseudos des utilisateurs car unique + selon critères caractères
        public string Pseudo { get => _pseudo; set => _pseudo = value; }
        //todo: validation mot de passe selon critères caractères
        public string MotDePasse { get => _motDePasse; set => _motDePasse = value; }
        public string Nom { get => _nom; set => _nom = value; }
        public string Prenom { get => _prenom; set => _prenom = value; }
        public Role RoleUser { get => _roleUser; set => _roleUser = value; }
        public List<Video> ListeFavoris { get => _listeFavoris; set => _listeFavoris = value; }
        public SortedSet<Evaluation> ListeEvaluations { get => _listeEvaluations; set => _listeEvaluations = value; }

        /// <summary>
        /// Constructeur de la classe, avec 2 paramètres. Le pseudo est unique. Son nom et prénom sont mis automatiquement
        /// en chaines vides et seront définis plus tard, son rôle est mis par défaut à UtilisateurSimple, sa liste de favoris
        /// ainsi que sa liste d'évaluations sont créées (listes vides)
        /// </summary>
        /// <param name="pPseudo">le pseudo de l'utilisateur</param>
        /// <param name="pMotDePasse">le mot de passe de l'utilisateur</param>
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

        /// <summary>
        /// Permet l'ajout d'une vidéo à la liste des vidéos favories de l'utilisateur
        /// </summary>
        /// <param name="video">La vidéo à ajouter</param>
        /// <returns>bool : true si l'ajout a bien été fait</returns>
        public bool AjouterFavori(Video video)
        {
            this.ListeFavoris.Add(video);
            return this.ListeFavoris.Last() == video;
        }

        /// <summary>
        /// Permet l'ajout d'une évaluation à la liste des évaluations de l'utilisateur
        /// </summary>
        /// <param name="video">La vidéo évaluée</param>
        /// <param name="cote">La cote attribuée</param>
        /// <param name="texte">Le texte que l'utilisateur a écrit pour son évaluation</param>
        /// <returns>bool : true si l'évaluation a bien été ajoutée à sa liste</returns>
        public bool AjouterEvaluation(Video video, Evaluation.Cote cote, string texte)
        {
            Evaluation evaluationActuel = new Evaluation(video.IdVideo, this.Pseudo, cote, texte);

            this.ListeEvaluations.Add(evaluationActuel);
            video.ListeEvaluations.Add(evaluationActuel);

            return this.ListeEvaluations.Last() == evaluationActuel && video.ListeEvaluations.Last() == evaluationActuel;
        }

        /// <summary>
        /// Methode Equals de la classe, qui valide si l'objet reçu en param est bien un utilisateur et si c'est le même
        /// utilisateur que celui sur lequel la méthode a été appelée (selon les pseudos)
        /// </summary>
        /// <param name="obj">L'objet à comparer à l'utilisateur sur lequel la méthode a été appelée</param>
        /// <returns>bool : true si ce sont le même utilisateur</returns>
        public override bool Equals(object? obj)
        {
            return obj is Utilisateur utilisateur &&
                   Pseudo == utilisateur.Pseudo;
        }

        /// <summary>
        /// Méthode GetHashCode de la classe, génère hashcode sur la base du pseudo
        /// </summary>
        /// <returns>int : le hashcode généré</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Pseudo);
        }


    }
}
