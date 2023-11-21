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
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0eddcd7a-b582-4637-ba9c-b1ed69cc361a"),
                columns: new[] { "ConcurrencyStamp", "Gender", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e2a27e6-308c-48c9-a41e-eafcd89ac0e5", (byte)0, "AQAAAAIAAYagAAAAEHiHOiOwkr3vYOPv6T81ZGZsM3ngWdmD0NcF6EvZD9mNaEJCq4pmHCK//ukPXz1G2A==", "6c4953fe-2da4-4da1-b4a5-025e5eecfdaa" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("70f36e69-f7b9-42fc-9e77-a59f3fd9bfac"),
                columns: new[] { "ConcurrencyStamp", "Gender", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a8f52338-0ec9-423b-968f-9d6b03fd709e", (byte)0, "AQAAAAIAAYagAAAAEJ5oenbS+Z1J/X2TABU800Dp9DDrucpQz0FSkk+InZeax7sSgoZd1bzmR/aVt8mFMg==", "be48e84e-d37c-4800-bf5f-5b52519a2954" });
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
                oldClrType: typeof(byte),
                oldType: "smallint");

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
