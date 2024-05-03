using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ProjetCatalogue.Models
{

    /// <summary>
    /// Classe constituant un catalogue de vidéos (contient une liste de vidéos)
    /// </summary>
    public class Catalogue : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(
            @"Server=(localdb)\MSSQLLocalDB;Database=ProjetCatalogue;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Video>().ToTable("Video");

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

            base.OnModelCreating(modelBuilder);
        }

        private DbSet<Video> _listeVideos;

        public DbSet<Video> ListeVideos { get => _listeVideos; set => _listeVideos = value; }

        /// <summary>
        /// Permet l'ajout d'une vidéo à la liste de vidéos du catalogue, après avoir validé que celle-ci n'y est pas déjà
        /// Une erreur sera lancée et attrapée si la vidéo existe déjà
        /// </summary>
        /// <param name="video">La vidéo à ajouter</param>
        /// <returns>bool : true si l'ajout a bien été effectué; false si la vidéo existait déjà</returns>
        public bool AjouterVideo(Video video)
        {
            bool erreurNote = false;

            try
            {
                if (TrouverUneVideo(video.IdVideo) != null)
                {
                    throw new ArgumentException("La video " + video.IdVideo + " existe déjà");
                }

                this.ListeVideos.Add(video);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                erreurNote = true;
            }

            return !erreurNote;

        }

        /// <summary>
        /// Permet de trouver une vidéo selon son id
        /// </summary>
        /// <param name="idVideoAChercher">le id de la vidéo à chercher</param>
        /// <returns>la vidéo trouvée (peut être null si rien trouvé)</returns>
        public Video? TrouverUneVideo(int idVideoAChercher)
        {
            return this.ListeVideos.Where(video => video.IdVideo == idVideoAChercher).FirstOrDefault();
        }

        /// <summary>
        /// Permet d'avoir accès à une query qui contient une liste de vidéos, à partir d'une liste de favoris
        /// </summary>
        /// <param name="listeFavori">Une liste de favoris</param>
        /// <returns>Une liste de vidéos</returns>
        public List<Video> ObtenirListeVideoFavorites(List<Favori> listeFavori)
        {
            IEnumerable < Video > query = 
                from videoTemp in this.ListeVideos
                join favoriTemp in listeFavori
                on videoTemp.IdVideo equals favoriTemp.IdVideo
                select videoTemp;

            return query.ToList();
        }
        
        /// <summary>
        /// Méthode ToString de la classe.
        /// </summary>
        /// <returns>string : chaine contenant la liste des vidéos du catalogue</returns>
        public override string ToString()
        {
            string catalogueVideo = "Catalogue de vidéos animales : \n\n";
            foreach (Video video in this.ListeVideos) 
            { 
                catalogueVideo += video.ToString() + "\n";
            }
            return catalogueVideo;
        }

    }
}
