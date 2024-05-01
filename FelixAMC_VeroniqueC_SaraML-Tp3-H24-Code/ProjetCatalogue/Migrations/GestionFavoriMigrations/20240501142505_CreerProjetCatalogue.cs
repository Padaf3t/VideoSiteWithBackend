using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetCatalogue.Migrations.GestionFavoriMigrations
{
    /// <inheritdoc />
    public partial class CreerProjetCatalogue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListeFavoris",
                columns: table => new
                {
                    IdFavori = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVideo = table.Column<int>(type: "int", nullable: false),
                    PseudoUtilisateur = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListeFavoris", x => x.IdFavori);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListeFavoris");
        }
    }
}
