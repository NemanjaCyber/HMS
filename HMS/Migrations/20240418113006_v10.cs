using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Migrations
{
    /// <inheritdoc />
    public partial class v10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Department_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Department_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department_EmplCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Department_ID);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Doctor_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Doctor_Fname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_Lname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_Qualification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_Specialization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_DateJoining = table.Column<DateOnly>(type: "date", nullable: false),
                    Doctor_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor_Department_IdDepartment_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Doctor_ID);
                    table.ForeignKey(
                        name: "FK_Doctors_Departments_Doctor_Department_IdDepartment_ID",
                        column: x => x.Doctor_Department_IdDepartment_ID,
                        principalTable: "Departments",
                        principalColumn: "Department_ID");
                });

            migrationBuilder.CreateTable(
                name: "Nurses",
                columns: table => new
                {
                    Nurse_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nurse_Fname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nurse_Lname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nurse_DateJoining = table.Column<DateOnly>(type: "date", nullable: false),
                    Nurse_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nurse_Department_IdDepartment_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nurses", x => x.Nurse_ID);
                    table.ForeignKey(
                        name: "FK_Nurses_Departments_Nurse_Department_IdDepartment_ID",
                        column: x => x.Nurse_Department_IdDepartment_ID,
                        principalTable: "Departments",
                        principalColumn: "Department_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Doctor_Department_IdDepartment_ID",
                table: "Doctors",
                column: "Doctor_Department_IdDepartment_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_Nurse_Department_IdDepartment_ID",
                table: "Nurses",
                column: "Nurse_Department_IdDepartment_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Nurses");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
