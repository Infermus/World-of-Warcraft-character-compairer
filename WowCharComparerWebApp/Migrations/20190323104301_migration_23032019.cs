using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WowCharComparerWebApp.Migrations
{
    public partial class migration_23032019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AchievementCategory",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementCategory", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "APIClient",
                columns: table => new
                {
                    ClientID = table.Column<string>(nullable: false),
                    ClientSecret = table.Column<string>(nullable: false),
                    ClientName = table.Column<string>(nullable: false),
                    ValidationUntil = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIClient", x => x.ClientID);
                });

            migrationBuilder.CreateTable(
                name: "BonusStats",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    BonusStatsID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusStats", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Nickname = table.Column<string>(maxLength: 30, nullable: false),
                    Password = table.Column<string>(maxLength: 30, nullable: false),
                    RegistrationDate = table.Column<DateTime>(nullable: false),
                    LastLoginDate = table.Column<DateTime>(nullable: false),
                    IsOnline = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AchievementsData",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Points = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    FactionId = table.Column<int>(nullable: false),
                    AchievementCategoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievementsData", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AchievementsData_AchievementCategory_AchievementCategoryID",
                        column: x => x.AchievementCategoryID,
                        principalTable: "AchievementCategory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AchievementCategory_ID",
                table: "AchievementCategory",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AchievementsData_AchievementCategoryID",
                table: "AchievementsData",
                column: "AchievementCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_AchievementsData_ID",
                table: "AchievementsData",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BonusStats_ID",
                table: "BonusStats",
                column: "ID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AchievementsData");

            migrationBuilder.DropTable(
                name: "APIClient");

            migrationBuilder.DropTable(
                name: "BonusStats");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AchievementCategory");
        }
    }
}
