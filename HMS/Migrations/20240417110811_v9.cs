using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Migrations
{
    /// <inheritdoc />
    public partial class v9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmergencyContacts",
                columns: table => new
                {
                    EmergencyContact_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmergencyContact_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyContact_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyContact_Realtion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelatedPatient_IdPatient_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyContacts", x => x.EmergencyContact_ID);
                    table.ForeignKey(
                        name: "FK_EmergencyContacts_Patients_RelatedPatient_IdPatient_ID",
                        column: x => x.RelatedPatient_IdPatient_ID,
                        principalTable: "Patients",
                        principalColumn: "Patient_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyContacts_RelatedPatient_IdPatient_ID",
                table: "EmergencyContacts",
                column: "RelatedPatient_IdPatient_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmergencyContacts");
        }
    }
}
