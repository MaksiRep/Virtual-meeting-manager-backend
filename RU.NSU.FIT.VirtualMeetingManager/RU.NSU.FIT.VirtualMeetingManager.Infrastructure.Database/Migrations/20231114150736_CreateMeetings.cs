using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RU.NSU.FIT.VirtualMeetingManager.Migrations
{
    /// <inheritdoc />
    public partial class CreateMeetings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    MaxUsers = table.Column<int>(type: "integer", nullable: false),
                    MinAge = table.Column<int>(type: "integer", nullable: false),
                    Sex = table.Column<string>(type: "text", nullable: false),
                    ManagerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meetings_Users_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMeetings",
                columns: table => new
                {
                    MeetingsId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMeetings", x => new { x.MeetingsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_UserMeetings_Meetings_MeetingsId",
                        column: x => x.MeetingsId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMeetings_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_ManagerId",
                table: "Meetings",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMeetings_UsersId",
                table: "UserMeetings",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMeetings");

            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0eddcd7a-b582-4637-ba9c-b1ed69cc361a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3f1a473e-8774-4c8c-978c-bfe0daa9cdb6", "AQAAAAIAAYagAAAAEAi9bJTvk2nDlje+Xvx60sU9UO/4czm6BFUwC7gRxWSUpLbbsyPz0prXUSdYPufq6g==", "706f1ef6-361e-41d2-aeec-d9f7f1e3394b" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("70f36e69-f7b9-42fc-9e77-a59f3fd9bfac"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6529a386-dd02-4ad6-af5f-dcbe1ba38479", "AQAAAAIAAYagAAAAEFuYGp8RsBrsXsY3ArJ6riPWmM/hZF/AzGg8HBhcNubQitDAeo2mlgj/Fbf4+K6RWg==", "17a0c33f-83d5-40ee-a450-d4311ea56ed9" });
        }
    }
}
