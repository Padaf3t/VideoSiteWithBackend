﻿using Newtonsoft.Json;

namespace ProjetCatalogue.Models
{

    /// <summary>
    /// Classe constituant un catalogue de vidéos (contient une liste de vidéos)
    /// </summary>
    public class Catalogue
    {
        /// <summary>
        /// Champs qui représente le dernier id qui a été attribué à une vidéo dans ce catalogue
        /// </summary>
        private int _lastId;

        private List<Video> _listeVideos;

        public List<Video> ListeVideos { get => _listeVideos; set => _listeVideos = value; }
        public int LastId { get => _lastId; }

        /// <summary>
        /// Constructeur de la classe, sans paramètres. Crée une liste de vidéos vide.
        /// </summary>
        public Catalogue()
        {
            /*this.*/ListeVideos = new List<Video>();
            /*this.*/_lastId = 0;
        }

        /// <summary>
        /// Permet de générer un id pour la vidéo, qui sera incrémenté après le dernier id utilisé pour une vidéo
        /// </summary>
        /// <returns>int : le id généré</returns>
        public int GenerateId()
        {
            return Interlocked.Increment(ref this._lastId);
        }

        /// <summary>
        /// Permet l'ajout d'une vidéo à la liste de vidéos du catalogue, après avoir validé que celle-ci n'y est pas déjà
        /// Une erreur sera lancée et attrapée si la vidéo existe déjà
        /// </summary>
        /// <param name="video">La vidéo à ajouter</param>
        /// <returns>bool : true si l'ajout a bien été effectué; false si la vidéo existait déjà</returns>
        public bool AjouterVideo(Video video)
        {
            IEnumerable<Video> query = QueryPourTrouverVideoSelonId(video.IdVideo);

            bool erreurNote = false;

            try
            {
                if (query.Count() > 0)
                {
                    throw new ArgumentException("La video " + video.IdVideo + " existe déjà");
                }

                this.ListeVideos.Add(video);
                AjusterLastId(video);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                erreurNote = true;
            }

            return !erreurNote;

        }

        /// <summary>
        /// Permet d'avoir accès à une query qui contient une vidéo, selon l'id de vidéo reçu en paramètre
        /// </summary>
        /// <param name="idVideoAChercher">le id de la vidéo à chercher</param>
        /// <returns>un IEnumerable de vidéos</returns>
        private IEnumerable<Video> QueryPourTrouverVideoSelonId(int idVideoAChercher)
        {
            return from videoTemp in this.ListeVideos
                   where videoTemp.IdVideo.Equals(idVideoAChercher)
                   select videoTemp;
        }

        /// <summary>
        /// Permet de trouver une vidéo selon son id
        /// </summary>
        /// <param name="idVideoAChercher">le id de la vidéo à chercher</param>
        /// <returns>la vidéo trouvée (peut être null si rien trouvé)</returns>
        public Video? TrouverUneVideo(int idVideoAChercher)
        {
            return QueryPourTrouverVideoSelonId(idVideoAChercher).Count() > 0 ? QueryPourTrouverVideoSelonId(idVideoAChercher).ToList()[0] : null;
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
            return this.ListeVideos.Remove(video);
        }

        //todo : changer nom pour ViderLeCatalogue ??? plus accurate
        /// <summary>
        /// Permet de vider le catalogue (vider le contenu de sa liste de vidéos)
        /// </summary>
        /// <returns>bool : true si la liste de vidéos est effectivement vide</returns>
       public bool SupprimerLeCatalogue()
        {
            this.ListeVideos.Clear();
            this._lastId = 0;
            return this.ListeVideos.Count == 0;
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
        /// Méthode qui permet la désérialisation d'un fichier JSON pour en extraire des objets C# Video et les placer dans
        /// une liste de vidéos. Lance et attrappe des erreurs si le dossier n'est pas trouvé ou si le fichier n'est pas trouvé.
        /// </summary>
        /// <param name="fichierJSON"><Le fichier JSON utilisé/param>
        public void DeserisalisationJSONVideo(string fichierJSON)
        {
            
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
                Console.WriteLine("Le dossier {0} n'a pas été trouvé", @fichierJSON);
            }
            catch (FileNotFoundException)
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
                _lastId = this.ListeVideos.Max(video => video.IdVideo);
            }
            
        }

        /// <summary>
        /// permet de voir si la video a un plus grand id que le lastId et affecter cette valeur au lastId si tel est le cas
        /// </summary>
        /// <param name="video"></param>
        private void AjusterLastId(Video video) {
            int id = video.IdVideo;
            if(LastId < id)
            {
                _lastId = id;
            }
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
