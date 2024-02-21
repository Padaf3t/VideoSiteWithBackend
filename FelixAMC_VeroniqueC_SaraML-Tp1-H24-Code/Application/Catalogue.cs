

using Newtonsoft.Json;

namespace ProjetCatalogue
{

    /// <summary>
    /// Classe constituant un catalogue de vidéos (contient une liste de vidéos)
    /// </summary>
    internal class Catalogue
    {
        private List<Video> _listeVideo;

        public List<Video> ListeVideo { get => _listeVideo; set => _listeVideo = value; }
        
        /// <summary>
        /// Constructeur de la classe, sans paramètres. Crée une liste de vidéos vide.
        /// </summary>
        public Catalogue()
        {
            ListeVideo = new List<Video>();
        }

        /// <summary>
        /// Permet l'ajout d'une vidéo à la liste de vidéos du catalogue
        /// </summary>
        /// <param name="video">La vidéo à ajouter</param>
        /// <returns>bool : true si l'ajout a bien été effectué</returns>
        public bool AjouterVideo(Video video)
        {
            this.ListeVideo.Add(video);
            return this.ListeVideo.Last() == video;

        }

        /// <summary>
        /// Méthode qui permet la désérialisation d'un fichier JSON pour en extraire des objets C# Video et les placer
        /// dans une liste de vidéos
        /// </summary>
        /// <param name="fichier"><Le fichier JSON utilisé/param>
        /// <returns>List(Video): la liste de vidéos ainsi créée</returns>
        public List<Video> AjouterJSONVideo(string fichier)
        {
            //TODO : gerer erreurs
            List<Video>? listeVideos = null;
            try
            {

                listeVideos = JsonConvert.DeserializeObject<List<Video>>(File.ReadAllText(@fichier), new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
            }catch(FileNotFoundException e)
            {
                Console.WriteLine("Le fichier {0} n'a pas été trouvé", fichier);
            }

            return listeVideos;
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
            if (this.ListeVideo.Contains(videoARetirer))
            {
                index = this.ListeVideo.FindIndex(x => x.IdVideo == videoARetirer.IdVideo);
                this.ListeVideo.Remove(videoARetirer);
                this.ListeVideo.Insert(index, videoAAjouter);
            }
            return this.ListeVideo.Contains(videoAAjouter) && this.ListeVideo.FindIndex(x => x.IdVideo == videoAAjouter.IdVideo) == index;
        }

        /// <summary>
        /// Permet de supprimer une vidéo de la liste de vidéos du catalogue
        /// </summary>
        /// <param name="video">La vidéo à supprimer</param>
        /// <returns>bool: true si la suppression a bien été faite</returns>
        public bool SupprimerVideo(Video video)
        {
            return ListeVideo.Remove(video);
        }

        //todo : changer nom pour ViderLeCatalogue ??? plus accurate
        /// <summary>
        /// Permet de vider le catalogue (vider le contenu de sa liste de vidéos)
        /// </summary>
        /// <returns>bool : true si la liste de vidéos est effectivement vide</returns>
        public bool SupprimerLeCatalogue()
        {
            this.ListeVideo.Clear();
            return this.ListeVideo.Count == 0;
        }

        /// <summary>
        /// Permet de prendre une liste de vidéos et de la sérialiser dans un fichier JSON
        /// </summary>
        /// <param name="fichier">Le fichier JSON à utiliser</param>
        public void SauvegarderVideos(string fichier)
        {
            
            string jsonListe = JsonConvert.SerializeObject(this.ListeVideo, this.ListeVideo.GetType(), Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            File.WriteAllText(@fichier, jsonListe);
        }

        //todo: implémenter
        /// <summary>
        /// Parcourt la liste de vidéos du catalogue pour trouver celle avec le plus grand id
        /// </summary>
        /// <returns>int : le id de la vidéo ayant le plus grand id</returns>
        public int TrouverLePlusGrandIdDeLaListeDeVideo()
        {
            return 0;
        }

        /// <summary>
        /// Méthode ToString de la classe.
        /// </summary>
        /// <returns>string : chaine contenant la liste des vidéos du catalogue</returns>
        public override string ToString()
        {
            string catalogueVideo = "";
            foreach (Video video in this.ListeVideo) 
            { 
                catalogueVideo += video.ToString() + "\n";
            }
            return catalogueVideo;
        }




    }
}