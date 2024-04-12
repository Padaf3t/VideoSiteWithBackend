namespace ProjetCatalogue
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            
            //Routes Administrateur
            app.MapControllerRoute(
                name: "adminUsers",
                pattern: "/Admin/Utilisateurs",
                defaults: new { controller = "Administrateur", action = "LesUtilisateurs" }
                );

            app.MapControllerRoute(
                name: "adminMedias",
                pattern: "/Admin/Videos",
                defaults: new { controller = "Administrateur", action = "LesMedias" }
                );

            app.MapControllerRoute(
                name: "adminSuppressionUser",
                pattern: "/Admin/Utilisateurs",
                defaults: new { controller = "Administrateur", action = "SupprimerUtilisateur" }
                );

            //Routes Utilisateurs
            app.MapControllerRoute(
                name: "userMedia",
                pattern: "/Utilisateur/Medias",
                defaults: new {controller="Utilisateur", action="TousLesMedias" }
                );

            app.MapControllerRoute(
                name: "userFavori",
                pattern: "/Utilisateur/Favoris",
                defaults: new { controller = "Utilisateur", action = "MesFavoris" }
                );

            app.MapControllerRoute(
                name: "userVideo",
                pattern: "/Utilisateur/Video/{id}/{favoriEstModifie}",
                defaults: new { controller = "Utilisateur", action = "VideoSpecifique"}
                );

            app.MapControllerRoute(
                name: "userVideo",
                pattern: "/Utilisateur/Video/{id}",
                defaults: new { controller = "Utilisateur", action = "VideoSpecifique"}
                );

            //Route Non Connecté

            app.MapControllerRoute(
                name: "formulaire",
                pattern: "/Accueil",
                defaults: new { controller = "NonConnecte", action = "ResultatFormulaire" }
                );

            app.MapControllerRoute(
                name: "accueil",
                pattern: "/Accueil",
                defaults: new { controller = "NonConnecte", action = "Accueil" }
                );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=NonConnecte}/{action=Accueil}");

            app.Run();
        }
    }
}