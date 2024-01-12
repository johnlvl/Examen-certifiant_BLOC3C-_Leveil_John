using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen_certifiant_BLOC3C__Leveil_John.Migrations
{
    /// <inheritdoc />
    public partial class SuppressionChampsTableOffre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offres_Paniers_PanierId",
                table: "Offres");

            migrationBuilder.DropColumn(
                name: "Quantite",
                table: "Offres");

            migrationBuilder.RenameColumn(
                name: "PanierId",
                table: "Offres",
                newName: "PanierID");

            migrationBuilder.RenameIndex(
                name: "IX_Offres_PanierId",
                table: "Offres",
                newName: "IX_Offres_PanierID");

            migrationBuilder.AlterColumn<int>(
                name: "PanierID",
                table: "Offres",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.RenameColumn(
                name: "PanierID",
                table: "Offres",
                newName: "PanierId");

            migrationBuilder.RenameIndex(
                name: "IX_Offres_PanierID",
                table: "Offres",
                newName: "IX_Offres_PanierId");

            migrationBuilder.AlterColumn<int>(
                name: "PanierId",
                table: "Offres",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantite",
                table: "Offres",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Offres_Paniers_PanierId",
                table: "Offres",
                column: "PanierId",
                principalTable: "Paniers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
