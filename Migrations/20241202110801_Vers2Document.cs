using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Administration.Migrations
{
    /// <inheritdoc />
    public partial class Vers2Document : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID_Societe",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID_Societe",
                table: "Documents");
        }
    }
}
