using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstateExplorer.Migrations
{
    /// <inheritdoc />
    public partial class V5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrencyValues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ExchangeMiddle = table.Column<double>(type: "float", nullable: false),
                    CashBuy = table.Column<double>(type: "float", nullable: false),
                    CashSell = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyValues", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyValues_Code",
                table: "CurrencyValues",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyValues");
        }
    }
}
