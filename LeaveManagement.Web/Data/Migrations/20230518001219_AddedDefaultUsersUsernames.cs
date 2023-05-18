using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultUsersUsernames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e303bf5c-2604-4914-bc01-1988991bc4e3",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "578b33cd-f466-4a6f-ab29-90921170024b", true, "USER@LOCALHOST.COM", "AQAAAAIAAYagAAAAEJTuKgLKtVOhwIid0zO7jWtgTKPZ9YN4fAQHRDAp03k1NkMLLjgzgllrsIAl3qQ4CQ==", "44801b71-512d-4133-88d7-38ca1d87e513", "user@localhost.com" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f603bf5c-2604-4914-bc01-1988994a1dff",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "d9f9de64-a9cb-4b7c-8115-0f0a587b536b", true, "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEEVvT7LBFOIrDagMczwkS/LUsXlL4L22rl5Wyh8dimpmVrqOrauzAIgOtm8Q7vbiSQ==", "888d4040-9e83-48e7-8428-1d797968cefb", "admin@localhost.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e303bf5c-2604-4914-bc01-1988991bc4e3",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "dd3b55cb-7ad0-4c48-ab4f-6a0b9a808083", false, null, "AQAAAAIAAYagAAAAEEXEZEtOygteRb19WZSsOblFHO+DEekzEbAG5/GhxLaJUKP7jdWmBXtl8w9h81isTw==", "a2f842af-950c-449a-8486-daa01b28a79e", null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f603bf5c-2604-4914-bc01-1988994a1dff",
                columns: new[] { "ConcurrencyStamp", "EmailConfirmed", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "76d35d39-15b8-4d8c-ab35-b09503a61ad9", false, null, "AQAAAAIAAYagAAAAEBXwGl6S6byKADF9fYuIhAOqJR+Ixt8bgR11VM58igQwx/dbYipFM4SmS0RGLVQr4g==", "019acd56-3971-49cc-845f-f0f05852540e", null });
        }
    }
}
