using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCatalogue
{
    public class Application
    {
        private List<Utilisateur> _listeUtilisateur;
        private Catalogue _catalogueApplication;

        public Application()
        {
            ListeUtilisateur = new List<Utilisateur>();
            CatalogueApplication = new Catalogue();
        }

        internal List<Utilisateur> ListeUtilisateur { get => _listeUtilisateur; set => _listeUtilisateur = value; }
        internal Catalogue CatalogueApplication { get => _catalogueApplication; set => _catalogueApplication = value; }

        internal bool AjouterUtilisateur(Utilisateur user)
        {
            ListeUtilisateur.Add(user);
            return ListeUtilisateur.Last() == user;
        }

        private List<Utilisateur> AjouterJSONUtilisateur(string fichier)
        {
            //TODO : gerer erreurs
            List<Utilisateur> listeUtilisateurs = JsonConvert.DeserializeObject<List<Utilisateur>>(File.ReadAllText(@fichier), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            return listeUtilisateurs;
        }

        private void SauvegarderUtilisateurs(string fichier)
        {
            string jsonListe = JsonConvert.SerializeObject(this.ListeUtilisateur, this.ListeUtilisateur.GetType(), Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            File.WriteAllText(@fichier, jsonListe);
        }

        static void Main(string[] args)
        {
            
            Console.WriteLine(new DateOnly());
        }
    }
}
