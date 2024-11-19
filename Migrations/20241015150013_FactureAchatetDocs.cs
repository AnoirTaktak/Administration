using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Administration.Migrations
{
    /// <inheritdoc />
    public partial class FactureAchatetDocs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "CachetSignature",
                table: "Societes",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    ID_Document = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Employe = table.Column<int>(type: "int", nullable: false),
                    ID_TypeDocument = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Contenu = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.ID_Document);
                });

            migrationBuilder.CreateTable(
                name: "FacturesAchat",
                columns: table => new
                {
                    ID_FactureAchat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAchat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Montant = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EtatPaiement = table.Column<bool>(type: "bit", nullable: false),
                    ID_Fournisseur = table.Column<int>(type: "int", nullable: false),
                    ImageFacture = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturesAchat", x => x.ID_FactureAchat);
                });

            migrationBuilder.CreateTable(
                name: "TypesDocuments",
                columns: table => new
                {
                    ID_TypeDocument = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesDocuments", x => x.ID_TypeDocument);
                });

            migrationBuilder.CreateTable(
                name: "Retenues",
                columns: table => new
                {
                    ID_Retenue = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Taux = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Categorie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_FactureAchat = table.Column<int>(type: "int", nullable: false),
                    FactureAchatID_FactureAchat = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Retenues", x => x.ID_Retenue);
                    table.ForeignKey(
                        name: "FK_Retenues_FacturesAchat_FactureAchatID_FactureAchat",
                        column: x => x.FactureAchatID_FactureAchat,
                        principalTable: "FacturesAchat",
                        principalColumn: "ID_FactureAchat");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Retenues_FactureAchatID_FactureAchat",
                table: "Retenues",
                column: "FactureAchatID_FactureAchat");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Retenues");

            migrationBuilder.DropTable(
                name: "TypesDocuments");

            migrationBuilder.DropTable(
                name: "FacturesAchat");

            migrationBuilder.AlterColumn<byte[]>(
                name: "CachetSignature",
                table: "Societes",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);
        }
    }
}
