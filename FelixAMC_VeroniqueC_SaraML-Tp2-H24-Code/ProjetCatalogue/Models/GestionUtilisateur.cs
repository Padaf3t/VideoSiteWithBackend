using Newtonsoft.Json;
/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/

namespace ProjetCatalogue.Models
{
    /// <summary>
    /// Classe qui permet de gérer une liste d'utilisateur
    /// </summary>
    public class GestionUtilisateur
    {
        List<Utilisateur> _listeUtilisateur;

        public List<Utilisateur> ListeUtilisateurs { get => _listeUtilisateur; set => _listeUtilisateur = value; }

        /// <summary>
        /// Constructeur sans paramètre; crée une nouvelle liste vide pour la propriété de la liste d'utilisateurs
        /// </summary>
        public GestionUtilisateur()
        {
            ListeUtilisateurs = new List<Utilisateur>();
        }

        /// <summary>
        /// Permet l'ajout d'un utilisateur à la liste d'utilisateurs de l'application
        /// </summary>
        /// <param name="user">L'utilisateur à ajouter</param>
        /// <returns>bool : true si l'ajout a bien été effectué</returns>
        public bool AjouterUtilisateur(Utilisateur user, out string? messageErreur)
        {
            messageErreur = null;
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
                messageErreur=e.Message;
                erreurNote = true;
            }
            
            return !erreurNote;
        }

        /// <summary>
        /// Permet de créer un nouvel utilisateur à l'aide de du pseudo et du mot de passe
        /// </summary>
        /// <param name="pseudo">Le pseudo à donner à  l'utilisateur</param>
        /// <param name="motDePasse">Le mot de passe à donner à l'utilisateur</param>
        /// <param name="utilisateur">L'utilisateur créé s'il y a lieu</param>
        /// <param name="messageErreur">Un message d'erreur s'il y a lieu, si la création n'a pas fonctionné</param>
        /// <returns></returns>
        public bool CreationUtilisateur(string pseudo, string motDePasse, out Utilisateur? utilisateur, out string? messageErreur)
        {
            return CreationUtilisateur(pseudo, motDePasse, "", "", false, out utilisateur, out messageErreur);
        }

        /// <summary>
        /// Permet de créer un nouvel utilisateur à l'aide de du pseudo, du mot de passe, du nom, du prénom
        /// et d'un bool qui dit si est administrateur ou non
        /// </summary>
        /// <param name="pseudo">Le pseudo à donner à  l'utilisateur</param>
        /// <param name="motDePasse">Le mot de passe à donner à l'utilisateur</param>
        /// <param name="prenom">Le prénom à donner à l'utilisateur</param>
        /// <param name="nom">Le nom à donner à l'utilisateur</param>
        /// <param name="estAdministrateur">booléen true si l'utilisateur est un administrateur, false sinon</param>
        /// <param name="utilisateur">L'utilisateur créé s'il y a lieu</param>
        /// <param name="messageErreur">Un message d'erreur s'il y a lieu, si la création n'a pas fonctionné</param>
        /// <returns></returns>
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
            }catch (ArgumentException)
            
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
                try
                {
                    Utilisateur.VerifierUnNom(nom);
                }
                catch (ArgumentException exception)
                {
                    messageErreur += exception.Message;
                }
                try
                {
                    Utilisateur.VerifierUnPrenom(prenom);
                }
                catch (ArgumentException exception)
                {
                    messageErreur += exception.Message;
                }
            }

            return estCree;
        }

        /// <summary>
        /// Supprime un utilisateur
        /// </summary>
        /// <param name="user">L'Utilisateur à supprimer</param>
        /// <returns>un booléen: true si l'utilisateur a bien été supprimé; false sinon</returns>
        public bool SupprimerUtilisateur(Utilisateur user)
        {
            return ListeUtilisateurs.Remove(user);
        }

        /// <summary>
        /// Permet d'avoir accès à une query contenant un utilisateur, si trouvé parmi la liste d'utilisateurs,
        /// qui est le même que celui reçu en paramètre
        /// </summary>
        /// <param name="user">l'utilisateur à trouver</param>
        /// <returns>un IEnumerable contenant ou non cet utilisateur</returns>
        private IEnumerable<Utilisateur> QueryPourTrouverUser(Utilisateur user)
        {
            return from utilisateur in this.ListeUtilisateurs
                   where utilisateur.Equals(user)
                   select utilisateur;
        }

        /// <summary>
        /// Permet l'accès à une query contenant un utilisateur, si trouvé parmi la liste d'utilisateurs, selon un pseudo
        /// </summary>
        /// <param name="pseudo">le pseudo de l'utilisateur à chercher</param>
        /// <returns>un IEnumerable contenant ou non l'utilisateur</returns>
        private IEnumerable<Utilisateur> QueryPourTrouverUser(String pseudo)
        {
            return from utilisateur in this.ListeUtilisateurs
                   where utilisateur.Pseudo.Equals(pseudo)
                   select utilisateur;
        }

        /// <summary>
        /// Fait la validation côté serveur d'un utilisateur, donc valide que le pseudo et mot de passe sont valides (que l'utilisateur
        /// existe bel et bien selon ces propriétés)
        /// </summary>
        /// <param name="pseudo">le pseudo de l'utilisateur</param>
        /// <param name="motDePasse">Le mot de passe de l'utilisateur</param>
        /// <param name="utilisateur">L'utilisateur à retourner si valide</param>
        /// <param name="messageErreur">Un message d'erreur à retourner si invalide</param>
        /// <returns>un bool qui est true si l'utilisateur est valide</returns>
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

        /// <summary>
        /// Cherche un utilisateur selon son pseudo
        /// </summary>
        /// <param name="pseudo">le pseudo de l'utilisateur à chercher</param>
        /// <returns>l'utilisateur</returns>
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
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Le dossier {0} n'a pas été trouvé", @fichierJSON);
            }
            catch (FileNotFoundException)
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
