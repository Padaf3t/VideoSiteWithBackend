using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ProjetCatalogue.Models
{

    /// <summary>
    /// Classe constituant un catalogue de vidéos (contient une liste de vidéos)
    /// </summary>
    public class Catalogue : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(
            @"Server=(localdb)\MSSQLLocalDB;Database=ProjetCatalogue;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        private DbSet<Video> _listeVideos;

        public DbSet<Video> ListeVideos { get => _listeVideos; set => _listeVideos = value; }

        /// <summary>
        /// Permet l'ajout d'une vidéo à la liste de vidéos du catalogue, après avoir validé que celle-ci n'y est pas déjà
        /// Une erreur sera lancée et attrapée si la vidéo existe déjà
        /// </summary>
        /// <param name="video">La vidéo à ajouter</param>
        /// <returns>bool : true si l'ajout a bien été effectué; false si la vidéo existait déjà</returns>
        public bool AjouterVideo(Video video)
        {
            bool erreurNote = false;

            try
            {
                if (TrouverUneVideo(video.IdVideo) != null)
                {
                    throw new ArgumentException("La video " + video.IdVideo + " existe déjà");
                }

                this.ListeVideos.Add(video);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                erreurNote = true;
            }

            return !erreurNote;

        }

        /// <summary>
        /// Permet de trouver une vidéo selon son id
        /// </summary>
        /// <param name="idVideoAChercher">le id de la vidéo à chercher</param>
        /// <returns>la vidéo trouvée (peut être null si rien trouvé)</returns>
        public Video? TrouverUneVideo(int idVideoAChercher)
        {
            return this.ListeVideos.Where(video => video.IdVideo == idVideoAChercher).FirstOrDefault();
        }

        /// <summary>
        /// Permet d'avoir accès à une query qui contient une liste de vidéos, à partir d'une liste de favoris
        /// </summary>
        /// <param name="listeFavori">Une liste de favoris</param>
        /// <returns>Une liste de vidéos</returns>
        public List<Video> ObtenirListeVideoFavorites(List<Favori> listeFavori)
        {
            IEnumerable < Video > query = 
                from videoTemp in this.ListeVideos
                join favoriTemp in listeFavori
                on videoTemp.IdVideo equals favoriTemp.IdVideo
                select videoTemp;

            return query.ToList();
        }
        
        /// <summary>
        /// Méthode ToString de la classe.
        /// </summary>
        /// <returns>string : chaine contenant la liste des vidéos du catalogue</returns>
        public override string ToString()
        {
            string catalogueVideo = "Catalogue de vidéos animales : \n\n";
            foreach (Video video in this.ListeVideos) 
            { 
                catalogueVideo += video.ToString() + "\n";
            }
            return catalogueVideo;
        }
    }
}
