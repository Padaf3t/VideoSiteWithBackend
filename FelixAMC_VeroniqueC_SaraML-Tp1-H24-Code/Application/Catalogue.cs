

using Newtonsoft.Json;

namespace ProjetCatalogue
{
    internal class Catalogue
    {
        private List<Video> _listeVideo;

        public List<Video> ListeVideo { get => _listeVideo; set => _listeVideo = value; }
        

        public Catalogue()
        {
            ListeVideo = new List<Video>();
        }

        public bool AjouterVideo(Video video)
        {
            this.ListeVideo.Add(video);
            return this.ListeVideo.Last() == video;

        }

        public List<Video> AjouterJSONVideo(string fichier)
        {
            //TODO : gerer erreurs
            List<Video> listeVideos = JsonConvert.DeserializeObject<List<Video>>(File.ReadAllText(@fichier), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            return listeVideos;
        }

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

        public bool SupprimerVideo(Video video)
        {
            return ListeVideo.Remove(video);
        }

        public bool SupprimerLeCatalogue()
        {
            this.ListeVideo.Clear();
            return this.ListeVideo.Count == 0;
        }

        public void SauvegarderVideos(string fichier)
        {
            
            string jsonListe = JsonConvert.SerializeObject(this.ListeVideo, this.ListeVideo.GetType(), Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            File.WriteAllText(@fichier, jsonListe);
        }

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