using Newtonsoft.Json;
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

        /// <summary>
        /// Permet de prendre une liste de favoris et de la sérialiser dans un fichier JSON
        /// </summary>
        /// <param name="fichierJSON">Le fichier JSON à utiliser</param>
        public void SerialisationFavoris(string fichierJSON)
        {

            string jsonListe = JsonConvert.SerializeObject(this.ListeFavoris, this.ListeFavoris.GetType(), Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            File.WriteAllText(@fichierJSON, jsonListe);
        }

        /// <summary>
        /// Méthode qui permet la désérialisation d'un fichier JSON pour en extraire des objets C# Favoris et les placer
        /// dans une liste de favoris
        /// </summary>
        /// <param name="fichierJSON"><Le fichier JSON utilisé/param>
        /// <returns>List(Video): la liste de favoris ainsi créée</returns>
        public List<Video> DeserisalisationJSONFavoris(string fichierJSON)
        {
            //TODO : gerer erreurs
            List<Favori>? listeFavoris = null;
            try
            {

                listeFavoris = JsonConvert.DeserializeObject<List<Favori>>(File.ReadAllText(@fichierJSON), new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Le fichier {0} n'a pas été trouvé", fichierJSON);
            }

            return listeFavoris;
        }
    }
}
