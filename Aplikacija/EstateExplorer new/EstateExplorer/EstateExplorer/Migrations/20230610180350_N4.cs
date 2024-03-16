using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstateExplorer.Migrations
{
    /// <inheritdoc />
    public partial class N4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nekretnine_Zgrade_Zgradaid",
                table: "Nekretnine");

            migrationBuilder.AlterColumn<int>(
                name: "Zgradaid",
                table: "Nekretnine",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Nekretnine_Zgrade_Zgradaid",
                table: "Nekretnine",
                column: "Zgradaid",
                principalTable: "Zgrade", 
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nekretnine_Zgrade_Zgradaid",
                table: "Nekretnine");

            migrationBuilder.AlterColumn<int>(
                name: "Zgradaid",
                table: "Nekretnine",
                type: "int",
                nullable: false,
                defaultValue: 0, 
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Nekretnine_Zgrade_Zgradaid",
                table: "Nekretnine",
                column: "Zgradaid",
                principalTable: "Zgrade",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
