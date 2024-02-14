

namespace Catalogue
{
    internal class Catalogue
    {
        private List<Video> _listeVideo;

        internal List<Video> ListeVideo { get => _listeVideo; set => _listeVideo = value; }
        

        public Catalogue()
        {
            ListeVideo = new List<Video>();
        }

        public bool AjouterVideo(Video video)
        {
            int longeurInitial = ListeVideo.Count;
            ListeVideo.Add(video);
            return ListeVideo.Count > longeurInitial;

        }

        public List<Video> AjouterJSONVideo(string fichier)
        {
            return this.ListeVideo;
        }

        public bool RemplacerVideo(Video videoARetirer, Video videoAAjouter)
        {
            return true;
        }

        public bool SupprimerVideo(Video video)
        {
            return true;
        }

        private bool SupprimerLeCatalogue()
        {
            return true;
        }

        private void SauvegarderVideos(string fichier)
        {
            
        }

        public override string ToString()
        {
            return base.ToString();
        }



    }
}