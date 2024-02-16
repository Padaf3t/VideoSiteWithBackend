﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCatalogue
{
    /// <summary>
    /// Possede un constructeur inhérent sans param, mais il faut toujours passer par le constructeur avec paramètres
    /// </summary>
    public struct Evaluation : IEquatable<Evaluation>
    {
        public enum Cote
        {
            Mediocre = 0, Mauvais = 1, Moyen = 2, Bon = 3, Excellent = 4, Extraordinaire = 5
        }

        public Cote _cote;
        public int _idVideo;
        public string _pseudoUtilisateur;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pidVideo"></param>
        /// <param name="ppseudoUtilisateur"></param>
        /// <param name="pcote">La cote de L'évaluation : doit être un Enum (Evaluation.Cote) entre 0 et 5</param>
        public Evaluation(int pidVideo, string ppseudoUtilisateur, Cote pcote)
        {
            _idVideo = pidVideo; // unique
            _pseudoUtilisateur = ppseudoUtilisateur; // unique
            _cote = pcote;
            
        }

        public override bool Equals(object? obj)
        {
            return obj is Evaluation evaluation && Equals(evaluation);
        }

        public bool Equals(Evaluation other)
        {
            return _idVideo == other._idVideo &&
                   _pseudoUtilisateur == other._pseudoUtilisateur;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_idVideo, _pseudoUtilisateur);
        }

        public static bool operator ==(Evaluation left, Evaluation right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Evaluation left, Evaluation right)
        {
            return !(left == right);
        }
    }
}
