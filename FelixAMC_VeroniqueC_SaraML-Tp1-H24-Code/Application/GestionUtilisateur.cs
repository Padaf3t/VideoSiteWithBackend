using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCatalogue
{
    public class GestionUtilisateur
    {
        List<Utilisateur> _listeUtilisateur;

        public List<Utilisateur> ListeUtilisateur { get => _listeUtilisateur; set => _listeUtilisateur = value; }

        public GestionUtilisateur()
        {
            ListeUtilisateur = new List<Utilisateur>();
        }



        public GestionUtilisateur(List<Utilisateur> listeUtilisateur)
        {
            _listeUtilisateur = listeUtilisateur;
        }

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
        /// Méthode qui permet la désérialisation d'un fichier JSON pour en extraire des objets C# Utilisateurs
        /// et les placer dans une liste d'utilisateurs
        /// </summary>
        /// <param name="fichier">Le fichier JSON utilisé</param>
        /// <returns>List(Utilisateur): la liste d'utilisateurs ainsi créée</returns>
        private List<Utilisateur> AjouterJSONUtilisateur(string fichier)
        {
            List<Utilisateur>? listeUtilisateurs = null;
            try
            {
                listeUtilisateurs = JsonConvert.DeserializeObject<List<Utilisateur>>(File.ReadAllText(@fichier), new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Le fichier {0} n'a pas été trouvé", fichier);
            }

            return listeUtilisateurs;

        }
    }
}
