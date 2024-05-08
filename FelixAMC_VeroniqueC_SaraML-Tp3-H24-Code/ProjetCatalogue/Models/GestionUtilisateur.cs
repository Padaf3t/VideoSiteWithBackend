using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ProjetCatalogue.Models
{
    /// <summary>
    /// Permet de gérer une liste d'utilisateur
    /// </summary>
    public class GestionUtilisateur : GestionContext
    {

        public DbSet<Utilisateur> ListeUtilisateurs { get; set; }

        /// <summary>
        /// Permet l'ajout d'un utilisateur à la liste d'utilisateurs de l'application, peut aussi produire un message d'erreur si pas possible de faire l'ajout
        /// </summary>
        /// <param name="user">L'utilisateur à ajouter</param>
        /// <param name="messageErreur">Le message d'erreur signalant le problème avec la tentative d'ajout d'un utilisateur, s'il y a lieu</param>
        /// <returns>bool : true si l'ajout a bien été effectué, false sinon (message d'erreur a été produit)</returns>
        public bool AjouterUtilisateur(Utilisateur user, out string? messageErreur)
        {
            messageErreur = null;
            bool erreurNote = false;

            try
            {
                //Utilisateur qui existe déjà
                if (TrouverUtilisateur(user) != null)
                {
                    throw new ArgumentException("L'utilisateur " + user.Pseudo + " existe déjà");
                }
            
                ListeUtilisateurs.Add(user);
                SaveChanges();
            }
            catch(ArgumentException e)
            {
                //Message d'erreur
                messageErreur=e.Message;
                erreurNote = true;
            }
            
            return !erreurNote;
        }

        /// <summary>
        /// Permet de créer un nouvel utilisateur selon un utilisateur reçu en paramètre, et peut produire un message d'erreur si pas possible de le créer
        /// </summary>
        /// <param name="utilisateurVoulu">L'utilisateur temp à partir des données duquel on va créer un utilisateur réel</param>
        /// <param name="utilisateur">L'Utilisateur réel qu'on va créer</param>
        /// <param name="messageErreur">Un message d'erreur s'il y a lieu, si la création n'a pas fonctionné</param>
        /// <returns>bool: true si l'utilisateur a bien pu être créé</returns>
        public bool CreationUtilisateur(Utilisateur utilisateurVoulu, out Utilisateur? utilisateur, out string? messageErreur)
        {
            utilisateur = null;
            bool estCree = false;
            messageErreur = null;

            try
            {
                utilisateur = new Utilisateur(utilisateurVoulu.Pseudo, utilisateurVoulu.MotDePasse, utilisateurVoulu.Nom, utilisateurVoulu.Prenom, EnumRole.UtilisateurSimple);
                estCree = true;
            }
            catch (ArgumentException)

            {
                messageErreur = "";
                try
                {
                    Utilisateur.VerifierUnPseudo(utilisateurVoulu.Pseudo);
                }
                catch (ArgumentException exception)
                {
                    messageErreur += exception.Message + "/n/n";
                }
                try
                {
                    Utilisateur.VerifierUnMotDePasse(utilisateurVoulu.MotDePasse);
                }
                catch (ArgumentException exception)
                {
                    messageErreur += exception.Message + "/n/n";
                }
                try
                {
                    Utilisateur.VerifierUnNom(utilisateurVoulu.Nom);
                }
                catch (ArgumentException exception)
                {
                    messageErreur += exception.Message + "/n/n";
                }
                try
                {
                    Utilisateur.VerifierUnPrenom(utilisateurVoulu.Prenom);
                }
                catch (ArgumentException exception)
                {
                    messageErreur += exception.Message + "/n/n";
                }
            }

            return estCree;
        }

        /// <summary>
        /// Supprime un utilisateur
        /// </summary>
        /// <param name="user">L'Utilisateur à supprimer</param>
        /// <returns>bool: true si l'utilisateur a bien été supprimé; false sinon</returns>
        public bool SupprimerUtilisateur(Utilisateur user)
        {
            if(!this.ListeUtilisateurs.Contains(user))
            {
                return false;
            }
            this.ListeUtilisateurs.Remove(user);
            SaveChanges();
            return !this.ListeUtilisateurs.Contains(user);
        }

        /// <summary>
        /// Cherche un utilisateur à partir d'un utilisateur (en apelant même méthode qui prend un pseudo)
        /// </summary>
        /// <param name="user">L'utilisateur à trouver</param>
        /// <returns>L'utilisateur trouvé, si trouvé, sinon null</returns>
        private Utilisateur? TrouverUtilisateur(Utilisateur? user)
        {
            return TrouverUtilisateur(user.Pseudo);
        }

        /// <summary>
        /// Cherche un utilisateur selon son pseudo
        /// </summary>
        /// <param name="pseudo">le pseudo de l'utilisateur à chercher</param>
        /// <returns>L'utilisateur trouvé, si trouvé, sinon null</returns>
        public Utilisateur? TrouverUtilisateur(string pseudo)
        {
            Utilisateur? user = this.ListeUtilisateurs.Where(utilisateur => utilisateur.Pseudo == pseudo).FirstOrDefault();
            return user;
        }

        /// <summary>
        /// Fait la validation côté serveur d'un utilisateur (utilisateur), en vérifiant s'il existe bien en appelant la méthode qui
        /// trouve un utilisateur
        /// </summary>
        /// <param name="utilisateur">L'utilisateur à valider</param>
        /// <param name="utilisateurEnregistre">L'utilisateur trouvé si trouvé, null si pas trouvé, est retourné via out</param>
        /// <param name="messageErreur">Un message d'erreur à retourner si invalide</param>
        /// <returns>bool: true si utilisateur est valide</returns>
        public bool ValiderUtilisateur(Utilisateur utilisateur, out Utilisateur? utilisateurEnregistre, out string messageErreur)
        {

            bool estValide = false;
            messageErreur = null;

            utilisateurEnregistre = TrouverUtilisateur(utilisateur);
            if (utilisateurEnregistre != null)
            {
                estValide = true;
            }
            

            if (!estValide)
            {
                messageErreur = "Le pseudo ou mot de passe est invalide";
            }
            return estValide;
        }

        public void ModifierRoleUtilisateur(Utilisateur utilisateur, EnumRole roleVoulu)
        {
            Utilisateur? utilisateurBD = TrouverUtilisateur(utilisateur);
            if(utilisateurBD != null)
            {
                utilisateurBD.RoleUser = roleVoulu;
                SaveChanges();
            }
            
        }
    }
}
