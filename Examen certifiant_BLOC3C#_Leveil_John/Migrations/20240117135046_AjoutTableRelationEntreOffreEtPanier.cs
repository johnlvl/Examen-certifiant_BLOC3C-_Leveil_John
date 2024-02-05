using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen_certifiant_BLOC3C__Leveil_John.Migrations
{
    /// <inheritdoc />
    public partial class AjoutTableRelationEntreOffreEtPanier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offres_Paniers_PanierID",
                table: "Offres");

            migrationBuilder.DropIndex(
                name: "IX_Offres_PanierID",
                table: "Offres");

            migrationBuilder.DropColumn(
                name: "PanierID",
                table: "Offres");

            migrationBuilder.CreateTable(
                name: "RelationOffrePanier",
                columns: table => new
                {
                    OffresID = table.Column<int>(type: "int", nullable: false),
                    PaniersID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelationOffrePanier", x => new { x.OffresID, x.PaniersID });
                    table.ForeignKey(
                        name: "FK_RelationOffrePanier_Offres_OffresID",
                        column: x => x.OffresID,
                        principalTable: "Offres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelationOffrePanier_Paniers_PaniersID",
                        column: x => x.PaniersID,
                        principalTable: "Paniers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RelationOffrePanier_PaniersID",
                table: "RelationOffrePanier",
                column: "PaniersID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelationOffrePanier");

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
    }
}
