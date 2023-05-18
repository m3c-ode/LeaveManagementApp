using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagement.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultUsersAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1aed16eb-c7b4-4e3a-a17f-09f839084ad7", null, "User", "USER" },
                    { "3cae19ac-c7b1-4e4c-a17f-09f839353abc", null, "Public", "PUBLIC" },
                    { "6ead16eb-a6a4-4e3d-a17f-09f839084ea3", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateJoined", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TaxId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "e303bf5c-2604-4914-bc01-1988991bc4e3", 0, "dd3b55cb-7ad0-4c48-ab4f-6a0b9a808083", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user@localhost.com", false, "System", "User", false, null, "USER@LOCALHOST.COM", null, "AQAAAAIAAYagAAAAEEXEZEtOygteRb19WZSsOblFHO+DEekzEbAG5/GhxLaJUKP7jdWmBXtl8w9h81isTw==", null, false, "a2f842af-950c-449a-8486-daa01b28a79e", null, false, null },
                    { "f603bf5c-2604-4914-bc01-1988994a1dff", 0, "76d35d39-15b8-4d8c-ab35-b09503a61ad9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@localhost.com", false, "System", "Admin", false, null, "ADMIN@LOCALHOST.COM", null, "AQAAAAIAAYagAAAAEBXwGl6S6byKADF9fYuIhAOqJR+Ixt8bgR11VM58igQwx/dbYipFM4SmS0RGLVQr4g==", null, false, "019acd56-3971-49cc-845f-f0f05852540e", null, false, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1aed16eb-c7b4-4e3a-a17f-09f839084ad7", "e303bf5c-2604-4914-bc01-1988991bc4e3" },
                    { "6ead16eb-a6a4-4e3d-a17f-09f839084ea3", "f603bf5c-2604-4914-bc01-1988994a1dff" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3cae19ac-c7b1-4e4c-a17f-09f839353abc");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1aed16eb-c7b4-4e3a-a17f-09f839084ad7", "e303bf5c-2604-4914-bc01-1988991bc4e3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6ead16eb-a6a4-4e3d-a17f-09f839084ea3", "f603bf5c-2604-4914-bc01-1988994a1dff" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1aed16eb-c7b4-4e3a-a17f-09f839084ad7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ead16eb-a6a4-4e3d-a17f-09f839084ea3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e303bf5c-2604-4914-bc01-1988991bc4e3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f603bf5c-2604-4914-bc01-1988994a1dff");
        }
    }
}
