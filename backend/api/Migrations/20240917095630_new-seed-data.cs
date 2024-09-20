using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class newseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "pages",
                columns: new[] { "title", "content", "language", "last_updated", "url" },
                values: new object[,]
                {
                    {
                        "Java Title",
                        "Java",
                        "en",
                        null,
                        "https://en.wikipedia.org/wiki/Java_(programming_language)"
                    },
                    {
                        "Javascript Title",
                        "Javascript",
                        "en",
                        null,
                        "https://en.wikipedia.org/wiki/JavaScript"
                    }
                }
            );

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "email", "password", "username" },
                values: new object[] { 1, "test@test.com", "admin", "admin" }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "pages", keyColumn: "title", keyValue: "Java Title");

            migrationBuilder.DeleteData(
                table: "pages",
                keyColumn: "title",
                keyValue: "Javascript Title"
            );

            migrationBuilder.DeleteData(table: "users", keyColumn: "Id", keyValue: 1);
        }
    }
}
