using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCatalogue
{
    public class GestionFavori
    {
        List<Favori> _listeFavoris;

        public List<Favori> ListeFavoris { get => _listeFavoris; set => _listeFavoris = value; }

        public GestionFavori()
        {
            _listeFavoris = new List<Favori>();
        }

        /// <summary>
        /// Permet l'ajout d'une vidéo à la liste des vidéos favories de l'utilisateur
        /// </summary>
        /// <param name="video">La vidéo à ajouter</param>
        /// <returns>bool : true si l'ajout a bien été fait</returns>
        public bool AjouterFavori(Utilisateur user, Video video)
        {
            Favori nouveauFavori = new Favori(video.IdVideo, user.Pseudo);
            this.ListeFavoris.Add(new Favori(video.IdVideo, user.Pseudo));
            return this.ListeFavoris.Last() == nouveauFavori;
        }
    }
}
