using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWayToGerman.Core.Migrations
{
    /// <inheritdoc />
    public partial class addDefaultUserToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f7c68a6d-933b-4ee1-b729-fee0ad96e9c4"));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "text",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "bytea");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Email", "Name", "Password", "UpdateDate", "UserType", "Username" },
                values: new object[] { new Guid("402813df-0d72-4b3c-9645-3ad223c20f8f"), new DateTime(2023, 5, 8, 11, 55, 7, 850, DateTimeKind.Utc).AddTicks(7432), null, "masaylighto@gmail.com", "mohammed", "CPDk5fucxtvODK+MF0+aTUYOuKMtNki1m2dFSz8gbioATBjHPIk+IDMUkSeJKiQCIXOV9quaCwL0sAu5aYfrGA==", new DateTime(2023, 5, 8, 11, 55, 7, 850, DateTimeKind.Utc).AddTicks(7434), 1, "masaylighto" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("402813df-0d72-4b3c-9645-3ad223c20f8f"));

            migrationBuilder.AlterColumn<byte[]>(
                name: "Password",
                table: "Users",
                type: "bytea",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Email", "Name", "Password", "UpdateDate", "UserType", "Username" },
                values: new object[] { new Guid("f7c68a6d-933b-4ee1-b729-fee0ad96e9c4"), new DateTime(2023, 5, 8, 11, 51, 31, 635, DateTimeKind.Utc).AddTicks(2795), null, "masaylighto@gmail.com", "mohammed", new byte[] { 8, 240, 228, 229, 251, 156, 198, 219, 206, 12, 175, 140, 23, 79, 154, 77, 70, 14, 184, 163, 45, 54, 72, 181, 155, 103, 69, 75, 63, 32, 110, 42, 0, 76, 24, 199, 60, 137, 62, 32, 51, 20, 145, 39, 137, 42, 36, 2, 33, 115, 149, 246, 171, 154, 11, 2, 244, 176, 11, 185, 105, 135, 235, 24 }, new DateTime(2023, 5, 8, 11, 51, 31, 635, DateTimeKind.Utc).AddTicks(2796), 1, "masaylighto" });
        }
    }
}
