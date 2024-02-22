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
            //for(int i = 0; i < 10; i++)
            //{
            //    app.CatalogueApplication.AjouterVideo(new Video());
            //}
            //app.CatalogueApplication.SerialisationVideos("listeVideo.JSON");
            app.CatalogueApplication.ListeVideos =  app.CatalogueApplication.DeserisalisationJSONVideo("listeVideo.JSON");
            app.CatalogueApplication.SetLastId();
            app.CatalogueApplication.ListeVideos.Add(new Video(app.CatalogueApplication.GenerateId()));
            app.CatalogueApplication.SerialisationVideos("listeVideo.JSON");


            Console.WriteLine(new DateOnly());
        }
    }
}
