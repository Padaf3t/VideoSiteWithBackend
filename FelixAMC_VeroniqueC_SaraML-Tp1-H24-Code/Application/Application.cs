using Newtonsoft.Json;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCatalogue
{

    /// <summary>
    /// Classe qui constitue l'application, c'est la classe principale qui contient le Main donc l'entrée dans le programme
    /// </summary>
    public class Application
    {
        private string pathJSONFavori = "fichierJSON/favoris.JSON";
        private string pathJSONVideo = "fichierJSON/videos.JSON";
        private string pathJSONEvaluation = "fichierJSON/evaluations.JSON";
        private string pathJSONUtilisateur = "fichierJSON/utilisateurs.JSON";

        private GestionFavori _gestionFavoris;
        private GestionEvaluation _gestionEvaluations;
        private GestionUtilisateur _gestionUtilisateurs;
        private Catalogue _catalogueApplication;

        /// <summary>
        /// Constructeur de la classe, sans paramètre. Crée une liste d'utilisateurs et un catalogue pour l'application
        /// </summary>
        public Application()
        {
            GestionFavoris = new GestionFavori();
            GestionEvaluations = new GestionEvaluation();
            GestionUtilisateurs = new GestionUtilisateur();
            CatalogueApplication = new Catalogue();
        }

        public GestionFavori GestionFavoris { get => _gestionFavoris; set => _gestionFavoris = value; }
        public GestionEvaluation GestionEvaluations { get => _gestionEvaluations; set => _gestionEvaluations = value; }
        internal GestionUtilisateur GestionUtilisateurs { get => _gestionUtilisateurs; set => _gestionUtilisateurs = value; }
        internal Catalogue CatalogueApplication { get => _catalogueApplication; set => _catalogueApplication = value; }

        
        /// <summary>
        /// Méthode d'entrée dans le programme
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Application app = new Application();

            app.setupInitial();

            app.populerListes();
            app.afficheCatalogue();

            app.setupFinal();
        }

        /// <summary>
        /// Méthode de test de début d'application
        /// Méthode à être utilisé seulement pour peupler au depart les listes de l'application.
        /// Ne pas utiliser si vous ne controler pas la ligne d'entrée
        /// </summary>
        private void populerListes()
        {
            Console.WriteLine("Entrez un début de pseudo unique");
            string debutPseudo = Console.ReadLine();

            for(int i = 0; i < 10; i++)
            {
                Video video = new Video(this.CatalogueApplication.GenerateId());
                string pseudoUser = debutPseudo + i;
                Utilisateur user = new Utilisateur(pseudoUser, "Soleil01!");

                this.CatalogueApplication.ListeVideos.Add(video);
                this.GestionUtilisateurs.AjouterUtilisateur(user);
                this.GestionEvaluations.AjouterEvaluation(video, user, EnumCote.Moyen, "");
                this.GestionFavoris.AjouterFavori(user, video);
            }
        }

        /// <summary>
        /// Setup initial de l'application
        /// Va chercher les fichier JSON et désérialiser leur contenu afin de les placer dans leur liste respective
        /// </summary>
        private void setupInitial()
        {
            this.CatalogueApplication.DeserisalisationJSONVideo(this.pathJSONVideo);
            this.CatalogueApplication.SetLastId();
            this.GestionUtilisateurs.DeserialisationJSONUtilisateur(this.pathJSONUtilisateur);
            this.GestionEvaluations.DeserisalisationJSONEvaluation(this.pathJSONEvaluation);
            this.GestionFavoris.DeserisalisationJSONFavoris(this.pathJSONFavori);
        }

        /// <summary>
        /// Setup final de l'application
        /// Permet de Sérialisé dans des fichiers JSON les listes de l'application
        /// Devrait être utiliser avant la fermeture de l'application ou à chaque fois que l'on veut sauvegarder des modifications
        /// </summary>
        private void setupFinal()
        {
            this.CatalogueApplication.SerialisationVideos(this.pathJSONVideo);
            this.GestionUtilisateurs.SerialisationUtilisateurs(this.pathJSONUtilisateur);
            this.GestionEvaluations.SerialisationEvaluation(this.pathJSONEvaluation);
            this.GestionFavoris.SerialisationFavoris(this.pathJSONFavori);
        }

        /// <summary>
        /// Permet d'afficher le catalogue de l'application dans la console
        /// </summary>
        private void afficheCatalogue()
        {
            associerBonneCotePourChaqueVideo();
            Console.WriteLine(this.CatalogueApplication.ToString());
        }

        private void associerBonneCotePourChaqueVideo()
        {
            foreach (Video video in this.CatalogueApplication.ListeVideos)
            {
                video.calculerCoteEvaluation(this.GestionEvaluations.ListeEvaluations);
            }
        }
    }
}
