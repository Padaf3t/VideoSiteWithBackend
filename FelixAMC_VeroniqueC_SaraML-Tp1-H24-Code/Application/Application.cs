using Newtonsoft.Json;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        private string pathJSONFavori = "fichierJSON/favoris.JSON";
        private string pathJSONVideo = "fichierJSON/videos.JSON";
        private string pathJSONEvaluation = "fichierJSON/evaluations.JSON";
        private string pathJSONUtilisateur = "fichierJSON/utilisateurs.JSON";

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

            app.populerListes();
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
            this.CatalogueApplication.ListeVideos.Add(new Video(1, "Funny Bunny", EnumAnimal.Lapin, 0, new DateOnly(2010, 10, 21), 0.28,
                "Polo Paulson", "Mr Carrots", "ressources/extraits/1.mp4", "ressources/videos/1.mp4", "ressources/images/1.jpeg"));
            this.CatalogueApplication.ListeVideos.Add(new Video(2, "Grumpy Cat on a talk show", EnumAnimal.Chat, 0, new DateOnly(2017, 05, 10), 1.27,
                "Tamara Tamarin", "Grumpy Cat", "ressources/extraits/2.mp4", "ressources/videos/2.mp4", "ressources/images/2.jpeg"));
            this.CatalogueApplication.ListeVideos.Add(new Video(3, "Grumpy Cat's first pitch", EnumAnimal.Chat, 0, new DateOnly(2019, 07, 06), 1.00,
                "Tamara Tamarin", "Grumpy Cat", "ressources/extraits/3.mp4", "ressources/videos/3.mp4", "ressources/images/3.jpeg"));
            this.CatalogueApplication.ListeVideos.Add(new Video(4, "Playful ferret", EnumAnimal.Furet, 0, new DateOnly(2012, 01, 30), 1.28,
                "Mick McMac", "Jean-Guy le furet", "ressources/extraits/4.mp4", "ressources/videos/4.mp4", "ressources/images/4.jpeg"));
            this.CatalogueApplication.ListeVideos.Add(new Video(5, "Fox likes attention", EnumAnimal.Renard, 0, new DateOnly(2019, 12, 16), 0.52,
                "Tommy Tomtoms", "FoxyFox", "ressources/extraits/5.mp4", "ressources/videos/5.mp4", "ressources/images/5.jpeg"));
            this.CatalogueApplication.ListeVideos.Add(new Video(6, "Keyboard Cat", EnumAnimal.Chat, 0, new DateOnly(2019, 12, 16), 0.54,
                "Michelle Michels", "Keyboard Cat", "ressources/extraits/6.mp4", "ressources/videos/6.mp4", "ressources/images/6.jpeg"));
            this.CatalogueApplication.ListeVideos.Add(new Video(7, "Big Insect", EnumAnimal.Insecte, 0, new DateOnly(2010, 04, 04), 0.21,
                "Georgio Georges", "Mc Roach", "ressources/extraits/7.mp4", "ressources/videos/7.mp4", "ressources/images/7.jpeg"));
            this.CatalogueApplication.ListeVideos.Add(new Video(8, "Rabbit eats lemon", EnumAnimal.Lapin, 0, new DateOnly(2013, 08, 27), 1.38,
                "Stella Steel", "Miss Muffin", "ressources/extraits/8.mp4", "ressources/videos/8.mp4", "ressources/images/8.jpeg"));
            this.CatalogueApplication.ListeVideos.Add(new Video(9, "Raccoon steals carpet", EnumAnimal.Raton, 0, new DateOnly(2008, 06, 29), 1.00,
                "Yan YinYang", "Robber Raccoon", "ressources/extraits/9.mp4", "ressources/videos/9.mp4", "ressources/images/9.jpeg"));
            this.CatalogueApplication.ListeVideos.Add(new Video(10, "Marmot gets a bath", EnumAnimal.Raton, 0, new DateOnly(2023, 01, 05), 2.34,
                "Albert Albertson", "One Small marmot", "ressources/extraits/10.mp4", "ressources/videos/10.mp4", "ressources/images/10.jpeg"));

            for (int i = 0; i < 10; i++)
            {
                string pseudoUser = debutPseudo + i;
                Utilisateur user = new Utilisateur(pseudoUser, "Soleil01!");

                
                this.GestionUtilisateurs.AjouterUtilisateur(user);
                this.GestionEvaluations.AjouterEvaluation(this.CatalogueApplication.ListeVideos[i], user, EnumCote.Moyen, "");
                this.GestionFavoris.AjouterFavori(user, this.CatalogueApplication.ListeVideos[i]);
            }
            this.CatalogueApplication.SetLastId();
        }

        /// <summary>
        /// Setup initial de l'application
        /// Va chercher les fichiers JSON et désérialiser leur contenu afin de les placer dans leur liste respective
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
        /// Permet de sérialiser dans des fichiers JSON les listes de l'application
        /// Devrait être utilisé avant la fermeture de l'application ou à chaque fois que l'on veut sauvegarder des modifications
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
