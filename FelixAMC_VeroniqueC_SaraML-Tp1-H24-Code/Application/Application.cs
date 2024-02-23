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

        private GestionFavori _listeFavoris;
        private GestionEvaluation _listeEvaluations;
        private GestionUtilisateur _listeUtilisateurs;
        private Catalogue _catalogueApplication;

        /// <summary>
        /// Constructeur de la classe, sans paramètre. Crée une liste d'utilisateurs et un catalogue pour l'application
        /// </summary>
        public Application()
        {
            ListeFavoris = new GestionFavori();
            ListeEvaluations = new GestionEvaluation();
            ListeUtilisateurs = new GestionUtilisateur();
            CatalogueApplication = new Catalogue();
        }

        public GestionFavori ListeFavoris { get => _listeFavoris; set => _listeFavoris = value; }
        public GestionEvaluation ListeEvaluations { get => _listeEvaluations; set => _listeEvaluations = value; }
        internal GestionUtilisateur ListeUtilisateurs { get => _listeUtilisateurs; set => _listeUtilisateurs = value; }
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
                this.ListeUtilisateurs.AjouterUtilisateur(user);
                this.ListeEvaluations.AjouterEvaluation(video, user, EnumCote.Moyen, "");
                this.ListeFavoris.AjouterFavori(user, video);
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
            this.ListeUtilisateurs.DeserialisationJSONUtilisateur(this.pathJSONUtilisateur);
            this.ListeEvaluations.DeserisalisationJSONEvaluation(this.pathJSONEvaluation);
            this.ListeFavoris.DeserisalisationJSONFavoris(this.pathJSONFavori);
        }

        /// <summary>
        /// Setup final de l'application
        /// Permet de Sérialisé dans des fichiers JSON les listes de l'application
        /// Devrait être utiliser avant la fermeture de l'application ou à chaque fois que l'on veut sauvegarder des modifications
        /// </summary>
        private void setupFinal()
        {
            this.CatalogueApplication.SerialisationVideos(this.pathJSONVideo);
            this.ListeUtilisateurs.SerialisationUtilisateurs(this.pathJSONUtilisateur);
            this.ListeEvaluations.SerialisationEvaluation(this.pathJSONEvaluation);
            this.ListeFavoris.SerialisationFavoris(this.pathJSONFavori);
        }

        /// <summary>
        /// Permet d'afficher le catalogue de l'application dans la console
        /// </summary>
        private void afficheCatalogue()
        {
            Console.WriteLine(this.CatalogueApplication.ToString());
        }
    }
}
