using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KlidecekIS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Activitycanhavemultiplegrades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Students_StudentEntityId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_ActivityId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_StudentEntityId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "StudentEntityId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "ActivityEntities");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Students",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_ActivityId",
                table: "Grades",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentId",
                table: "Grades",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Students_StudentId",
                table: "Grades",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Students_StudentId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_ActivityId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_StudentId",
                table: "Grades");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentEntityId",
                table: "Grades",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "GradeId",
                table: "ActivityEntities",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Grades_ActivityId",
                table: "Grades",
                column: "ActivityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentEntityId",
                table: "Grades",
                column: "StudentEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Students_StudentEntityId",
                table: "Grades",
                column: "StudentEntityId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
