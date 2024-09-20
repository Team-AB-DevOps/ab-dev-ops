using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class minmigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(name: "Username", table: "Users", newName: "username");

            migrationBuilder.RenameColumn(name: "Password", table: "Users", newName: "password");

            migrationBuilder.RenameColumn(name: "Email", table: "Users", newName: "email");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Username",
                table: "Users",
                newName: "IX_Users_username"
            );

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "Users",
                newName: "IX_Users_email"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(name: "username", table: "Users", newName: "Username");

            migrationBuilder.RenameColumn(name: "password", table: "Users", newName: "Password");

            migrationBuilder.RenameColumn(name: "email", table: "Users", newName: "Email");

            migrationBuilder.RenameIndex(
                name: "IX_Users_username",
                table: "Users",
                newName: "IX_Users_Username"
            );

            migrationBuilder.RenameIndex(
                name: "IX_Users_email",
                table: "Users",
                newName: "IX_Users_Email"
            );
        }
    }
}
