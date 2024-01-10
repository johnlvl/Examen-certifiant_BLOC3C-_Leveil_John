using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen_certifiant_BLOC3C__Leveil_John.Migrations
{
    /// <inheritdoc />
    public partial class AjoutChampsDansTableAspNetUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PanierId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PanierId",
                table: "AspNetUsers",
                column: "PanierId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Paniers_PanierId",
                table: "AspNetUsers",
                column: "PanierId",
                principalTable: "Paniers",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Paniers_PanierId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PanierId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PanierId",
                table: "AspNetUsers");
        }
    }
}
