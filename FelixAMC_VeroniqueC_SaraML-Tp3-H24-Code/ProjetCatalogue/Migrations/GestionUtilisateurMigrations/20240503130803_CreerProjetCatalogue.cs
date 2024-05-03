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
                table: "Utilisateurs",
                columns: new[] { "Pseudo", "MotDePasse", "Nom", "Prenom", "RoleUser" },
                values: new object[,]
                {
                    { "adminPrincipal", "Soleil01!", "Rogers", "Roger", 2 },
                    { "JeSuisJolie93", "Soleil01!", "Bobinson", "Maritza", 0 },
                    { "KeyboardCatBobby", "Soleil01!", "McBob", "Bobby", 0 }
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
