using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen_certifiant_BLOC3C__Leveil_John.Migrations
{
    /// <inheritdoc />
    public partial class AjoutChampQuantiteTableOffre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantite",
                table: "Offres",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantite",
                table: "Offres");
        }
    }
}
