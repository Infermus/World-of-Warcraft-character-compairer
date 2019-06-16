using Microsoft.EntityFrameworkCore.Migrations;

namespace WowCharComparerWebApp.Migrations
{
    public partial class migration_v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Verified",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Verified",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VerificationToken",
                table: "Users");
        }
    }
}
