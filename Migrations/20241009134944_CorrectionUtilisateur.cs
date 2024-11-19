using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Administration.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionUtilisateur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrixHT",
                table: "Services");

            migrationBuilder.AlterColumn<string>(
                name: "Email_Utilisateur",
                table: "Utilisateurs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Pseudo",
                table: "Utilisateurs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Total_LigneHT",
                table: "LignesFacture",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_Email_Utilisateur",
                table: "Utilisateurs",
                column: "Email_Utilisateur",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_Nom_Utilisateur",
                table: "Utilisateurs",
                column: "Nom_Utilisateur",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Utilisateurs_Pseudo",
                table: "Utilisateurs",
                column: "Pseudo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Utilisateurs_Email_Utilisateur",
                table: "Utilisateurs");

            migrationBuilder.DropIndex(
                name: "IX_Utilisateurs_Nom_Utilisateur",
                table: "Utilisateurs");

            migrationBuilder.DropIndex(
                name: "IX_Utilisateurs_Pseudo",
                table: "Utilisateurs");

            migrationBuilder.DropColumn(
                name: "Pseudo",
                table: "Utilisateurs");

            migrationBuilder.DropColumn(
                name: "Total_LigneHT",
                table: "LignesFacture");

            migrationBuilder.AlterColumn<string>(
                name: "Email_Utilisateur",
                table: "Utilisateurs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<decimal>(
                name: "PrixHT",
                table: "Services",
                type: "decimal(18,3)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
