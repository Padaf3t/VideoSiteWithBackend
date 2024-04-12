using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetCatalogue.Models
{
    /// <summary>
    /// Classe qui permet de gérer une liste d'utilisateur
    /// </summary>
    public class GestionUtilisateur
    {
        List<Utilisateur> _listeUtilisateur;

        public List<Utilisateur> ListeUtilisateurs { get => _listeUtilisateur; set => _listeUtilisateur = value; }

        public GestionUtilisateur()
        {
            ListeUtilisateurs = new List<Utilisateur>();
        }

        /// <summary>
        /// Permet l'ajout d'un utilisateur à la liste d'utilisateurs de l'application
        /// </summary>
        /// <param name="user">L'utilisateur à ajouter</param>
        /// <returns>bool : true si l'ajout a bien été effectué</returns>
        public bool AjouterUtilisateur(Utilisateur user)
        {
            IEnumerable<Utilisateur> query = QueryPourTrouverUser(user);
            

            bool erreurNote = false;

            try
            {
                if(query.Count() > 0)
                {
                    throw new ArgumentException("L'utilisateur " + user.Pseudo + " existe déjà");
                }
            
                ListeUtilisateurs.Add(user);
            }
            catch(ArgumentException e)
            {
                Console.WriteLine(e.Message);
                erreurNote = true;
            }
            
            return !erreurNote;
        }

        public bool CreationUtilisateur(string pseudo, string motDePasse, out Utilisateur? utilisateur, out string? messageErreur)
        {
            return CreationUtilisateur(pseudo, motDePasse, "", "", false, out utilisateur, out messageErreur);
        }


        public bool CreationUtilisateur(string pseudo, string motDePasse, string prenom, string nom, bool estAdministrateur, out Utilisateur? utilisateur, out string? messageErreur)
        {
            utilisateur = null;
            bool estCree = false;
            messageErreur = null;
            

            try
            {
                EnumRole enumRole;

                if (estAdministrateur)
                {
                    enumRole = EnumRole.Admin;
                }
                else
                {
                    enumRole = EnumRole.UtilisateurSimple;
                }
                utilisateur = new Utilisateur(pseudo, motDePasse, nom, prenom, enumRole);
                estCree = true;
            }catch (ArgumentException e)
            {
                messageErreur = "";
                try
                {
                    Utilisateur.VerifierUnPseudo(pseudo);
                }
                catch (ArgumentException exception)
                {
                    messageErreur += exception.Message;
                }
                try
                {
                    Utilisateur.VerifierUnMotDePasse(motDePasse);
                }
                catch (ArgumentException exception)
                {
                    messageErreur += exception.Message;
                }
            }

            return estCree;
        }

        public bool SupprimerUtilisateur(Utilisateur user)
        {
            return ListeUtilisateurs.Remove(user);
        }

        private IEnumerable<Utilisateur> QueryPourTrouverUser(Utilisateur user)
        {
            return from utilisateur in this.ListeUtilisateurs
                   where utilisateur.Equals(user)
                   select utilisateur;
        }

        private IEnumerable<Utilisateur> QueryPourTrouverUser(String pseudo)
        {
            return from utilisateur in this.ListeUtilisateurs
                   where utilisateur.Pseudo.Equals(pseudo)
                   select utilisateur;
        }

        public bool ValiderUtilisateur(String pseudo, String motDePasse, out Utilisateur? utilisateur, out string? messageErreur)
        {

            bool estValide = false;
            messageErreur = null;

            if(CreationUtilisateur(pseudo, motDePasse, out utilisateur, out messageErreur))
            {
                List<Utilisateur> listeUser = QueryPourTrouverUser(utilisateur).ToList();
                if (listeUser.Count() > 0)
                {
                    utilisateur = listeUser[0];
                    estValide = true;
                }
            }

            if (!estValide)
            {
                messageErreur = "Le pseudo ou mot de passe est invalide";
            }

            return estValide;
        }

        public Utilisateur? TrouverUtilisateur(string pseudo)
        {
            List<Utilisateur> listeUser = QueryPourTrouverUser(pseudo).ToList();

            Utilisateur? utilisateur = null;

            if (listeUser.Count() > 0)
            {
                utilisateur = listeUser[0];
            }

            return utilisateur;
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
        /// soulève une exception si le dossier n'est pas trouvé ou que le fichier n'est pas trouver
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
