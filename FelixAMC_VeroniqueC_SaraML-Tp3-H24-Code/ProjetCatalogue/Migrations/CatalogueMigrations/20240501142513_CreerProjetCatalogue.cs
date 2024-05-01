using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetCatalogue.Migrations.CatalogueMigrations
{
    /// <inheritdoc />
    public partial class CreerProjetCatalogue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListeVideos",
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
                    table.PrimaryKey("PK_ListeVideos", x => x.IdVideo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListeVideos");
        }
    }
}
