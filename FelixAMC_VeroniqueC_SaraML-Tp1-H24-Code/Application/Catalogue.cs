

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

        public bool AjouterUtilisateur(Utilisateur user)
        {
            return true;
        }

        private List<Utilisateur> AjouterJSONUtilisateur(string fichier)
        {
            return this._listeUtilisateur;
        }

        public bool AjouterVideo(Video video)
        {
            return true;
            
        }

        private List<Video> AjouterJSONVideo(string fichier)
        {
            return this._listeVideo;
        }

        public bool AjouterJSON(string fichierVideo, string fichierUtilisateur)
        {
            AjouterJSONUtilisateur(fichierUtilisateur);
            AjouterJSONVideo(fichierVideo);
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

        private bool SupprimerLeCatalogue()
        {
            return true;
        }

        private void SauvegarderVideos(string fichier)
        {
            
        }

        private void SauvegarderUtilisateurs(string fichier)
        {

        }

        public void SauvegarderLeCatalogue(string fichierUtilisateur, string fichierVideo)
        {
            SauvegarderUtilisateurs(fichierUtilisateur);
            SauvegarderVideos(fichierVideo);
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