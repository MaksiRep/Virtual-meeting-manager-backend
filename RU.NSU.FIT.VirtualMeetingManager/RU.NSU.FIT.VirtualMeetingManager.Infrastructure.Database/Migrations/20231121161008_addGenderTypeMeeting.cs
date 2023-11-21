using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RU.NSU.FIT.VirtualMeetingManager.Migrations
{
    /// <inheritdoc />
    public partial class addGenderTypeMeeting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Meetings");

            migrationBuilder.RenameColumn(
                name: "Sex",
                table: "Meetings",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Meetings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Meetings");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Meetings",
                newName: "Sex");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Meetings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0eddcd7a-b582-4637-ba9c-b1ed69cc361a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67ae8cc2-a4a8-47ea-8ae3-413834ef89bb", "AQAAAAIAAYagAAAAEFBxofNtJTP2Hzcqzvk9XK5YOABC2n9WVPKnXBAyxjN6MyNWE+NTgtHZ32Om/rqKWQ==", "6b6e4060-e627-4a0a-be95-a4667efc9db4" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("70f36e69-f7b9-42fc-9e77-a59f3fd9bfac"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "67fa3b44-c077-4ba5-b506-dda345a36632", "AQAAAAIAAYagAAAAEO+VSvtV0kbcdJyvN0fY7jhVRgwyXDXKRNEPms7CFI27D0ue47+nklkl7lvI1bUIxw==", "339643b2-bcc7-4042-ae96-f042678205e1" });
        }
    }
}
