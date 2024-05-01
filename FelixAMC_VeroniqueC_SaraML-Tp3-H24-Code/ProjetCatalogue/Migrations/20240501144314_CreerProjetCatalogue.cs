using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjetCatalogue.Migrations
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Utilisateurs");
        }
    }
}
