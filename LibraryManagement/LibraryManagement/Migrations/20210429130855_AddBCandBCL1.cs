using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryManagement.Migrations
{
    public partial class AddBCandBCL1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCopy_Edition_EditionId",
                table: "BookCopy");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopy_Edition_EditionId",
                table: "BookCopy",
                column: "EditionId",
                principalTable: "Edition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCopy_Edition_EditionId",
                table: "BookCopy");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopy_Edition_EditionId",
                table: "BookCopy",
                column: "EditionId",
                principalTable: "Edition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
