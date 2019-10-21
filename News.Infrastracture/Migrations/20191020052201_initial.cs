using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace News.Infrastracture.Migrations
{
	public partial class initial : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Stories",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
					Model_Title = table.Column<string>(nullable: false),
					Model_Summary = table.Column<string>(nullable: false),
					Model_Text = table.Column<string>(nullable: false),
					Model_PictureUrl = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Stories", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Users",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
					EmailConfirmed = table.Column<bool>(nullable: false),
					Model_Email = table.Column<string>(nullable: false),
					Model_Login = table.Column<string>(nullable: false),
					Model_PasswordHash = table.Column<string>(nullable: false),
					Model_Surname = table.Column<string>(nullable: false),
					Model_Name = table.Column<string>(nullable: false),
					Model_Patronymic = table.Column<string>(nullable: false),
					Model_BirthDate = table.Column<DateTime>(nullable: false),
					Model_Role = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Users", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "UserEmailConfirmations",
				columns: table => new
				{
					Id = table.Column<Guid>(nullable: false),
					ConfirmationUserID = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserEmailConfirmations", x => x.Id);
					table.ForeignKey(
						name: "FK_UserEmailConfirmations_Users_ConfirmationUserID",
						column: x => x.ConfirmationUserID,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_UserEmailConfirmations_ConfirmationUserID",
				table: "UserEmailConfirmations",
				column: "ConfirmationUserID",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Users_Model_Email",
				table: "Users",
				column: "Model_Email",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_Users_Model_Login",
				table: "Users",
				column: "Model_Login",
				unique: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Stories");

			migrationBuilder.DropTable(
				name: "UserEmailConfirmations");

			migrationBuilder.DropTable(
				name: "Users");
		}
	}
}
