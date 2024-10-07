using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Administration.Migrations
{
    /// <inheritdoc />
    public partial class Correction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LignesFacture_FacturesVente_FactureVenteID_FactureVente",
                table: "LignesFacture");

            migrationBuilder.AlterColumn<int>(
                name: "ID_FactureVente",
                table: "LignesFacture",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FactureVenteID_FactureVente",
                table: "LignesFacture",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_LignesFacture_FacturesVente_FactureVenteID_FactureVente",
                table: "LignesFacture",
                column: "FactureVenteID_FactureVente",
                principalTable: "FacturesVente",
                principalColumn: "ID_FactureVente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LignesFacture_FacturesVente_FactureVenteID_FactureVente",
                table: "LignesFacture");

            migrationBuilder.AlterColumn<int>(
                name: "ID_FactureVente",
                table: "LignesFacture",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FactureVenteID_FactureVente",
                table: "LignesFacture",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LignesFacture_FacturesVente_FactureVenteID_FactureVente",
                table: "LignesFacture",
                column: "FactureVenteID_FactureVente",
                principalTable: "FacturesVente",
                principalColumn: "ID_FactureVente",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
