using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RU.NSU.FIT.VirtualMeetingManager.Migrations
{
    /// <inheritdoc />
    public partial class SetImageUrlNorRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Meetings",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0eddcd7a-b582-4637-ba9c-b1ed69cc361a"),
                columns: new[] { "ConcurrencyStamp", "Gender", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea0c31a3-0ef5-4bdd-9f4c-88a3edea8e8e", (byte)2, "AQAAAAIAAYagAAAAEHMxIM3mh2SerXiic5NMzq3lrORnhu8WXvkdEtUSR4609yt22I4oDs1KVMMDWRMn8g==", "7a99fe45-8ffb-4fd9-b861-bea055d5a9d9" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("70f36e69-f7b9-42fc-9e77-a59f3fd9bfac"),
                columns: new[] { "ConcurrencyStamp", "Gender", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a75da166-1399-4f47-b702-1797d907d678", (byte)2, "AQAAAAIAAYagAAAAEEyjPal7/FqKvAjNGX8NzxWRtPoJjrfjq7YO8Ea+uGcv39mkC6hguTi3ZGYPbKsWRw==", "c4be35eb-1b9a-4a53-b5a7-d3c8d2478543" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Meetings",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0eddcd7a-b582-4637-ba9c-b1ed69cc361a"),
                columns: new[] { "ConcurrencyStamp", "Gender", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11e2a195-6473-474c-939f-8eb98bd05dd1", (byte)0, "AQAAAAIAAYagAAAAEOXbaYd7EFX/wmO5oNAI3X7inA9ftPD3eOnuIQGZwqmn+Y354ztQ2LL9CJVcpBqqMw==", "82948483-f185-4145-b11f-d682298105f1" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("70f36e69-f7b9-42fc-9e77-a59f3fd9bfac"),
                columns: new[] { "ConcurrencyStamp", "Gender", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0ceb8e16-adf5-4e86-937b-3e8cda4efc80", (byte)0, "AQAAAAIAAYagAAAAEFp6FXXzDauomyKsjBnDKbU515vtjjiEOIT/dF61iA4G/nwWJUP6xVAgwd/mVRvxKA==", "edd8de79-758b-41ec-8891-b6f29273f261" });
        }
    }
}
