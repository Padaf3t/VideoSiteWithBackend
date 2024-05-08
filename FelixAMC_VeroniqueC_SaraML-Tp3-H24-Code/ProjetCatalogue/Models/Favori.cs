using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetCatalogue.Models
{
    /// <summary>
    /// Classe qui constitue une vidéo favorie pour un utilisateur en particulier, opération ayant été faite une date particulière
    /// </summary>
    public class Favori
    {
        DateTime _dateAjout;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int IdFavori { get; set; }
        
        public int IdVideo { get; set; }
        
        public string PseudoUtilisateur { get; set; }

        public DateTime DateAjout { get => _dateAjout; set => _dateAjout = value; }

        [ForeignKey("IdVideo")]
        public virtual Video Video { get; set; }

        [ForeignKey("PseudoUtilisateur")]
        public virtual Utilisateur Utilisateur { get; set; }

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Favori()
        {
        }

        /// <summary>
        /// Constructeur recevant 2 param qu identifient ensemble de façon unique un favori.
        /// Met la date d'ajout à la date actuelle.
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
