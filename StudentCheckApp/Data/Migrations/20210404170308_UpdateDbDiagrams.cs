using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentCheckApp.Data.Migrations
{
    public partial class UpdateDbDiagrams : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_CheckDay_CheckDayID",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Homeworks_HomeworksID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_CheckDayID",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_HomeworksID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CheckDayID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "HomeworksID",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "StudentsID",
                table: "Homeworks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentsID",
                table: "CheckDay",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_StudentsID",
                table: "Homeworks",
                column: "StudentsID");

            migrationBuilder.CreateIndex(
                name: "IX_CheckDay_StudentsID",
                table: "CheckDay",
                column: "StudentsID");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckDay_Students_StudentsID",
                table: "CheckDay",
                column: "StudentsID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Homeworks_Students_StudentsID",
                table: "Homeworks",
                column: "StudentsID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckDay_Students_StudentsID",
                table: "CheckDay");

            migrationBuilder.DropForeignKey(
                name: "FK_Homeworks_Students_StudentsID",
                table: "Homeworks");

            migrationBuilder.DropIndex(
                name: "IX_Homeworks_StudentsID",
                table: "Homeworks");

            migrationBuilder.DropIndex(
                name: "IX_CheckDay_StudentsID",
                table: "CheckDay");

            migrationBuilder.DropColumn(
                name: "StudentsID",
                table: "Homeworks");

            migrationBuilder.DropColumn(
                name: "StudentsID",
                table: "CheckDay");

            migrationBuilder.AddColumn<int>(
                name: "CheckDayID",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HomeworksID",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_CheckDayID",
                table: "Students",
                column: "CheckDayID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_HomeworksID",
                table: "Students",
                column: "HomeworksID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_CheckDay_CheckDayID",
                table: "Students",
                column: "CheckDayID",
                principalTable: "CheckDay",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Homeworks_HomeworksID",
                table: "Students",
                column: "HomeworksID",
                principalTable: "Homeworks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
