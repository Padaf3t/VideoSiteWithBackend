using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ProjetCatalogue.Models
{
    /// <summary>
    /// Classe qui permet de gérer une liste de favoris
    /// </summary>
    public class GestionFavori : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(
            @"Server=(localdb)\MSSQLLocalDB;Database=Ecole;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }
            DbSet<Favori> _listeFavoris;

        public DbSet<Favori> ListeFavoris { get => _listeFavoris; set => _listeFavoris = value; }

        public GestionFavori()
        {
        }

        public GestionFavori(DbContextOptions options) : base(options)
        {
        }

        //TODO a supprimé et modifier test
        /// <summary>
        /// Permet l'ajout d'une vidéo à la liste des vidéos favories de l'utilisateur
        /// </summary>
        /// <param name="video">La vidéo à ajouter</param>
        /// <returns>bool : true si l'ajout a bien été fait</returns>
        public bool AjouterFavori(Utilisateur user, Video video)
        {
            Favori favori = new Favori(video.IdVideo, user.Pseudo);
            bool erreurNote = false;
            try
            {
                if (ListeFavoris.Add(favori) == null)
                {
                    throw new ArgumentException("L'utilisateur " + user.Pseudo + " a déjà mis la vidéo #" + video.IdVideo + " en favori");
                }
            }catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                erreurNote = true;
            }

            return !erreurNote;

        }

        /// <summary>
        /// Permet de modifier un favori pour un utilisateur (l'ajouter ou le supprimer de ses favoris)
        /// </summary>
        /// <param name="user">l'utilisateur pour lequel on fait la modification</param>
        /// <param name="video">la vidéo pour laquelle on fait la modification</param>
        public void ModifierFavori(Utilisateur user, Video video)
        {
            Favori favori = new Favori(video.IdVideo, user.Pseudo);

            IEnumerable<Favori> query =
            from favoriTemp in this.ListeFavoris
            where favoriTemp.Equals(favori)
            select favoriTemp;

            bool favoriPresent = query.Count() > 0;

            if (favoriPresent)
            {
                this.ListeFavoris.Remove(favori);
            }
            else
            {
                this.ListeFavoris.Add(favori);
            }
        }

        public void SupprimerFavori(Favori favori)
        {
            if (favori != null)
            {
                this.ListeFavoris.Remove(favori);
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
           from favoriTemp in this.ListeFavoris
           where favoriTemp.PseudoUtilisateur.Equals(user.Pseudo)
           select favoriTemp;

            return query.ToList();
        }

        /// <summary>
        /// Permet de dire si une vidéo fait partie de la liste de favoris d'un utilisateur
        /// </summary>
        /// <param name="user">L'utilisateur</param>
        /// <param name="video">La vidéo</param>
        /// <returns>un booléen : true si la vidéo est dans sa liste de favoris, sinon false</returns>
        public bool FavoriPresent(Utilisateur user, Video video)
        {
            IEnumerable<Favori> query =
           from favoriTemp in this.ListeFavoris
           where favoriTemp.PseudoUtilisateur.Equals(user.Pseudo)
           && favoriTemp.IdVideo.Equals(video.IdVideo)
           select favoriTemp;

            return query.ToList().Count() > 0;
        }
    }
}
