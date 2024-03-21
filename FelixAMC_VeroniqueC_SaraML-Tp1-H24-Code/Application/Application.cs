using Newtonsoft.Json;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCatalogue
{

    /// <summary>
    /// Classe qui constitue l'application, c'est la classe principale qui contient le Main donc l'entrée dans le programme
    /// </summary>
    public class Application
    {
        private const string PathJSONFavori = "fichierJSON/favoris.JSON";
        private const string PathJSONVideo = "fichierJSON/videos.JSON";
        private const string PathJSONEvaluation = "fichierJSON/evaluations.JSON";
        private const string PathJSONUtilisateur = "fichierJSON/utilisateurs.JSON";

        private GestionFavori _gestionFavoris;
        private GestionEvaluation _gestionEvaluations;
        private GestionUtilisateur _gestionUtilisateurs;
        private Catalogue _catalogueApplication;

        /// <summary>
        /// Constructeur de la classe, sans paramètre. Crée un catalogue pour l'application, ainsi qu'un gestionnaire de favoris,
        /// un gestionnaire d'évaluations et un gestionnaire d'utilisateurs
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
            if (app.CatalogueApplication.ListeVideos.Count() == 0)
            {
                app.populerListes();
            }
            else { 
                app.CatalogueApplication.AjouterVideo(new Video(app.CatalogueApplication.GenerateId()));
            }
            
            app.afficheCatalogue();

            app.setupFinal();
        }

        /// <summary>
        /// Méthode de test de début d'application
        /// Méthode à être utilisée seulement pour peupler au depart les listes de l'application.
        /// Ne pas utiliser si vous ne controlez pas la ligne d'entrée.
        /// C'est ici que sont créés manuellement nos 10 vidéos
        /// </summary>
        private void populerListes()
        {
            Console.WriteLine("Entrez un début de pseudo unique");
            string debutPseudo = Console.ReadLine();

            //ajout de toutes les vidéos avec leurs champs manuellement
            this.CatalogueApplication.AjouterVideo(new Video(1, "Funny Bunny", EnumAnimal.Lapin, 0, new DateOnly(2010, 10, 21), 0.28,
                "Polo Paulson", "Mr Carrots", "1.mp4", "1.mp4", "1.jpeg"));
            this.CatalogueApplication.AjouterVideo(new Video(2, "Grumpy Cat on a talk show", EnumAnimal.Chat, 0, new DateOnly(2017, 05, 10), 1.27,
                "Tamara Tamarin", "Grumpy Cat", "2.mp4", "2.mp4", "2.jpeg"));
            this.CatalogueApplication.AjouterVideo(new Video(3, "Grumpy Cat's first pitch", EnumAnimal.Chat, 0, new DateOnly(2019, 07, 06), 1.00,
                "Tamara Tamarin", "Grumpy Cat", "3.mp4", "3.mp4", "3.jpeg"));
            this.CatalogueApplication.AjouterVideo(new Video(4, "Playful ferret", EnumAnimal.Furet, 0, new DateOnly(2012, 01, 30), 1.28,
                "Mick McMac", "Jean-Guy le furet", "4.mp4", "4.mp4", "4.jpeg"));
            this.CatalogueApplication.AjouterVideo(new Video(5, "Fox likes attention", EnumAnimal.Renard, 0, new DateOnly(2019, 12, 16), 0.52,
                "Tommy Tomtoms", "FoxyFox", "5.mp4", "5.mp4", "5.jpeg"));
            this.CatalogueApplication.AjouterVideo(new Video(6, "Keyboard Cat", EnumAnimal.Chat, 0, new DateOnly(2019, 12, 16), 0.54,
                "Michelle Michels", "Keyboard Cat", "6.mp4", "6.mp4", "6.jpeg"));
            this.CatalogueApplication.AjouterVideo(new Video(7, "Big Insect", EnumAnimal.Insecte, 0, new DateOnly(2010, 04, 04), 0.21,
                "Georgio Georges", "Mc Roach", "7.mp4", "7.mp4", "7.jpeg"));
            this.CatalogueApplication.AjouterVideo(new Video(8, "Rabbit eats lemon", EnumAnimal.Lapin, 0, new DateOnly(2013, 08, 27), 1.38,
                "Stella Steel", "Miss Muffin", "8.mp4", "8.mp4", "8.jpeg"));
            this.CatalogueApplication.AjouterVideo(new Video(9, "Raccoon steals carpet", EnumAnimal.Raton, 0, new DateOnly(2008, 06, 29), 1.00,
                "Yan YinYang", "Robber Raccoon", "9.mp4", "9.mp4", "9.jpeg"));
            this.CatalogueApplication.AjouterVideo(new Video(10, "Marmot gets a bath", EnumAnimal.Raton, 0, new DateOnly(2023, 01, 05), 2.34,
                "Albert Albertson", "One Small marmot", "10.mp4", "10.mp4", "10.jpeg"));

            //for (int i = 0; i < 10; i++)
            //{
            //    string pseudoUser = debutPseudo + i;
            //    Utilisateur user = new Utilisateur(pseudoUser, "Soleil01!");

                
            //    this.GestionUtilisateurs.AjouterUtilisateur(user);
            //    for(int j = 0; j < 10; j++)
            //    {
            //        this.GestionFavoris.AjouterFavori(user, this.CatalogueApplication.ListeVideos[j]);
            //        this.GestionEvaluations.AjouterEvaluation(this.CatalogueApplication.ListeVideos[j], user, (EnumCote)(j%6), "");
            //    }
                
                
            //}
        }

        /// <summary>
        /// Setup initial de l'application
        /// Va chercher les fichiers JSON et désérialiser leur contenu afin de les placer dans leur liste respective
        /// </summary>
        private void setupInitial()
        {
            this.CatalogueApplication.DeserisalisationJSONVideo(Application.PathJSONVideo);
            this.CatalogueApplication.SetLastId();
            this.GestionUtilisateurs.DeserialisationJSONUtilisateur(Application.PathJSONUtilisateur);
            this.GestionEvaluations.DeserisalisationJSONEvaluation(Application.PathJSONEvaluation);
            this.GestionFavoris.DeserisalisationJSONFavoris(Application.PathJSONFavori);
        }

        /// <summary>
        /// Setup final de l'application
        /// Permet de sérialiser dans des fichiers JSON les listes de l'application
        /// Devrait être utilisé avant la fermeture de l'application ou à chaque fois que l'on veut sauvegarder des modifications
        /// </summary>
        private void setupFinal()
        {
            this.CatalogueApplication.SerialisationVideos(Application.PathJSONVideo);
            this.GestionUtilisateurs.SerialisationUtilisateurs(Application.PathJSONUtilisateur);
            this.GestionEvaluations.SerialisationEvaluation(Application.PathJSONEvaluation);
            this.GestionFavoris.SerialisationFavoris(Application.PathJSONFavori);
        }

        /// <summary>
        /// Permet d'afficher le catalogue de l'application dans la console
        /// </summary>
        private void afficheCatalogue()
        {
            associerBonneCotePourChaqueVideo();
            Console.WriteLine(this.CatalogueApplication.ToString());
        }

        /// <summary>
        /// Parcourt la liste de vidéos du catalogue pour appeler la méthode calculerCoteEvaluation de la classe
        /// Video sur chacune d'entre elles
        /// </summary>
        private void associerBonneCotePourChaqueVideo()
        {
            foreach (Video video in this.CatalogueApplication.ListeVideos)
            {
                video.calculerCoteEvaluation(this.GestionEvaluations.ListeEvaluations);
            }
        }
    }
}
