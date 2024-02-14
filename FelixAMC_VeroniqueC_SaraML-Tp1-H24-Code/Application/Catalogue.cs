

namespace Catalogue
{
    internal class Catalogue
    {
        private List<Video> _listeVideo;
        private List<Utilisateur> _listeUtilisateur;

        internal List<Video> ListeVideo { get => _listeVideo; set => _listeVideo = value; }
        internal List<Utilisateur> ListeUtilisateur { get => _listeUtilisateur; set => _listeUtilisateur = value; }

        public Catalogue()
        {
            ListeVideo = new List<Video>();
            ListeUtilisateur = new List<Utilisateur>();
        }

        public bool AjouterUtilisateur(Utilisateur user)
        {
            int longeurInitial = ListeUtilisateur.Count;
            ListeUtilisateur.Add(user);
            return ListeUtilisateur.Count > longeurInitial;
        }

        private List<Utilisateur> AjouterJSONUtilisateur(string fichier)
        {
            return this.ListeUtilisateur;
        }

        public bool AjouterVideo(Video video)
        {
            int longeurInitial = ListeVideo.Count;
            ListeVideo.Add(video);
            return ListeVideo.Count > longeurInitial;

        }

        private List<Video> AjouterJSONVideo(string fichier)
        {
            return this.ListeVideo;
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