﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ProjetCatalogue.Models
{

    /// <summary>
    /// Classe constituant un catalogue de vidéos (contient une liste de vidéos)
    /// </summary>
    public class Catalogue : DbContext
    {

        public DbSet<Video> Videos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(
            @"Server=(localdb)\MSSQLLocalDB;Database=ProjetCatalogue;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Permet de trouver une vidéo selon son id
        /// </summary>
        /// <param name="idVideoAChercher">le id de la vidéo à chercher</param>
        /// <returns>la vidéo trouvée (peut être null si rien trouvé)</returns>
        public Video? TrouverUneVideo(int idVideoAChercher)
        {
            return this.Videos.Where(video => video.IdVideo == idVideoAChercher).FirstOrDefault();
        }

        /// <summary>
        /// Permet d'avoir accès à une query qui contient une liste de vidéos, à partir d'une liste de favoris
        /// </summary>
        /// <param name="listeFavori">Une liste de favoris</param>
        /// <returns>Une liste de vidéos</returns>
        public List<Video> ObtenirListeVideoFavorites(List<Favori> listeFavori)
        {
            IEnumerable<Video> query =
                from videoTemp in this.Videos.ToList()
                join favoriTemp in listeFavori
                on videoTemp.IdVideo equals favoriTemp.IdVideo
                select videoTemp;

            return query.ToList();
        }

    }
}
