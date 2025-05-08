using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotasUnivoDev.Migrations
{
    /// <inheritdoc />
    public partial class CareersSubjectsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CareersSubjects",
                columns: table => new
                {
                    CareersSubjetcId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CareerId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    Cycle = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Edited = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditeBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CareersSubjects", x => x.CareersSubjetcId);
                    table.ForeignKey(
                        name: "FK_CareersSubjects_Careers_CareerId",
                        column: x => x.CareerId,
                        principalTable: "Careers",
                        principalColumn: "CareerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CareersSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CareersSubjects_CareerId",
                table: "CareersSubjects",
                column: "CareerId");

            migrationBuilder.CreateIndex(
                name: "IX_CareersSubjects_SubjectId",
                table: "CareersSubjects",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CareersSubjects");
        }
    }
}
