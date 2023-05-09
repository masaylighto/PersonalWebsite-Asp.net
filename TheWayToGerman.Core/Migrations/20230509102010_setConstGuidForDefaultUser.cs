using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheWayToGerman.Core.Migrations
{
    /// <inheritdoc />
    public partial class setConstGuidForDefaultUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("402813df-0d72-4b3c-9645-3ad223c20f8f"));
            
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Email", "Name", "Password", "UpdateDate", "UserType", "Username" },
                values: new object[] { new Guid("4413e488-a54e-43df-b381-f48fc81b7080"), new DateTime(2023, 5, 9, 10, 20, 10, 290, DateTimeKind.Utc).AddTicks(7685), null, "masaylighto@gmail.com", "mohammed", "CPDk5fucxtvODK+MF0+aTUYOuKMtNki1m2dFSz8gbioATBjHPIk+IDMUkSeJKiQCIXOV9quaCwL0sAu5aYfrGA==", new DateTime(2023, 5, 9, 10, 20, 10, 290, DateTimeKind.Utc).AddTicks(7686), 1, "masaylighto" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4413e488-a54e-43df-b381-f48fc81b7080"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateDate", "DeleteDate", "Email", "Name", "Password", "UpdateDate", "UserType", "Username" },
                values: new object[] { new Guid("402813df-0d72-4b3c-9645-3ad223c20f8f"), new DateTime(2023, 5, 8, 11, 55, 7, 850, DateTimeKind.Utc).AddTicks(7432), null, "masaylighto@gmail.com", "mohammed", "CPDk5fucxtvODK+MF0+aTUYOuKMtNki1m2dFSz8gbioATBjHPIk+IDMUkSeJKiQCIXOV9quaCwL0sAu5aYfrGA==", new DateTime(2023, 5, 8, 11, 55, 7, 850, DateTimeKind.Utc).AddTicks(7434), 1, "masaylighto" });
        }
    }
}
