using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Administration.Migrations
{
    /// <inheritdoc />
    public partial class AddCachetAndSignatureToSociete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID_Fourinisseur",
                table: "Fournisseurs",
                newName: "ID_Fournisseur");

            migrationBuilder.AddColumn<string>(
                name: "Adresse_Societe",
                table: "Societes",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Cachet",
                table: "Societes",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "CodePostal",
                table: "Societes",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MF_Societe",
                table: "Societes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RaisonSociale_Societe",
                table: "Societes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte[]>(
                name: "Signature",
                table: "Societes",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "Tel_Societe",
                table: "Societes",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Adresse_Fournisseur",
                table: "Fournisseurs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email_Fournisseur",
                table: "Fournisseurs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MF_Fournisseur",
                table: "Fournisseurs",
                type: "int",
                maxLength: 20,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RaisonSociale_Fournisseur",
                table: "Fournisseurs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tel_Fournisseur",
                table: "Fournisseurs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type_Fournisseur",
                table: "Fournisseurs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CIN_Employe",
                table: "Employes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CNSS_Employe",
                table: "Employes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDebut",
                table: "Employes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateFin",
                table: "Employes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email_Employe",
                table: "Employes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nom_Employe",
                table: "Employes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Poste_Employe",
                table: "Employes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Salaire",
                table: "Employes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "TypeContrat",
                table: "Employes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Adresse_Client",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MF_Client",
                table: "Clients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RS_Client",
                table: "Clients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tel_Client",
                table: "Clients",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type_Client",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adresse_Societe",
                table: "Societes");

            migrationBuilder.DropColumn(
                name: "Cachet",
                table: "Societes");

            migrationBuilder.DropColumn(
                name: "CodePostal",
                table: "Societes");

            migrationBuilder.DropColumn(
                name: "MF_Societe",
                table: "Societes");

            migrationBuilder.DropColumn(
                name: "RaisonSociale_Societe",
                table: "Societes");

            migrationBuilder.DropColumn(
                name: "Signature",
                table: "Societes");

            migrationBuilder.DropColumn(
                name: "Tel_Societe",
                table: "Societes");

            migrationBuilder.DropColumn(
                name: "Adresse_Fournisseur",
                table: "Fournisseurs");

            migrationBuilder.DropColumn(
                name: "Email_Fournisseur",
                table: "Fournisseurs");

            migrationBuilder.DropColumn(
                name: "MF_Fournisseur",
                table: "Fournisseurs");

            migrationBuilder.DropColumn(
                name: "RaisonSociale_Fournisseur",
                table: "Fournisseurs");

            migrationBuilder.DropColumn(
                name: "Tel_Fournisseur",
                table: "Fournisseurs");

            migrationBuilder.DropColumn(
                name: "Type_Fournisseur",
                table: "Fournisseurs");

            migrationBuilder.DropColumn(
                name: "CIN_Employe",
                table: "Employes");

            migrationBuilder.DropColumn(
                name: "CNSS_Employe",
                table: "Employes");

            migrationBuilder.DropColumn(
                name: "DateDebut",
                table: "Employes");

            migrationBuilder.DropColumn(
                name: "DateFin",
                table: "Employes");

            migrationBuilder.DropColumn(
                name: "Email_Employe",
                table: "Employes");

            migrationBuilder.DropColumn(
                name: "Nom_Employe",
                table: "Employes");

            migrationBuilder.DropColumn(
                name: "Poste_Employe",
                table: "Employes");

            migrationBuilder.DropColumn(
                name: "Salaire",
                table: "Employes");

            migrationBuilder.DropColumn(
                name: "TypeContrat",
                table: "Employes");

            migrationBuilder.DropColumn(
                name: "Adresse_Client",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "MF_Client",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "RS_Client",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Tel_Client",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Type_Client",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "ID_Fournisseur",
                table: "Fournisseurs",
                newName: "ID_Fourinisseur");
        }
    }
}
