using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstateExplorer.Migrations
{
    /// <inheritdoc />
    public partial class V8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CurrencyValues",
                newName: "id");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Zgrade",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Nekretnine",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Zgrade");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Nekretnine");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "CurrencyValues",
                newName: "Id");
        }
    }
}
