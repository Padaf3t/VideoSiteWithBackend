﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjetCatalogue
{

    /// <summary>
    /// Classe qui constitue un utilisateur
    /// </summary>
    public class Utilisateur
    {
        private string _pseudo;
        private string _motDePasse;
        private string _nom;
        private string _prenom;
        private EnumRole _roleUser;

        
        public string Pseudo { 
            get => _pseudo; 
            set {
                    _pseudo = VerifierPseudo(value);
                } 
        }
        
        public string MotDePasse {
            get => _motDePasse;
            set {
                _motDePasse = VerifierMotDePasse(value);
            } 
        }
        public string Nom { get => _nom; set => _nom = value; }
        public string Prenom { get => _prenom; set => _prenom = value; }
        public EnumRole RoleUser { get => _roleUser; set => _roleUser = value; }

        /// <summary>
        /// Constructeur par défaut pour la sérialisation
        /// </summary>
        public Utilisateur()
        {
        }


        /// <summary>
        /// Constructeur de la classe, avec 2 paramètres. Le pseudo défini l'utilisateur. Son nom et prénom sont mis automatiquement
        /// en chaines vides et seront définis plus tard, son rôle est mis par défaut à UtilisateurSimple
        /// </summary>
        /// <param name="pPseudo">le pseudo de l'utilisateur</param>
        /// <param name="pMotDePasse">le mot de passe de l'utilisateur</param>
        public Utilisateur(string pPseudo, string pMotDePasse) : this(pPseudo,pMotDePasse, "", "", EnumRole.UtilisateurSimple)
        {
        }

        /// <summary>
        /// Constructeur de la classe complet. Le pseudo défini l'utilisateur.
        /// 
        /// </summary>
        /// <param name="pPseudo"></param>
        /// <param name="pMotDePasse"></param>
        /// <param name="pNom"></param>
        /// <param name="pPrenom"></param>
        /// <param name="pRoleUser"></param>
        public Utilisateur(string pPseudo, string pMotDePasse, string pNom, string pPrenom, EnumRole pRoleUser)
        {
            Pseudo = pPseudo;
            MotDePasse = pMotDePasse;
            Nom = pNom;
            Prenom = pPrenom;
            RoleUser = pRoleUser;
        }

        /// <summary>
        /// Permet de vérifié si le pseudo contient minimalement 5 charactères à 20 charactères uniquement alphanumérique
        /// </summary>
        /// <param name="pseudo">le pseudo à tester</param>
        /// <returns>Le pseudo entré s'il répond à tous les critères</returns>
        /// <exception cref="ArgumentException"></exception>
        private string VerifierPseudo(string pseudo)
        {

            if (pseudo.Length < 5)
            {
                throw new ArgumentException("Le pseudo " + pseudo + " est trop court");
            }
            else if(pseudo.Length > 20)
            {
                throw new ArgumentException("Le pseudo " + pseudo + " est trop long");
            }
            else if(Regex.Matches(pseudo, "^\\w+$").Count == 0)
            {
                throw new ArgumentException("Le pseudo " + pseudo + " contient un caractère spécial");
            }
            return pseudo;
        }
        /// <summary>
        /// Permet de vérifier si le mot de passe contient minimum 8 charactères dont 1 chiffre et 1 charactère spécial
        /// Elle emmet une erreur si un des critère n'est pas respecté
        /// </summary>
        /// <param name="motDePasse">string : mot de passe a tester</param>
        /// <returns>Le mot de passe s'il répond à tous les critères </returns>
        /// <exception cref="ArgumentException">Lance une exception si un critère n'est pas respecté</exception>
        private string VerifierMotDePasse(string motDePasse)
        {
            if (motDePasse.Length < 8)
            {
                throw new ArgumentException("Le mot de passe est trop court");
            }
            else if (!motDePasse.Any(char.IsDigit))
            {
                throw new ArgumentException("Le mot de passe doit contenir au moins un chiffre");
            }
            else if (Regex.Matches(motDePasse, "^\\w+$").Count != 0)
            {
                throw new ArgumentException("Le mot de passe doit contenir au moins un charactère spécial");
            }
            return motDePasse;
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
