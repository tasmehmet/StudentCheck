using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentCheckApp.Data.Migrations
{
    public partial class UpdateDbTableForTeacernadStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Students_StudentsID",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_StudentsID",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "StudentsID",
                table: "Teachers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentsID",
                table: "Teachers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_StudentsID",
                table: "Teachers",
                column: "StudentsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Students_StudentsID",
                table: "Teachers",
                column: "StudentsID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
