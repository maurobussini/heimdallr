using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ZenProgramming.Heimdallr.EntityFramework.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "icHEIMDALLR_Audiences",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 255, nullable: false),
                    AllowedOrigin = table.Column<string>(maxLength: 255, nullable: false),
                    ClientId = table.Column<string>(maxLength: 255, nullable: false),
                    ClientSecret = table.Column<string>(maxLength: 255, nullable: false),
                    HasAdministrativeAccess = table.Column<bool>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    IsNative = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    RefreshTokenLifeTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_icHEIMDALLR_Audiences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "icHEIMDALLR_RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 255, nullable: false),
                    ClientId = table.Column<string>(maxLength: 255, nullable: false),
                    ExpiresUtc = table.Column<DateTime>(nullable: false),
                    IssuedUtc = table.Column<DateTime>(nullable: false),
                    ProtectedTicket = table.Column<string>(maxLength: 2000, nullable: false),
                    TokenHash = table.Column<string>(maxLength: 255, nullable: false),
                    UserName = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_icHEIMDALLR_RefreshTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "icHEIMDALLR_Users",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 255, nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false),
                    IsLocked = table.Column<bool>(nullable: false),
                    LastAccessDate = table.Column<DateTime>(nullable: true),
                    PasswordHash = table.Column<string>(maxLength: 1024, nullable: true),
                    PersonName = table.Column<string>(maxLength: 255, nullable: true),
                    PersonSurname = table.Column<string>(maxLength: 255, nullable: true),
                    PhotoBinary = table.Column<byte[]>(nullable: true),
                    UserName = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_icHEIMDALLR_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "icHEIMDALLR_Audiences");

            migrationBuilder.DropTable(
                name: "icHEIMDALLR_RefreshTokens");

            migrationBuilder.DropTable(
                name: "icHEIMDALLR_Users");
        }
    }
}
