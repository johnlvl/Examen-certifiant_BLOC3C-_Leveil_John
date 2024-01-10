using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen_certifiant_BLOC3C__Leveil_John.Migrations
{
    /// <inheritdoc />
    public partial class AjoutChampsDansTablePanier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UtilisateurId",
                table: "Paniers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PanierID",
                table: "Offres",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offres_PanierID",
                table: "Offres",
                column: "PanierID");

            migrationBuilder.AddForeignKey(
                name: "FK_Offres_Paniers_PanierID",
                table: "Offres",
                column: "PanierID",
                principalTable: "Paniers",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offres_Paniers_PanierID",
                table: "Offres");

            migrationBuilder.DropIndex(
                name: "IX_Offres_PanierID",
                table: "Offres");

            migrationBuilder.DropColumn(
                name: "UtilisateurId",
                table: "Paniers");

            migrationBuilder.DropColumn(
                name: "PanierID",
                table: "Offres");

            migrationBuilder.AddColumn<int>(
                name: "PanierId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PanierId",
                table: "AspNetUsers",
                column: "PanierId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Paniers_PanierId",
                table: "AspNetUsers",
                column: "PanierId",
                principalTable: "Paniers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
