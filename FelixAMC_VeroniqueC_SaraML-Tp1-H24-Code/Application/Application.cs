using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCatalogue
{

    /// <summary>
    /// Classe qui constitue l'application, c'est la classe principale qui contient le Main donc l'entrée dans le programme
    /// </summary>
    public class Application
    {

        private List<Utilisateur> _listeUtilisateur;
        private Catalogue _catalogueApplication;

        /// <summary>
        /// Constructeur de la classe, sans paramètre. Crée une liste d'utilisateurs et un catalogue pour l'application
        /// </summary>
        public Application()
        {
            ListeUtilisateur = new List<Utilisateur>();
            CatalogueApplication = new Catalogue();
        }

        internal List<Utilisateur> ListeUtilisateur { get => _listeUtilisateur; set => _listeUtilisateur = value; }
        internal Catalogue CatalogueApplication { get => _catalogueApplication; set => _catalogueApplication = value; }

        /// <summary>
        /// Permet l'ajout d'un utilisateur à la liste d'utilisateurs de l'application
        /// </summary>
        /// <param name="user">L'utilisateur à ajouter</param>
        /// <returns>bool : true si l'ajout a bien été effectué</returns>
        internal bool AjouterUtilisateur(Utilisateur user)
        {
            ListeUtilisateur.Add(user);
            return ListeUtilisateur.Last() == user;
        }

        /// <summary>
        /// Méthode qui permet la désérialisation d'un fichier JSON pour en extraire des objets Java Utilisateurs
        /// et les placer dans une liste d'utilisateurs
        /// </summary>
        /// <param name="fichier">Le fichier JSON utilisé</param>
        /// <returns>List(Utilisateur): la liste d'utilisateurs ainsi créée</returns>
        private List<Utilisateur> AjouterJSONUtilisateur(string fichier)
        {
            //TODO : gerer erreurs
            List<Utilisateur> listeUtilisateurs = JsonConvert.DeserializeObject<List<Utilisateur>>(File.ReadAllText(@fichier), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            return listeUtilisateurs;
        }

        /// <summary>
        /// Permet de prendre une liste d'utilisateurs et de la sérialiser dans un fichier JSON
        /// </summary>
        /// <param name="fichier">Le fichier JSON à utiliser</param>
        private void SauvegarderUtilisateurs(string fichier)
        {
            string jsonListe = JsonConvert.SerializeObject(this.ListeUtilisateur, this.ListeUtilisateur.GetType(), Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            File.WriteAllText(@fichier, jsonListe);
        }

        /// <summary>
        /// Méthode d'entrée dans le programme
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            
            Console.WriteLine(new DateOnly());
        }
    }
}
