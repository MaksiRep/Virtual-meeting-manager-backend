using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RU.NSU.FIT.VirtualMeetingManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMeetingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Meetings",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0eddcd7a-b582-4637-ba9c-b1ed69cc361a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11e2a195-6473-474c-939f-8eb98bd05dd1", "AQAAAAIAAYagAAAAEOXbaYd7EFX/wmO5oNAI3X7inA9ftPD3eOnuIQGZwqmn+Y354ztQ2LL9CJVcpBqqMw==", "82948483-f185-4145-b11f-d682298105f1" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("70f36e69-f7b9-42fc-9e77-a59f3fd9bfac"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0ceb8e16-adf5-4e86-937b-3e8cda4efc80", "AQAAAAIAAYagAAAAEFp6FXXzDauomyKsjBnDKbU515vtjjiEOIT/dF61iA4G/nwWJUP6xVAgwd/mVRvxKA==", "edd8de79-758b-41ec-8891-b6f29273f261" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Meetings");

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
    }
}
