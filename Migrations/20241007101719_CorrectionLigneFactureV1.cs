using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Administration.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionLigneFactureV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LignesFacture_Services_ServiceID_Service",
                table: "LignesFacture");

            migrationBuilder.DropIndex(
                name: "IX_LignesFacture_ServiceID_Service",
                table: "LignesFacture");

            migrationBuilder.DropColumn(
                name: "ServiceID_Service",
                table: "LignesFacture");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceID_Service",
                table: "LignesFacture",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LignesFacture_ServiceID_Service",
                table: "LignesFacture",
                column: "ServiceID_Service");

            migrationBuilder.AddForeignKey(
                name: "FK_LignesFacture_Services_ServiceID_Service",
                table: "LignesFacture",
                column: "ServiceID_Service",
                principalTable: "Services",
                principalColumn: "ID_Service",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
