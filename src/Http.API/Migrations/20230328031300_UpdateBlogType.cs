using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Http.API.Migrations;

/// <inheritdoc />
public partial class UpdateBlogType : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<int>(
            name: "BlogType",
            table: "Blogs",
            type: "integer",
            nullable: false,
            defaultValue: 0);

        migrationBuilder.AddColumn<bool>(
            name: "IsAudit",
            table: "Blogs",
            type: "boolean",
            nullable: false,
            defaultValue: false);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "BlogType",
            table: "Blogs");

        migrationBuilder.DropColumn(
            name: "IsAudit",
            table: "Blogs");
    }
}
