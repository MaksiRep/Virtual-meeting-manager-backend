using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RU.NSU.FIT.VirtualMeetingManager.Migrations
{
    /// <inheritdoc />
    public partial class NullabilityFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MinAge",
                table: "Meetings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "MaxUsers",
                table: "Meetings",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0eddcd7a-b582-4637-ba9c-b1ed69cc361a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3ce39213-4fb8-4b4a-8d51-080c689abd51", "AQAAAAIAAYagAAAAEAdhqUaH+KWG3q3WGe6uVtmu59sT+Oj9Vn9bQsy9f/rzH+DViPeRq+C/fL4TVlTzPQ==", "4c50b4ea-2657-41b2-ad24-f42156cb4ffd" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("70f36e69-f7b9-42fc-9e77-a59f3fd9bfac"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c957c978-5eea-4c8e-971b-1557ba3415e3", "AQAAAAIAAYagAAAAEO1axLhoa9zxo7fOalKcdvkWFUYY5AeNBNJNicLjO9jy9WImgBqS6zNyLbkJb6FdOQ==", "4a7fb4bc-4e1f-479f-987e-ccb697ec01a0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MinAge",
                table: "Meetings",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaxUsers",
                table: "Meetings",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0eddcd7a-b582-4637-ba9c-b1ed69cc361a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b06973f-2abb-416a-94f8-13b8ef626098", "AQAAAAIAAYagAAAAENL6gnbbUA8gw1EuamW0A4FpDhc4r0R5nK2e7fI60ZvORATJvK45LFAhws0kKZQcqw==", "07c2ce28-3847-45ab-809e-ec785fabbfe8" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("70f36e69-f7b9-42fc-9e77-a59f3fd9bfac"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f9e5a2e-558e-43c2-b02b-6f38fc34c7ce", "AQAAAAIAAYagAAAAENTbPDZAs/gaUEJVgJ11HFYSTx955MzAT4zVENho91z5+ipgiC5Ir8uAMSRDOHquEA==", "07cdf7ec-d43a-48dc-aad7-cf8e9abb498a" });
        }
    }
}
