using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCatalogue.Models
{
    /// <summary>
    /// Classe qui permet de gérer une liste de favoris
    /// </summary>
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
            Favori favori = new Favori(video.IdVideo, user.Pseudo);

            IEnumerable<Favori> query =
            from favoriTemp in this.ListeFavoris
            where favoriTemp.Equals(favori)
            select favoriTemp;

            bool erreurNote = false;

            try
            {
                if (query.Count() > 0)
                {
                    throw new ArgumentException("L'utilisateur " + user.Pseudo + " a déjà mis la vidéo #" + video.IdVideo + " en favori");
                }

                this.ListeFavoris.Add(favori);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                erreurNote = true;
            }

            return !erreurNote;

        }

        public List<Favori> ObtenirFavorisUtilisateur(Utilisateur user)
        {
            
            IEnumerable<Favori> query =
           from favoriTemp in this.ListeFavoris
           where favoriTemp.PseudoUtilisateur.Equals(user.Pseudo)
           select favoriTemp;

            return query.ToList();
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
        /// soulève une exception si le dossier n'est pas trouvé ou que le fichier n'est pas trouver
        /// </summary>
        /// <param name="fichierJSON"><Le fichier JSON utilisé/param>
        public void DeserisalisationJSONFavoris(string fichierJSON)
        {
            //TODO : gerer erreurs
            List<Favori>? liste = null;
            try
            {

                liste = JsonConvert.DeserializeObject<List<Favori>>(File.ReadAllText(@fichierJSON), new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Le dossier {0} n'a pas été trouvé", @fichierJSON);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Le fichier {0} n'a pas été trouvé", fichierJSON);
            }
            finally
            {
                if (liste != null)
                {
                    this.ListeFavoris = liste;
                }
            }
        }
    }
}
