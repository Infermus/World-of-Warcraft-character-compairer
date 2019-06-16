using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WowCharComparerWebApp.Migrations
{
    public partial class Migration_v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VerificationToken",
                table: "Users",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerificationToken",
                table: "Users");
        }
    }
}
