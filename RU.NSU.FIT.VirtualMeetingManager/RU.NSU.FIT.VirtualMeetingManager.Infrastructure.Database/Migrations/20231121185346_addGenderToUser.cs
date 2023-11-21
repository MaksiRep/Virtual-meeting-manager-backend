using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RU.NSU.FIT.VirtualMeetingManager.Migrations
{
    /// <inheritdoc />
    public partial class addGenderToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Gender",
                table: "Users",
                type: "smallint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AlterColumn<byte>(
                name: "Gender",
                table: "Meetings",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0eddcd7a-b582-4637-ba9c-b1ed69cc361a"),
                columns: new[] { "ConcurrencyStamp", "Gender", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b06973f-2abb-416a-94f8-13b8ef626098", (byte)0, "AQAAAAIAAYagAAAAENL6gnbbUA8gw1EuamW0A4FpDhc4r0R5nK2e7fI60ZvORATJvK45LFAhws0kKZQcqw==", "07c2ce28-3847-45ab-809e-ec785fabbfe8" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("70f36e69-f7b9-42fc-9e77-a59f3fd9bfac"),
                columns: new[] { "ConcurrencyStamp", "Gender", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f9e5a2e-558e-43c2-b02b-6f38fc34c7ce", (byte)0, "AQAAAAIAAYagAAAAENTbPDZAs/gaUEJVgJ11HFYSTx955MzAT4zVENho91z5+ipgiC5Ir8uAMSRDOHquEA==", "07cdf7ec-d43a-48dc-aad7-cf8e9abb498a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Meetings",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(byte),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0eddcd7a-b582-4637-ba9c-b1ed69cc361a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "69bd6e62-3003-48f7-b28f-ff28f2e1c226", "AQAAAAIAAYagAAAAELcdhTLqSZKNZbdbbPPKKN/PfpAn4ud/o2qY7pW95iENzEs3TdBlR+IdLtsr1eT7jQ==", "3b209f4e-970d-438b-a8e3-a214894938d6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("70f36e69-f7b9-42fc-9e77-a59f3fd9bfac"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5fd383ac-5f68-4397-9d74-8f2b02ba5de3", "AQAAAAIAAYagAAAAEDuFV7CkepW771AFdFhNXSfW6Q7Lz4ZE8fnwvwVrc5yqxWp69k5QeKxVtATLqlcjjw==", "8d268aa0-63c3-43d8-9482-f61228e41229" });
        }
    }
}
