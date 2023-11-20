using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace proekt.Migrations
{
    /// <inheritdoc />
    public partial class cr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    student_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор записи студента")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_student_firstname = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Имя студента"),
                    c_student_lastname = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Фамилия студента"),
                    c_student_secname = table.Column<string>(type: "varchar", maxLength: 100, nullable: false, comment: "Отчество студента")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_(TableName)_StudentId", x => x.student_id);
                });

            migrationBuilder.CreateTable(
                name: "StudSession",
                columns: table => new
                {
                    grade_id = table.Column<int>(type: "integer", nullable: false, comment: "Идентификатор успеваемости")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    c_student_gradenumber = table.Column<int>(type: "int4", nullable: false, comment: "Оценка"),
                    c_student_gradedate = table.Column<DateTime>(type: "timestamp", nullable: false, comment: "Дата оценки"),
                    c_student_studentid = table.Column<int>(type: "int4", nullable: false, comment: "Идентификатор студента")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_(TableName)_GradeId", x => x.grade_id);
                    table.ForeignKey(
                        name: "fk_f_student_id",
                        column: x => x.c_student_studentid,
                        principalTable: "Students",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "idx_(TableName)_fk_f_student_id",
                table: "StudSession",
                column: "c_student_studentid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudSession");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
