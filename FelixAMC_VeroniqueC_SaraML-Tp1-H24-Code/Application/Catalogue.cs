

using Newtonsoft.Json;
using System.Runtime.Intrinsics;

namespace ProjetCatalogue
{

    /// <summary>
    /// Classe constituant un catalogue de vidéos (contient une liste de vidéos)
    /// </summary>
    internal class Catalogue
    {
        /// <summary>
        /// Attribut statique qui représente le dernier id qui a été attribué à une vidéo
        /// </summary>
        static private int lastId = 0;

        private List<Video> _listeVideos;

        public List<Video> ListeVideos { get => _listeVideos; set => _listeVideos = value; }
        
        /// <summary>
        /// Constructeur de la classe, sans paramètres. Crée une liste de vidéos vide.
        /// </summary>
        public Catalogue()
        {
            ListeVideos = new List<Video>();
        }

        /// <summary>
        /// Permet de générer un id pour la vidéo, qui sera incrémenté après le dernier id utilisé pour une vidéo
        /// </summary>
        /// <returns>int : le id généré</returns>
        public int GenerateId()
        {
            return Interlocked.Increment(ref lastId);
        }

        /// <summary>
        /// Permet l'ajout d'une vidéo à la liste de vidéos du catalogue
        /// </summary>
        /// <param name="video">La vidéo à ajouter</param>
        /// <returns>bool : true si l'ajout a bien été effectué</returns>
        public bool AjouterVideo(Video video)
        {
            this.ListeVideos.Add(video);
            return this.ListeVideos.Last() == video;

        }

        /// <summary>
        /// Permet de remplacer une vidéo par une autre dans la liste de vidéos du catalogue
        /// </summary>
        /// <param name="videoARetirer">La vidéo à retirer</param>
        /// <param name="videoAAjouter">La vidéo à rajouter</param>
        /// <returns>bool : true si le remplacement a bien été effectué</returns>
        public bool RemplacerVideo(Video videoARetirer, Video videoAAjouter)
        {
            int index = 0;
            if (this.ListeVideos.Contains(videoARetirer))
            {
                index = this.ListeVideos.FindIndex(x => x.IdVideo == videoARetirer.IdVideo);
                this.ListeVideos.Remove(videoARetirer);
                this.ListeVideos.Insert(index, videoAAjouter);
            }
            return this.ListeVideos.Contains(videoAAjouter) && this.ListeVideos.FindIndex(x => x.IdVideo == videoAAjouter.IdVideo) == index;
        }

        /// <summary>
        /// Permet de supprimer une vidéo de la liste de vidéos du catalogue
        /// </summary>
        /// <param name="video">La vidéo à supprimer</param>
        /// <returns>bool: true si la suppression a bien été faite</returns>
        public bool SupprimerVideo(Video video)
        {
            return ListeVideos.Remove(video);
        }

        //todo : changer nom pour ViderLeCatalogue ??? plus accurate
        /// <summary>
        /// Permet de vider le catalogue (vider le contenu de sa liste de vidéos)
        /// </summary>
        /// <returns>bool : true si la liste de vidéos est effectivement vide</returns>
        public bool SupprimerLeCatalogue()
        {
            this.ListeVideos.Clear();
            return this.ListeVideos.Count == 0;
        }

        /// <summary>
        /// Permet de prendre une liste de vidéos et de la sérialiser dans un fichier JSON
        /// </summary>
        /// <param name="fichierJSON">Le fichier JSON à utiliser</param>
        public void SerialisationVideos(string fichierJSON)
        {
            
            string jsonListe = JsonConvert.SerializeObject(this.ListeVideos, this.ListeVideos.GetType(), Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            File.WriteAllText(@fichierJSON, jsonListe);
        }

        /// <summary>
        /// Méthode qui permet la désérialisation d'un fichier JSON pour en extraire des objets C# Video et les placer
        /// dans une liste de vidéos
        /// </summary>
        /// <param name="fichierJSON"><Le fichier JSON utilisé/param>
        public void DeserisalisationJSONVideo(string fichierJSON)
        {
            //TODO : gerer erreurs
            List<Video>? liste = null;
            try
            {

                liste = JsonConvert.DeserializeObject<List<Video>>(File.ReadAllText(@fichierJSON), new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
            }
            catch (DirectoryNotFoundException)
            {
                
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Le fichier {0} n'a pas été trouvé", @fichierJSON);
            }
            finally
            {
                if (liste != null)
                {
                    this.ListeVideos = liste;
                }
            }
        }

        /// <summary>
        /// Parcourt la liste de vidéos du catalogue pour trouver celle avec le plus grand id pour setter le lastId
        /// </summary>
        public void SetLastId()
        {
            if(ListeVideos.Count > 0)
            {
                this.ListeVideos.Sort();
                lastId = this.ListeVideos.Last().IdVideo;
            }
            
        }

        /// <summary>
        /// Méthode ToString de la classe.
        /// </summary>
        /// <returns>string : chaine contenant la liste des vidéos du catalogue</returns>
        public override string ToString()
        {
            string catalogueVideo = "";
            foreach (Video video in this.ListeVideos) 
            { 
                catalogueVideo += video.ToString() + "\n";
            }
            return catalogueVideo;
        }




    }
}