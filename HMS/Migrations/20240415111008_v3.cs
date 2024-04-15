using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMS.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Assigned_Room_IdRoom_ID",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Room_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Room_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Room_Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Room_ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Assigned_Room_IdRoom_ID",
                table: "Patients",
                column: "Assigned_Room_IdRoom_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Rooms_Assigned_Room_IdRoom_ID",
                table: "Patients",
                column: "Assigned_Room_IdRoom_ID",
                principalTable: "Rooms",
                principalColumn: "Room_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Rooms_Assigned_Room_IdRoom_ID",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Patients_Assigned_Room_IdRoom_ID",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Assigned_Room_IdRoom_ID",
                table: "Patients");
        }
    }
}
