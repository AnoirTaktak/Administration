using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Administration.Migrations
{
    /// <inheritdoc />
    public partial class relationFourFacAchat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FournisseurID_Fournisseur",
                table: "FacturesAchat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FacturesAchat_FournisseurID_Fournisseur",
                table: "FacturesAchat",
                column: "FournisseurID_Fournisseur");

            migrationBuilder.AddForeignKey(
                name: "FK_FacturesAchat_Fournisseurs_FournisseurID_Fournisseur",
                table: "FacturesAchat",
                column: "FournisseurID_Fournisseur",
                principalTable: "Fournisseurs",
                principalColumn: "ID_Fournisseur",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacturesAchat_Fournisseurs_FournisseurID_Fournisseur",
                table: "FacturesAchat");

            migrationBuilder.DropIndex(
                name: "IX_FacturesAchat_FournisseurID_Fournisseur",
                table: "FacturesAchat");

            migrationBuilder.DropColumn(
                name: "FournisseurID_Fournisseur",
                table: "FacturesAchat");
        }
    }
}
