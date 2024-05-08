using Microsoft.EntityFrameworkCore;

namespace ProjetCatalogue.Models
{
    public class GestionContext : DbContext
    {
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
    }
}
