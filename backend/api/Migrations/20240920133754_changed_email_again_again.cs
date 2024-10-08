﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
	/// <inheritdoc />
	public partial class changed_email_again_again : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder
				.AlterColumn<string>(name: "email", table: "users", type: "varchar(255)", nullable: true, oldClrType: typeof(string), oldType: "varchar(255)")
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.UpdateData(table: "users", keyColumn: "email", keyValue: null, column: "email", value: "");

			migrationBuilder
				.AlterColumn<string>(
					name: "email",
					table: "users",
					type: "varchar(255)",
					nullable: false,
					oldClrType: typeof(string),
					oldType: "varchar(255)",
					oldNullable: true
				)
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");
		}
	}
}
