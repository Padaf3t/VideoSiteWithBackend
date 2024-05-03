using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ProjetCatalogue.Models
{
    /// <summary>
    /// Classe qui permet de gérer une liste d'utilisateur
    /// </summary>
    public class GestionUtilisateur : DbContext
    {
        DbSet<Utilisateur> _listeUtilisateur;

        public DbSet<Utilisateur> ListeUtilisateurs { get => _listeUtilisateur; set => _listeUtilisateur = value; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(
            @"Server=(localdb)\MSSQLLocalDB;Database=ProjetCatalogue;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Video>().ToTable("Videos");

            modelBuilder.Entity<Video>().HasData(
                    new Video { IdVideo = 1, Titre = "Funny Bunny", TypeVideo = EnumAnimal.Lapin, CoteEvaluation = -1.0, DateMiseEnLigne = new DateTime(2010, 10, 21), DureeVideo = 0.28, Auteur = "Polo Paulson", Acteur = "Mr Carrots", Extrait = "1.mp4", VideoComplet = "1.mp4", Image = "1.jpeg" },
                    new Video { IdVideo = 2, Titre = "Grumpy Cat on a talk show", TypeVideo = EnumAnimal.Chat, CoteEvaluation = -1.0, DateMiseEnLigne = new DateTime(2017, 5, 10), DureeVideo = 1.27, Auteur = "Tamara Tamarin", Acteur = "Grumpy Cat", Extrait = "2.mp4", VideoComplet = "2.mp4", Image = "2.jpeg" },
                    new Video { IdVideo = 3, Titre = "Grumpy Cat's first pitch", TypeVideo = EnumAnimal.Chat, CoteEvaluation = -1.0, DateMiseEnLigne = new DateTime(2019, 7, 6), DureeVideo = 1.0, Auteur = "Tamara Tamarin", Acteur = "Grumpy Cat", Extrait = "3.mp4", VideoComplet = "3.mp4", Image = "3.jpeg" },
                    new Video { IdVideo = 4, Titre = "Playful ferret", TypeVideo = EnumAnimal.Furet, CoteEvaluation = -1.0, DateMiseEnLigne = new DateTime(2012, 1, 30), DureeVideo = 1.28, Auteur = "Mick McMac", Acteur = "Jean-Guy le furet", Extrait = "4.mp4", VideoComplet = "4.mp4", Image = "4.jpeg" },
                    new Video { IdVideo = 5, Titre = "Fox likes attention", TypeVideo = EnumAnimal.Renard, CoteEvaluation = -1.0, DateMiseEnLigne = new DateTime(2019, 12, 16), DureeVideo = 0.52, Auteur = "Tommy Tomtoms", Acteur = "FoxyFox", Extrait = "5.mp4", VideoComplet = "5.mp4", Image = "5.jpeg" },
                    new Video { IdVideo = 6, Titre = "Keyboard Cat", TypeVideo = EnumAnimal.Chat, CoteEvaluation = -1.0, DateMiseEnLigne = new DateTime(2019, 12, 16), DureeVideo = 0.54, Auteur = "Michelle Michels", Acteur = "Keyboard Cat", Extrait = "6.mp4", VideoComplet = "6.mp4", Image = "6.jpeg" },
                    new Video { IdVideo = 7, Titre = "Big Insect", TypeVideo = EnumAnimal.Insecte, CoteEvaluation = -1.0, DateMiseEnLigne = new DateTime(2010, 4, 4), DureeVideo = 0.21, Auteur = "Georgio Georges", Acteur = "Mc Roach", Extrait = "7.mp4", VideoComplet = "7.mp4", Image = "7.jpeg" },
                    new Video { IdVideo = 8, Titre = "Rabbit eats lemon", TypeVideo = EnumAnimal.Lapin, CoteEvaluation = -1.0, DateMiseEnLigne = new DateTime(2013, 8, 27), DureeVideo = 1.38, Auteur = "Stella Steel", Acteur = "Miss Muffin", Extrait = "8.mp4", VideoComplet = "8.mp4", Image = "8.jpeg" },
                    new Video { IdVideo = 9, Titre = "Raccoon steals carpet", TypeVideo = EnumAnimal.Raton, CoteEvaluation = -1.0, DateMiseEnLigne = new DateTime(2008, 6, 29), DureeVideo = 1.0, Auteur = "Yan YinYang", Acteur = "Robber Raccoon", Extrait = "9.mp4", VideoComplet = "9.mp4", Image = "9.jpeg" },
                    new Video { IdVideo = 10, Titre = "Marmot gets a bath", TypeVideo = EnumAnimal.Marmotte, CoteEvaluation = -1.0, DateMiseEnLigne = new DateTime(2023, 1, 5), DureeVideo = 2.34, Auteur = "Albert Albertson", Acteur = "One Small marmot", Extrait = "10.mp4", VideoComplet = "10.mp4", Image = "10.jpeg" }

                    );
            modelBuilder.Entity<Favori>().ToTable("Favoris");

            modelBuilder.Entity<Favori>().HasData(


                new Favori { IdFavori = 7, IdVideo = 1, PseudoUtilisateur = "KeyboardCatBobby", DateAjout = new DateTime(2023, 09, 22, 10, 11, 51, 0) },
                new Favori { IdFavori = 1, IdVideo = 4, PseudoUtilisateur = "KeyboardCatBobby", DateAjout = new DateTime(2024, 01, 22, 10, 11, 51, 0) },
                new Favori { IdFavori = 2, IdVideo = 6, PseudoUtilisateur = "KeyboardCatBobby", DateAjout = new DateTime(2024, 01, 10, 10, 11, 51, 0) },
                new Favori { IdFavori = 3, IdVideo = 6, PseudoUtilisateur = "JeSuisJolie93", DateAjout = new DateTime(2023, 09, 22, 10, 11, 51, 0) },
                new Favori { IdFavori = 4, IdVideo = 7, PseudoUtilisateur = "JeSuisJolie93", DateAjout = new DateTime(2023, 09, 22, 11, 11, 51, 0) },
                new Favori { IdFavori = 5, IdVideo = 2, PseudoUtilisateur = "JeSuisJolie93", DateAjout = new DateTime(2023, 09, 22, 12, 11, 51, 0) },
                new Favori { IdFavori = 6, IdVideo = 10, PseudoUtilisateur = "JeSuisJolie93", DateAjout = new DateTime(2023, 09, 22, 09, 11, 51, 0) });

            modelBuilder.Entity<Utilisateur>().ToTable("Utilisateurs");

            modelBuilder.Entity<Utilisateur>().HasData(
                new Utilisateur { Pseudo = "KeyboardCatBobby", MotDePasse = "Soleil01!", RoleUser = EnumRole.UtilisateurSimple, Nom = "McBob", Prenom = "Bobby" },
                new Utilisateur { Pseudo = "JeSuisJolie93", MotDePasse = "Soleil01!", RoleUser = EnumRole.UtilisateurSimple, Nom = "Bobinson", Prenom = "Maritza" },
                new Utilisateur { Pseudo = "adminPrincipal", MotDePasse = "Soleil01!", RoleUser = EnumRole.Admin, Nom = "Rogers", Prenom = "Roger" }
                );
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Constructeur sans paramètre; crée une nouvelle liste vide pour la propriété de la liste d'utilisateurs
        /// </summary>
        //public GestionUtilisateur()
        //{
        //    ListeUtilisateurs = new DbSet<Utilisateur>();
        //    DeserialisationJSONUtilisateur(PathFinder.PathJsonUtilisateur);
        //}

        /// <summary>
        /// Permet l'ajout d'un utilisateur à la liste d'utilisateurs de l'application
        /// </summary>
        /// <param name="user">L'utilisateur à ajouter</param>
        /// <returns>bool : true si l'ajout a bien été effectué</returns>
        public bool AjouterUtilisateur(Utilisateur user, out string? messageErreur)
        {
            messageErreur = null;
            bool erreurNote = false;

            try
            {
                if (TrouverUtilisateur(user) != null)
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
                    messageErreur += exception.Message + "/n/n";
                }
                try
                {
                    Utilisateur.VerifierUnMotDePasse(motDePasse);
                }
                catch (ArgumentException exception)
                {
                    messageErreur += exception.Message + "/n/n";
                }
                try
                {
                    Utilisateur.VerifierUnNom(nom);
                }
                catch (ArgumentException exception)
                {
                    messageErreur += exception.Message + "/n/n";
                }
                try
                {
                    Utilisateur.VerifierUnPrenom(prenom);
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
        /// <returns>un booléen: true si l'utilisateur a bien été supprimé; false sinon</returns>
        public bool SupprimerUtilisateur(Utilisateur user)
        {
            if(!this.ListeUtilisateurs.Contains(user))
            {
                return false;
            }
            this.ListeUtilisateurs.Remove(user);
            return !this.ListeUtilisateurs.Contains(user);
        }

        /// <summary>
        /// Permet d'avoir accès à une query contenant un utilisateur, si trouvé parmi la liste d'utilisateurs,
        /// qui est le même que celui reçu en paramètre
        /// </summary>
        /// <param name="user">l'utilisateur à trouver</param>
        /// <returns>un IEnumerable contenant ou non cet utilisateur</returns>
        private Utilisateur? TrouverUtilisateur(Utilisateur? user)
        {
            return TrouverUtilisateur(user.Pseudo);
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
                Utilisateur? util = TrouverUtilisateur(utilisateur);
                if (util != null)
                {
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
            return this.ListeUtilisateurs.Where(utilisateur => utilisateur.Pseudo == pseudo).FirstOrDefault();
        }
    }
}
