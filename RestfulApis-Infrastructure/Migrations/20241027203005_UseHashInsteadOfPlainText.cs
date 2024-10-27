using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestfulApis_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UseHashInsteadOfPlainText : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Hash");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Hash",
                value: "$2a$11$fk6flnBZPIz05Ay4ZQ/mXOCxwrrcuvkxxgXkkKYgyy11zV8lTYr5i");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Hash",
                value: "$2a$11$3NOnM49DfuTyXPwMjQG2WuFgacNtzaA9u/lV4mwqEFI2rdT8ocKLa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Hash",
                table: "Users",
                newName: "Password");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "abed123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "ibrahim123");
        }
    }
}
