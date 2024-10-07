using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Administration.Migrations
{
    /// <inheritdoc />
    public partial class AjoutFacturesEtLignes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacturesVente",
                columns: table => new
                {
                    ID_FactureVente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroFacture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateFacture = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total_FactureVente = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TimbreFiscale = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClientID_Client = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturesVente", x => x.ID_FactureVente);
                    table.ForeignKey(
                        name: "FK_FacturesVente_Clients_ClientID_Client",
                        column: x => x.ClientID_Client,
                        principalTable: "Clients",
                        principalColumn: "ID_Client",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LignesFacture",
                columns: table => new
                {
                    ID_LigneFV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Service = table.Column<int>(type: "int", nullable: false),
                    ServiceID_Service = table.Column<int>(type: "int", nullable: false),
                    ID_FactureVente = table.Column<int>(type: "int", nullable: false),
                    FactureVenteID_FactureVente = table.Column<int>(type: "int", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    PrixUnitaire = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total_LigneFV = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LignesFacture", x => x.ID_LigneFV);
                    table.ForeignKey(
                        name: "FK_LignesFacture_FacturesVente_FactureVenteID_FactureVente",
                        column: x => x.FactureVenteID_FactureVente,
                        principalTable: "FacturesVente",
                        principalColumn: "ID_FactureVente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LignesFacture_Services_ServiceID_Service",
                        column: x => x.ServiceID_Service,
                        principalTable: "Services",
                        principalColumn: "ID_Service",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacturesVente_ClientID_Client",
                table: "FacturesVente",
                column: "ClientID_Client");

            migrationBuilder.CreateIndex(
                name: "IX_LignesFacture_FactureVenteID_FactureVente",
                table: "LignesFacture",
                column: "FactureVenteID_FactureVente");

            migrationBuilder.CreateIndex(
                name: "IX_LignesFacture_ServiceID_Service",
                table: "LignesFacture",
                column: "ServiceID_Service");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LignesFacture");

            migrationBuilder.DropTable(
                name: "FacturesVente");
        }
    }
}
