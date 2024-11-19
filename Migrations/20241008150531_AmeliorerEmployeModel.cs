using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Administration.Migrations
{
    /// <inheritdoc />
    public partial class AmeliorerEmployeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TypeContrat",
                table: "Employes",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CNSS_Employe",
                table: "Employes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Tel_Employe",
                table: "Employes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employes_CIN_Employe",
                table: "Employes",
                column: "CIN_Employe",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employes_CNSS_Employe",
                table: "Employes",
                column: "CNSS_Employe");

            migrationBuilder.CreateIndex(
                name: "IX_Employes_Nom_Employe",
                table: "Employes",
                column: "Nom_Employe",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employes_CIN_Employe",
                table: "Employes");

            migrationBuilder.DropIndex(
                name: "IX_Employes_CNSS_Employe",
                table: "Employes");

            migrationBuilder.DropIndex(
                name: "IX_Employes_Nom_Employe",
                table: "Employes");

            migrationBuilder.DropColumn(
                name: "Tel_Employe",
                table: "Employes");

            migrationBuilder.AlterColumn<string>(
                name: "TypeContrat",
                table: "Employes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "CNSS_Employe",
                table: "Employes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
