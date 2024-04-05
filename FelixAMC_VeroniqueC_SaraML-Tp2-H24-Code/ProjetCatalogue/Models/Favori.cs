using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCatalogue.Models
{
    /// <summary>
    /// Classe qui constitue une vidéo favorie pour un utilisateur en particulier, opération ayant été faite une date particulière
    /// </summary>
    public class Favori
    {
        int _idVideo;
        string _pseudoUtilisateur;
        DateTime _dateAjout;

        public int IdVideo { get => _idVideo; set => _idVideo = value; }
        public string PseudoUtilisateur { get => _pseudoUtilisateur; set => _pseudoUtilisateur = value; }
        public DateTime DateAjout { get => _dateAjout; }

        /// <summary>
        /// Constructeur par défaut pour la sérialisation
        /// </summary>
        public Favori()
        {
        }

        /// <summary>
        /// Constructeur recevant 2 param qu identifient ensemble de façon unique un favori
        /// </summary>
        /// <param name="idVideo">L'id de la vidéo mise en favori</param>
        /// <param name="pseudoUtilisateur">Le pseudo de l'utilisateur ayant mis en favori</param>
        public Favori(int idVideo, string pseudoUtilisateur)
        {
            IdVideo = idVideo;
            PseudoUtilisateur = pseudoUtilisateur;
            _dateAjout = System.DateTime.Now;
        }

        /// <summary>
        /// Methode Equals de la classe, qui établi si l'objet reçu en paramètre est bien un favori, puis si oui
        /// valide si le favori sur lequel la methode est appelee est le meme que celui recu en paramètre (Equals
        /// si même idVideo et meme pseudoUtilisateur
        /// </summary>
        /// <param name="obj">L'objet qu'on veut vérifier</param>
        /// <returns>bool : true si Equals</returns>
        public override bool Equals(object? obj)
        {
            return obj is Favori favori &&
                   IdVideo == favori.IdVideo &&
                   PseudoUtilisateur == favori.PseudoUtilisateur;
        }

        /// <summary>
        /// Methode GetHashCode de la classe
        /// </summary>
        /// <returns>int : le hashcode généré en fonction du idVideo et du pseudoUtilisateur</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(IdVideo, PseudoUtilisateur);
        }
    }
}
