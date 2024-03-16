using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstateExplorer.Migrations
{
    /// <inheritdoc />
    public partial class V6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nekretnine_Zgrade_ZgradaId",
                table: "Nekretnine");

            migrationBuilder.DropForeignKey(
                name: "FK_Rate_Nekretnine_NekretninaId",
                table: "Rate");

            migrationBuilder.DropForeignKey(
                name: "FK_VidjenaObavestenja_Obavestenja_ObavestenjeId",
                table: "VidjenaObavestenja");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Zgrade",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ZakazivanjeTermina",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ObavestenjeId",
                table: "VidjenaObavestenja",
                newName: "Obavestenjeid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "VidjenaObavestenja",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_VidjenaObavestenja_ObavestenjeId",
                table: "VidjenaObavestenja",
                newName: "IX_VidjenaObavestenja_Obavestenjeid");

            migrationBuilder.RenameColumn(
                name: "NekretninaId",
                table: "Rate",
                newName: "Nekretninaid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Rate",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Rate_NekretninaId",
                table: "Rate",
                newName: "IX_Rate_Nekretninaid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Obavestenja",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ZgradaId",
                table: "Nekretnine",
                newName: "Zgradaid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Nekretnine",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Nekretnine_ZgradaId",
                table: "Nekretnine",
                newName: "IX_Nekretnine_Zgradaid");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Nedostupnosti",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nekretnine_Zgrade_Zgradaid",
                table: "Nekretnine",
                column: "Zgradaid",
                principalTable: "Zgrade",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rate_Nekretnine_Nekretninaid",
                table: "Rate",
                column: "Nekretninaid",
                principalTable: "Nekretnine",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_VidjenaObavestenja_Obavestenja_Obavestenjeid",
                table: "VidjenaObavestenja",
                column: "Obavestenjeid",
                principalTable: "Obavestenja",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nekretnine_Zgrade_Zgradaid",
                table: "Nekretnine");

            migrationBuilder.DropForeignKey(
                name: "FK_Rate_Nekretnine_Nekretninaid",
                table: "Rate");

            migrationBuilder.DropForeignKey(
                name: "FK_VidjenaObavestenja_Obavestenja_Obavestenjeid",
                table: "VidjenaObavestenja");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Zgrade",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ZakazivanjeTermina",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Obavestenjeid",
                table: "VidjenaObavestenja",
                newName: "ObavestenjeId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "VidjenaObavestenja",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_VidjenaObavestenja_Obavestenjeid",
                table: "VidjenaObavestenja",
                newName: "IX_VidjenaObavestenja_ObavestenjeId");

            migrationBuilder.RenameColumn(
                name: "Nekretninaid",
                table: "Rate",
                newName: "NekretninaId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Rate",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Rate_Nekretninaid",
                table: "Rate",
                newName: "IX_Rate_NekretninaId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Obavestenja",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Zgradaid",
                table: "Nekretnine",
                newName: "ZgradaId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Nekretnine",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Nekretnine_Zgradaid",
                table: "Nekretnine",
                newName: "IX_Nekretnine_ZgradaId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Nedostupnosti",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nekretnine_Zgrade_ZgradaId",
                table: "Nekretnine",
                column: "ZgradaId",
                principalTable: "Zgrade",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rate_Nekretnine_NekretninaId",
                table: "Rate",
                column: "NekretninaId",
                principalTable: "Nekretnine",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VidjenaObavestenja_Obavestenja_ObavestenjeId",
                table: "VidjenaObavestenja",
                column: "ObavestenjeId",
                principalTable: "Obavestenja",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
