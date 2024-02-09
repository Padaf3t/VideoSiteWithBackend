

namespace Catalogue
{
    internal class Catalogue
    {
        private List<Video> _listeVideo;
        private List<Utilisateur> _listeUtilisateur;

        public Catalogue()
        {
            _listeVideo = new List<Video>();
            _listeUtilisateur = new List<Utilisateur>();
        }

        private bool AjouterVideo(Video video)
        {
            return true;
        }

        private bool AjouterVideo(Video[] videos)
        {
            return true;
        }

        private bool RemplacerVideo(Video videoARetirer, Video videoAAjouter)
        {
            return true;
        }

        private bool SupprimerVideo(Video video)
        {
            return true;
        }

        private bool SauvegarderVideo()
        {
            return true;
        }

        public override string ToString()
        {
            return base.ToString();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        
    }
}