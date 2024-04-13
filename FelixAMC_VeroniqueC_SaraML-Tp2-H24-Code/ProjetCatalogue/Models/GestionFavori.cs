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

        //TODO a supprimé et modifier test
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
