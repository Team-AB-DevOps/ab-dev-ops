using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class sqlmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlStatements = File.ReadAllText("./Sql/data.sql");
            if (sqlStatements == "" || sqlStatements == null) throw new Exception("SQL statements not found");

            migrationBuilder.Sql(sqlStatements);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
