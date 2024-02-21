using NUnit.Framework;
using System;
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
        private string _pseudo;
        private string _motDePasse;
        private string _nom;
        private string _prenom;
        private EnumRole _roleUser;
        private SortedSet<Evaluation> _listeEvaluations;

        //todo: validation du pseudo en validant que pas deja ds liste pseudos des utilisateurs car unique + selon critères caractères
        public string Pseudo { get => _pseudo; set => _pseudo = value; }
        //todo: validation mot de passe selon critères caractères
        public string MotDePasse { get => _motDePasse; set => _motDePasse = value; }
        public string Nom { get => _nom; set => _nom = value; }
        public string Prenom { get => _prenom; set => _prenom = value; }
        public EnumRole RoleUser { get => _roleUser; set => _roleUser = value; }
        public SortedSet<Evaluation> ListeEvaluations { get => _listeEvaluations; set => _listeEvaluations = value; }

        public Utilisateur()
        {
        }

        //todo: notes de cours disent que Les objets à sérialiser doivent offrir un constructeur par défaut;;;;;;
        /// <summary>
        /// Constructeur de la classe, avec 2 paramètres. Le pseudo est unique. Son nom et prénom sont mis automatiquement
        /// en chaines vides et seront définis plus tard, son rôle est mis par défaut à UtilisateurSimple, sa liste de favoris
        /// ainsi que sa liste d'évaluations sont créées (listes vides)
        /// </summary>
        /// <param name="pPseudo">le pseudo de l'utilisateur</param>
        /// <param name="pMotDePasse">le mot de passe de l'utilisateur</param>
        public Utilisateur(string pPseudo, string pMotDePasse) : this(pPseudo,pMotDePasse, "", "", EnumRole.UtilisateurSimple)
        {
        }

        public Utilisateur(string pPseudo, string pMotDePasse, string pNom, string pPrenom, EnumRole pRoleUser)
        {
            _pseudo = pPseudo;
            _motDePasse = pMotDePasse;
            _nom = pNom;
            _prenom = pPrenom;
            _roleUser = pRoleUser;
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
