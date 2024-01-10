using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen_certifiant_BLOC3C__Leveil_John.Migrations
{
    /// <inheritdoc />
    public partial class AjoutChampDescriptionDansOffre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Offres",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
