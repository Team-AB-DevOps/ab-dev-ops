using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class minmigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(name: "PK_Users", table: "Users");

            migrationBuilder.DropPrimaryKey(name: "PK_Pages", table: "Pages");

            migrationBuilder.RenameTable(name: "Users", newName: "users");

            migrationBuilder.RenameTable(name: "Pages", newName: "pages");

            migrationBuilder.RenameIndex(
                name: "IX_Users_username",
                table: "users",
                newName: "IX_users_username"
            );

            migrationBuilder.RenameIndex(
                name: "IX_Users_email",
                table: "users",
                newName: "IX_users_email"
            );

            migrationBuilder.AddPrimaryKey(name: "PK_users", table: "users", column: "Id");

            migrationBuilder.AddPrimaryKey(name: "PK_pages", table: "pages", column: "title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(name: "PK_users", table: "users");

            migrationBuilder.DropPrimaryKey(name: "PK_pages", table: "pages");

            migrationBuilder.RenameTable(name: "users", newName: "Users");

            migrationBuilder.RenameTable(name: "pages", newName: "Pages");

            migrationBuilder.RenameIndex(
                name: "IX_users_username",
                table: "Users",
                newName: "IX_Users_username"
            );

            migrationBuilder.RenameIndex(
                name: "IX_users_email",
                table: "Users",
                newName: "IX_Users_email"
            );

            migrationBuilder.AddPrimaryKey(name: "PK_Users", table: "Users", column: "Id");

            migrationBuilder.AddPrimaryKey(name: "PK_Pages", table: "Pages", column: "title");
        }
    }
}
