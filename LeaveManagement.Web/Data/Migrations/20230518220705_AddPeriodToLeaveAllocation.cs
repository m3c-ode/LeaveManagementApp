using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPeriodToLeaveAllocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3cae19ac-c7b1-4e4c-a17f-09f839353abc");

            migrationBuilder.AddColumn<int>(
                name: "Period",
                table: "LeaveAllocations",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Period",
                table: "LeaveAllocations");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3cae19ac-c7b1-4e4c-a17f-09f839353abc", null, "Public", "PUBLIC" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e303bf5c-2604-4914-bc01-1988991bc4e3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "578b33cd-f466-4a6f-ab29-90921170024b", "AQAAAAIAAYagAAAAEJTuKgLKtVOhwIid0zO7jWtgTKPZ9YN4fAQHRDAp03k1NkMLLjgzgllrsIAl3qQ4CQ==", "44801b71-512d-4133-88d7-38ca1d87e513" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f603bf5c-2604-4914-bc01-1988994a1dff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9f9de64-a9cb-4b7c-8115-0f0a587b536b", "AQAAAAIAAYagAAAAEEVvT7LBFOIrDagMczwkS/LUsXlL4L22rl5Wyh8dimpmVrqOrauzAIgOtm8Q7vbiSQ==", "888d4040-9e83-48e7-8428-1d797968cefb" });
        }
    }
}
