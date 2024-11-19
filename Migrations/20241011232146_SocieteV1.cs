using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Administration.Migrations
{
    /// <inheritdoc />
    public partial class SocieteV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cachet",
                table: "Societes");

            migrationBuilder.RenameColumn(
                name: "Signature",
                table: "Societes",
                newName: "CachetSignature");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CachetSignature",
                table: "Societes",
                newName: "Signature");

            migrationBuilder.AddColumn<byte[]>(
                name: "Cachet",
                table: "Societes",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
