using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Http.API.Migrations
{
    /// <inheritdoc />
    public partial class AddOpenSourceProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Identity",
                table: "ThirdVideos",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LanguageVersion",
                table: "EntityModels",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "OpenSourceProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ProjectUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Thumbnail = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Authors = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Tags = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenSourceProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenSourceProducts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SystemLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ActionUserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    TargetName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Route = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ActionType = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SystemUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemLogs_SystemUsers_SystemUserId",
                        column: x => x.SystemUserId,
                        principalTable: "SystemUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpenSourceProducts_Title",
                table: "OpenSourceProducts",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_OpenSourceProducts_UserId",
                table: "OpenSourceProducts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SystemLogs_SystemUserId",
                table: "SystemLogs",
                column: "SystemUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpenSourceProducts");

            migrationBuilder.DropTable(
                name: "SystemLogs");

            migrationBuilder.AlterColumn<string>(
                name: "Identity",
                table: "ThirdVideos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "LanguageVersion",
                table: "EntityModels",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);
        }
    }
}
