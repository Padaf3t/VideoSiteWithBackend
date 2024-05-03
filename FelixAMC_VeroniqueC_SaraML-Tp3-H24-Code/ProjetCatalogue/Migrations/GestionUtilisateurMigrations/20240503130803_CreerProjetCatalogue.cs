using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjetCatalogue.Migrations.GestionUtilisateurMigrations
{
    /// <inheritdoc />
    public partial class CreerProjetCatalogue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utilisateurs",
                columns: table => new
                {
                    Pseudo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MotDePasse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilisateurs", x => x.Pseudo);
                });

            migrationBuilder.InsertData(
                table: "Utilisateurs",
                columns: new[] { "Pseudo", "MotDePasse", "Nom", "Prenom", "RoleUser" },
                values: new object[,]
                {
                    { "adminPrincipal", "Soleil01!", "Rogers", "Roger", 2 },
                    { "JeSuisJolie93", "Soleil01!", "Bobinson", "Maritza", 0 },
                    { "KeyboardCatBobby", "Soleil01!", "McBob", "Bobby", 0 }
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    IdVideo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeVideo = table.Column<int>(type: "int", nullable: false),
                    CoteEvaluation = table.Column<double>(type: "float", nullable: false),
                    DateMiseEnLigne = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DureeVideo = table.Column<double>(type: "float", nullable: false),
                    Auteur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Acteur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extrait = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoComplet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.IdVideo);
                });

            migrationBuilder.InsertData(
                table: "Video",
                columns: new[] { "IdVideo", "Acteur", "Auteur", "CoteEvaluation", "DateMiseEnLigne", "DureeVideo", "Extrait", "Image", "Titre", "TypeVideo", "VideoComplet" },
                values: new object[,]
                {
                    { 1, "Mr Carrots", "Polo Paulson", -1.0, new DateTime(2010, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.28000000000000003, "1.mp4", "1.jpeg", "Funny Bunny", 7, "1.mp4" },
                    { 2, "Grumpy Cat", "Tamara Tamarin", -1.0, new DateTime(2017, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1.27, "2.mp4", "2.jpeg", "Grumpy Cat on a talk show", 1, "2.mp4" },
                    { 3, "Grumpy Cat", "Tamara Tamarin", -1.0, new DateTime(2019, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1.0, "3.mp4", "3.jpeg", "Grumpy Cat's first pitch", 1, "3.mp4" },
                    { 4, "Jean-Guy le furet", "Mick McMac", -1.0, new DateTime(2012, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1.28, "4.mp4", "4.jpeg", "Playful ferret", 15, "4.mp4" },
                    { 5, "FoxyFox", "Tommy Tomtoms", -1.0, new DateTime(2019, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.52000000000000002, "5.mp4", "5.jpeg", "Fox likes attention", 12, "5.mp4" },
                    { 6, "Keyboard Cat", "Michelle Michels", -1.0, new DateTime(2019, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.54000000000000004, "6.mp4", "6.jpeg", "Keyboard Cat", 1, "6.mp4" },
                    { 7, "Mc Roach", "Georgio Georges", -1.0, new DateTime(2010, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.20999999999999999, "7.mp4", "7.jpeg", "Big Insect", 9, "7.mp4" },
                    { 8, "Miss Muffin", "Stella Steel", -1.0, new DateTime(2013, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 1.3799999999999999, "8.mp4", "8.jpeg", "Rabbit eats lemon", 7, "8.mp4" },
                    { 9, "Robber Raccoon", "Yan YinYang", -1.0, new DateTime(2008, 6, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1.0, "9.mp4", "9.jpeg", "Raccoon steals carpet", 5, "9.mp4" },
                    { 10, "One Small marmot", "Albert Albertson", -1.0, new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2.3399999999999999, "10.mp4", "10.jpeg", "Marmot gets a bath", 6, "10.mp4" }
                });

            migrationBuilder.CreateTable(
                name: "Favori",
                columns: table => new
                {
                    IdFavori = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVideo = table.Column<int>(type: "int", nullable: false),
                    PseudoUtilisateur = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateAjout = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favori", x => x.IdFavori);
                    table.ForeignKey(
                        name: "FK_Favori_Utilisateurs_PseudoUtilisateur",
                        column: x => x.PseudoUtilisateur,
                        principalTable: "Utilisateurs",
                        principalColumn: "Pseudo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favori_Video_IdVideo",
                        column: x => x.IdVideo,
                        principalTable: "Video",
                        principalColumn: "IdVideo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
            table: "Favori",
            columns: new[] { "IdFavori", "DateAjout", "IdVideo", "PseudoUtilisateur" },
            values: new object[,]
            {
                    { 1, new DateTime(2024, 1, 22, 10, 11, 51, 0, DateTimeKind.Unspecified), 4, "KeyboardCatBobby" },
                    { 2, new DateTime(2024, 1, 10, 10, 11, 51, 0, DateTimeKind.Unspecified), 6, "KeyboardCatBobby" },
                    { 3, new DateTime(2023, 9, 22, 10, 11, 51, 0, DateTimeKind.Unspecified), 6, "JeSuisJolie93" },
                    { 4, new DateTime(2023, 9, 22, 11, 11, 51, 0, DateTimeKind.Unspecified), 7, "JeSuisJolie93" },
                    { 5, new DateTime(2023, 9, 22, 12, 11, 51, 0, DateTimeKind.Unspecified), 2, "JeSuisJolie93" },
                    { 6, new DateTime(2023, 9, 22, 9, 11, 51, 0, DateTimeKind.Unspecified), 10, "JeSuisJolie93" },
                    { 7, new DateTime(2023, 9, 22, 10, 11, 51, 0, DateTimeKind.Unspecified), 1, "KeyboardCatBobby" }
            });


            migrationBuilder.CreateIndex(
                name: "IX_Favori_IdVideo",
                table: "Favori",
                column: "IdVideo");

            migrationBuilder.CreateIndex(
                name: "IX_Favori_PseudoUtilisateur",
                table: "Favori",
                column: "PseudoUtilisateur");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favori");

            migrationBuilder.DropTable(
                name: "Utilisateurs");

            migrationBuilder.DropTable(
                name: "Video");
        }
    }
}
