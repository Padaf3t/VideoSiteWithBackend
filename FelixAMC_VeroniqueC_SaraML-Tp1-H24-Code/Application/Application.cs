using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogue
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
            int longeurInitial = ListeUtilisateur.Count;
            ListeUtilisateur.Add(user);
            return ListeUtilisateur.Count > longeurInitial;
        }

        private List<Utilisateur> AjouterJSONUtilisateur(string fichier)
        {
            return this.ListeUtilisateur;
        }

        private void SauvegarderUtilisateurs(string fichier)
        {

        }

        static void Main(string[] args)
        {
            
            Console.WriteLine(new DateOnly());
        }
    }
}
