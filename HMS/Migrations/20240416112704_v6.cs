using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Migrations
{
    /// <inheritdoc />
    public partial class v6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prescriptions",
                columns: table => new
                {
                    Prescription_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dosage = table.Column<int>(type: "int", nullable: false),
                    Asigned_To_PatientPatient_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prescriptions", x => x.Prescription_ID);
                    table.ForeignKey(
                        name: "FK_Prescriptions_Patients_Asigned_To_PatientPatient_ID",
                        column: x => x.Asigned_To_PatientPatient_ID,
                        principalTable: "Patients",
                        principalColumn: "Patient_ID");
                });

            migrationBuilder.CreateTable(
                name: "MedicinePrescription",
                columns: table => new
                {
                    Asigned_PrescriptionsPrescription_ID = table.Column<int>(type: "int", nullable: false),
                    Prescripted_MedicinesMedicine_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicinePrescription", x => new { x.Asigned_PrescriptionsPrescription_ID, x.Prescripted_MedicinesMedicine_ID });
                    table.ForeignKey(
                        name: "FK_MedicinePrescription_Medicines_Prescripted_MedicinesMedicine_ID",
                        column: x => x.Prescripted_MedicinesMedicine_ID,
                        principalTable: "Medicines",
                        principalColumn: "Medicine_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicinePrescription_Prescriptions_Asigned_PrescriptionsPrescription_ID",
                        column: x => x.Asigned_PrescriptionsPrescription_ID,
                        principalTable: "Prescriptions",
                        principalColumn: "Prescription_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicinePrescription_Prescripted_MedicinesMedicine_ID",
                table: "MedicinePrescription",
                column: "Prescripted_MedicinesMedicine_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_Asigned_To_PatientPatient_ID",
                table: "Prescriptions",
                column: "Asigned_To_PatientPatient_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicinePrescription");

            migrationBuilder.DropTable(
                name: "Prescriptions");
        }
    }
}
