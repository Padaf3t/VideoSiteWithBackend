using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjetCatalogue.Migrations.GestionFavoriMigrations
{
    /// <inheritdoc />
    public partial class CreerProjetCatalogue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utilisateur",
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
                    table.PrimaryKey("PK_Utilisateur", x => x.Pseudo);
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

            migrationBuilder.CreateTable(
                name: "Favoris",
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
                    table.PrimaryKey("PK_Favoris", x => x.IdFavori);
                    table.ForeignKey(
                        name: "FK_Favoris_Utilisateur_PseudoUtilisateur",
                        column: x => x.PseudoUtilisateur,
                        principalTable: "Utilisateur",
                        principalColumn: "Pseudo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favoris_Video_IdVideo",
                        column: x => x.IdVideo,
                        principalTable: "Video",
                        principalColumn: "IdVideo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Favoris",
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
                name: "IX_Favoris_IdVideo",
                table: "Favoris",
                column: "IdVideo");

            migrationBuilder.CreateIndex(
                name: "IX_Favoris_PseudoUtilisateur",
                table: "Favoris",
                column: "PseudoUtilisateur");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favoris");

            migrationBuilder.DropTable(
                name: "Utilisateur");

            migrationBuilder.DropTable(
                name: "Video");
        }
    }
}
