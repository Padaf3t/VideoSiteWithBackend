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

            app.MapControllerRoute(
                name: "userMedia",
                pattern: "/Utilisateur/Medias/{pseudo?}",
                defaults: new {controller="Utilisateur", action="TousLesMedias" }
                );

            app.MapControllerRoute(
                name: "userFavori",
                pattern: "/Utilisateur/Favoris/{pseudo?}",
                defaults: new { controller = "Utilisateur", action = "MesFavoris" }
                );
            app.MapControllerRoute(
                name: "userVideo",
                pattern: "/Utilisateur/Video/{id}/{estAjouteFavori?}/{pseudo?}",
                defaults: new { controller = "Utilisateur", action = "VideoSpecifique" }
                );

            app.MapControllerRoute(
                name: "userVideo",
                pattern: "/Utilisateur/Video/{id}",
                defaults: new { controller = "Utilisateur", action = "VideoSpecifique" }
                );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=NonConnecte}/{action=Accueil}/{id?}");

            app.Run();
        }
    }
}