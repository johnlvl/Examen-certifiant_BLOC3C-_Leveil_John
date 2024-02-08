using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Examen_certifiant_BLOC3C__Leveil_John.Migrations
{
    /// <inheritdoc />
    public partial class ModifTypePropClientIdTableReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_ClientIdId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ClientIdId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ClientIdId",
                table: "Reservations");

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Reservations");

            migrationBuilder.AddColumn<string>(
                name: "ClientIdId",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ClientIdId",
                table: "Reservations",
                column: "ClientIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_ClientIdId",
                table: "Reservations",
                column: "ClientIdId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
