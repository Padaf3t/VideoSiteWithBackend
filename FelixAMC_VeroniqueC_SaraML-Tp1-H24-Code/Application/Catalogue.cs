

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
            //TODO: faire la logique
            return this.ListeVideo;
        }

        public bool RemplacerVideo(Video videoARetirer, Video videoAAjouter)
        {
            //TODO: faire la logique
            return true;
        }

        public bool SupprimerVideo(Video video)
        {
            //TODO: faire la logique
            return true;
        }

        private bool SupprimerLeCatalogue()
        {
            //TODO: faire la logique
            return true;
        }

        private void SauvegarderVideos(string fichier)
        {
            //TODO: faire la logique
        }

        public override string ToString()
        {
            //TODO: faire l'affichage
            return base.ToString();
        }



    }
}