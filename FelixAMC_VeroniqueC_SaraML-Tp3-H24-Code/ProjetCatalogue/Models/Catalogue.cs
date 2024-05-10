using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ProjetCatalogue.Models
{

    /// <summary>
    /// Classe constituant un catalogue de vidéos (contient une liste de vidéos)
    /// </summary>
    public class Catalogue
    {
        private GestionContext _gestionContext;
        private DbSet<Video> _dbSetVideos;

        public DbSet<Video> DbSetVideos { get => _dbSetVideos;}
        public GestionContext GestionContext { get => _gestionContext;}

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        public Catalogue()
        {
            _gestionContext = new GestionContext();
            _dbSetVideos = _gestionContext.Videos;
        }

        /// <summary>
        /// Permet de trouver une vidéo selon son id
        /// </summary>
        /// <param name="idVideoAChercher">le id de la vidéo à chercher</param>
        /// <returns>la vidéo trouvée (peut être null si rien trouvé)</returns>
        public Video? TrouverUneVideo(int idVideoAChercher)
        {
            return this.DbSetVideos.Where(video => video.IdVideo == idVideoAChercher).FirstOrDefault();
        }

        /// <summary>
        /// Permet d'avoir accès à une query qui contient une liste de vidéos, à partir d'une liste de favoris
        /// </summary>
        /// <param name="listeFavori">Une liste de favoris</param>
        /// <returns>Une liste de vidéos</returns>
        public List<Video> ObtenirListeVideoFavorites(List<Favori> listeFavori)
        {
            IEnumerable<Video> query =
                from videoTemp in this.DbSetVideos.ToList()
                join favoriTemp in listeFavori
                on videoTemp.IdVideo equals favoriTemp.IdVideo
                select videoTemp;

            return query.ToList();
        }

    }
}
