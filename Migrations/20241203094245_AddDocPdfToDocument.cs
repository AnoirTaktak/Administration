using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Administration.Migrations
{
    /// <inheritdoc />
    public partial class AddDocPdfToDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Doc_Pdf",
                table: "Documents",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Doc_Pdf",
                table: "Documents");
        }
    }
}
