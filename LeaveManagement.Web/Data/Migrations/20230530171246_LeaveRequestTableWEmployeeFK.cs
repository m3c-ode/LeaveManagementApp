using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class LeaveRequestTableWEmployeeFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LeaveTypeId = table.Column<int>(type: "int", nullable: false),
                    RequestComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: true),
                    Cancelled = table.Column<bool>(type: "bit", nullable: true),
                    RequestingEmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_AspNetUsers_RequestingEmployeeId",
                        column: x => x.RequestingEmployeeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_LeaveTypes_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalTable: "LeaveTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e303bf5c-2604-4914-bc01-1988991bc4e3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1df901b0-b90c-4ff3-9b29-a05cbc454163", "AQAAAAIAAYagAAAAECTm3e6uv31bvoH+wPr2HWG9mCi0AclUpVqk9+5bRIysOBK5MGYJVfddRsbmxOgPVA==", "72cccb8e-fe20-4680-81fe-25f8c24614cf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f603bf5c-2604-4914-bc01-1988994a1dff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f612b6d3-3896-4af7-a892-3c182aacafc3", "AQAAAAIAAYagAAAAEG3L3f6p+h2nGlnl1ML+W4493FBRmhw0g/nuvWDWbWJvYqrWbyVRSmdcHVRbf2FUuA==", "8e081d6c-b457-492d-ac1d-f3fbb44d32f4" });

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveTypeId",
                table: "LeaveRequests",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_RequestingEmployeeId",
                table: "LeaveRequests",
                column: "RequestingEmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeaveRequests");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e303bf5c-2604-4914-bc01-1988991bc4e3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "72d7f987-2358-4b41-95a5-1de997b369ac", "AQAAAAIAAYagAAAAEGIs1UQcwrYd+d2tJm7aLQSR17jyNtGYnO1LTJCtkK4KXnkwvKX6QD0j/iboVDh/XA==", "0e5fbc40-12e4-48a4-b229-d81c7a2d9a92" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f603bf5c-2604-4914-bc01-1988994a1dff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a302ddac-8497-4182-9796-99ca35b62e0c", "AQAAAAIAAYagAAAAEPl2L7yf5C6yNLAg+czDaUXNw7I1QlE29S1b1FpDqX+1mqFwww9ttZQpgRgl5TNUMg==", "1f577143-a538-4cbf-ac58-7b2b6d8ce985" });
        }
    }
}
