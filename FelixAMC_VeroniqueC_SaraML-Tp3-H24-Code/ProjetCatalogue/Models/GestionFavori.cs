using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static System.Collections.Specialized.BitVector32;

namespace ProjetCatalogue.Models
{
    /// <summary>
    /// Classe qui permet de gérer une liste de favoris
    /// </summary>
    public class GestionFavori : GestionContext
    {
        public DbSet<Favori> Favoris { get; set; }

        /// <summary>
        /// Permet de modifier un favori pour un utilisateur (l'ajouter ou le supprimer de ses favoris)
        /// </summary>
        /// <param name="user">l'utilisateur pour lequel on fait la modification</param>
        /// <param name="video">la vidéo pour laquelle on fait la modification</param>
        public void ModifierFavori(Utilisateur user, Video video)
        {
            Favori favori = new Favori(video.IdVideo, user.Pseudo);

            IEnumerable<Favori> query =
            from favoriTemp in this.Favoris.ToList()
            where favoriTemp.Equals(favori)
            select favoriTemp;

            bool favoriPresent = query.Count() > 0;

            if (favoriPresent)
            {
                favori = query.First();
                this.Favoris.Remove(favori);
            }
            else
            {
                this.Favoris.Add(favori);
            }

            SaveChanges();
        }

        public void SupprimerFavori(Favori favori)
        {
            if (favori != null)
            {
                this.Favoris.Remove(favori);
                SaveChanges();
            }
        }

        /// <summary>
        /// Permet d'obtenir la liste de favoris d'un utilisateur
        /// </summary>
        /// <param name="user">l'utilisateur</param>
        /// <returns>la liste de favoris de l'utilisateur</returns>
        public List<Favori> ObtenirFavorisUtilisateur(Utilisateur user)
        {

            IEnumerable<Favori> query =
           from favoriTemp in this.Favoris
           where favoriTemp.PseudoUtilisateur.Equals(user.Pseudo)
           select favoriTemp;

            return query.ToList();
        }

        /// <summary>
        /// Permet de dire si une vidéo fait partie de la liste de favoris d'un utilisateur
        /// </summary>
        /// <param name="user">L'utilisateur</param>
        /// <param name="video">La vidéo</param>
        /// <returns>bool: true si la vidéo est dans sa liste de favoris, sinon false</returns>
        public bool FavoriPresent(Utilisateur user, Video video)
        {
            IEnumerable<Favori> query =
           from favoriTemp in this.Favoris
           where favoriTemp.PseudoUtilisateur.Equals(user.Pseudo)
           && favoriTemp.IdVideo.Equals(video.IdVideo)
           select favoriTemp;

           return query.ToList().Count() > 0;
        }
    }
}
