using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Migrations
{
    /// <inheritdoc />
    public partial class v8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicinePrescription_Medicines_Prescripted_MedicinesMedicine_ID",
                table: "MedicinePrescription");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicinePrescription_Prescriptions_Asigned_PrescriptionsPrescription_ID",
                table: "MedicinePrescription");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Patients_Asigned_To_PatientPatient_ID",
                table: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "Asigned_To_PatientPatient_ID",
                table: "Prescriptions",
                newName: "Assigned_PatientPatient_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_Asigned_To_PatientPatient_ID",
                table: "Prescriptions",
                newName: "IX_Prescriptions_Assigned_PatientPatient_ID");

            migrationBuilder.RenameColumn(
                name: "Prescripted_MedicinesMedicine_ID",
                table: "MedicinePrescription",
                newName: "Assigned_To_PrescriptionsPrescription_ID");

            migrationBuilder.RenameColumn(
                name: "Asigned_PrescriptionsPrescription_ID",
                table: "MedicinePrescription",
                newName: "Assigned_MedicinesMedicine_ID");

            migrationBuilder.RenameIndex(
                name: "IX_MedicinePrescription_Prescripted_MedicinesMedicine_ID",
                table: "MedicinePrescription",
                newName: "IX_MedicinePrescription_Assigned_To_PrescriptionsPrescription_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinePrescription_Medicines_Assigned_MedicinesMedicine_ID",
                table: "MedicinePrescription",
                column: "Assigned_MedicinesMedicine_ID",
                principalTable: "Medicines",
                principalColumn: "Medicine_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinePrescription_Prescriptions_Assigned_To_PrescriptionsPrescription_ID",
                table: "MedicinePrescription",
                column: "Assigned_To_PrescriptionsPrescription_ID",
                principalTable: "Prescriptions",
                principalColumn: "Prescription_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Patients_Assigned_PatientPatient_ID",
                table: "Prescriptions",
                column: "Assigned_PatientPatient_ID",
                principalTable: "Patients",
                principalColumn: "Patient_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicinePrescription_Medicines_Assigned_MedicinesMedicine_ID",
                table: "MedicinePrescription");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicinePrescription_Prescriptions_Assigned_To_PrescriptionsPrescription_ID",
                table: "MedicinePrescription");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Patients_Assigned_PatientPatient_ID",
                table: "Prescriptions");

            migrationBuilder.RenameColumn(
                name: "Assigned_PatientPatient_ID",
                table: "Prescriptions",
                newName: "Asigned_To_PatientPatient_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Prescriptions_Assigned_PatientPatient_ID",
                table: "Prescriptions",
                newName: "IX_Prescriptions_Asigned_To_PatientPatient_ID");

            migrationBuilder.RenameColumn(
                name: "Assigned_To_PrescriptionsPrescription_ID",
                table: "MedicinePrescription",
                newName: "Prescripted_MedicinesMedicine_ID");

            migrationBuilder.RenameColumn(
                name: "Assigned_MedicinesMedicine_ID",
                table: "MedicinePrescription",
                newName: "Asigned_PrescriptionsPrescription_ID");

            migrationBuilder.RenameIndex(
                name: "IX_MedicinePrescription_Assigned_To_PrescriptionsPrescription_ID",
                table: "MedicinePrescription",
                newName: "IX_MedicinePrescription_Prescripted_MedicinesMedicine_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinePrescription_Medicines_Prescripted_MedicinesMedicine_ID",
                table: "MedicinePrescription",
                column: "Prescripted_MedicinesMedicine_ID",
                principalTable: "Medicines",
                principalColumn: "Medicine_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicinePrescription_Prescriptions_Asigned_PrescriptionsPrescription_ID",
                table: "MedicinePrescription",
                column: "Asigned_PrescriptionsPrescription_ID",
                principalTable: "Prescriptions",
                principalColumn: "Prescription_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Patients_Asigned_To_PatientPatient_ID",
                table: "Prescriptions",
                column: "Asigned_To_PatientPatient_ID",
                principalTable: "Patients",
                principalColumn: "Patient_ID");
        }
    }
}
