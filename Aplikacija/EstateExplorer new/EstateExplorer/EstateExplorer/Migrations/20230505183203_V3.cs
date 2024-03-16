using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstateExplorer.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rate_Nekretnine_NekretninaId",
                table: "Rate");

            migrationBuilder.AlterColumn<int>(
                name: "NekretninaId",
                table: "Rate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Rate_Nekretnine_NekretninaId",
                table: "Rate",
                column: "NekretninaId",
                principalTable: "Nekretnine",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rate_Nekretnine_NekretninaId",
                table: "Rate");

            migrationBuilder.AlterColumn<int>(
                name: "NekretninaId",
                table: "Rate",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rate_Nekretnine_NekretninaId",
                table: "Rate",
                column: "NekretninaId",
                principalTable: "Nekretnine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
