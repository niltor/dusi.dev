using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Http.API.Migrations;

/// <inheritdoc />
public partial class addThridNews : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "TranslateTitle",
            table: "Blogs",
            type: "character varying(200)",
            maxLength: 200,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

        migrationBuilder.CreateTable(
            name: "ThirdNews",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                AuthorName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                AuthorAvatar = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                Description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                Url = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                ThumbnailUrl = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                Provider = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                DatePublished = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                Content = table.Column<string>(type: "character varying(8000)", maxLength: 8000, nullable: true),
                Category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                IdentityId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                Type = table.Column<int>(type: "integer", nullable: false),
                NewsType = table.Column<int>(type: "integer", nullable: false),
                TechType = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ThirdNews", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "NewsTags",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                Name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                Color = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                ThirdNewsId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_NewsTags", x => x.Id);
                table.ForeignKey(
                    name: "FK_NewsTags_ThirdNews_ThirdNewsId",
                    column: x => x.ThirdNewsId,
                    principalTable: "ThirdNews",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_NewsTags_ThirdNewsId",
            table: "NewsTags",
            column: "ThirdNewsId");

        migrationBuilder.CreateIndex(
            name: "IX_ThirdNews_AuthorName",
            table: "ThirdNews",
            column: "AuthorName");

        migrationBuilder.CreateIndex(
            name: "IX_ThirdNews_Category",
            table: "ThirdNews",
            column: "Category");

        migrationBuilder.CreateIndex(
            name: "IX_ThirdNews_NewsType",
            table: "ThirdNews",
            column: "NewsType");

        migrationBuilder.CreateIndex(
            name: "IX_ThirdNews_Provider",
            table: "ThirdNews",
            column: "Provider");

        migrationBuilder.CreateIndex(
            name: "IX_ThirdNews_Title",
            table: "ThirdNews",
            column: "Title");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "NewsTags");

        migrationBuilder.DropTable(
            name: "ThirdNews");

        migrationBuilder.AlterColumn<string>(
            name: "TranslateTitle",
            table: "Blogs",
            type: "text",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(200)",
            oldMaxLength: 200,
            oldNullable: true);
    }
}
