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

        public List<Utilisateur> ListeUtilisateurs { get => _listeUtilisateur; set => _listeUtilisateur = value; }

        public GestionUtilisateur()
        {
            ListeUtilisateurs = new List<Utilisateur>();
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
        public bool AjouterUtilisateur(Utilisateur user)
        {
            foreach(Utilisateur utilisateur in ListeUtilisateurs)
            {
                if(utilisateur.Pseudo == user.Pseudo)
                {
                    throw new ArgumentException("L'utilisateur " + user.Pseudo + " existe déjà");
                }
            }
            ListeUtilisateurs.Add(user);
            return ListeUtilisateurs.Last() == user;
        }

        /// <summary>
        /// Permet de prendre une liste d'utilisateurs et de la sérialiser dans un fichier JSON
        /// </summary>
        /// <param name="fichierJSON">Le fichier JSON à utiliser</param>
        public void SerialisationUtilisateurs(string fichierJSON)
        {
            string jsonListe = JsonConvert.SerializeObject(this.ListeUtilisateurs, this.ListeUtilisateurs.GetType(), Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            File.WriteAllText(fichierJSON, jsonListe);
        }

        /// <summary>
        /// Méthode qui permet la désérialisation d'un fichier JSON pour en extraire des objets C# Utilisateurs
        /// et les placer dans une liste d'utilisateurs
        /// </summary>
        /// <param name="fichierJSON">Le fichier JSON utilisé</param>
        public void DeserialisationJSONUtilisateur(string fichierJSON)
        {
            List<Utilisateur>? liste = null;
            try
            {
                liste = JsonConvert.DeserializeObject<List<Utilisateur>>(File.ReadAllText(fichierJSON), new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Le dossier {0} n'a pas été trouvé", @fichierJSON);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Le fichier {0} n'a pas été trouvé", fichierJSON);
            }
            finally
            {
                if(liste != null)
                {
                    this.ListeUtilisateurs = liste;
                }
            }
            

        }
    }
}
