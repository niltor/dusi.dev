using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Http.API.Migrations
{
    /// <inheritdoc />
    public partial class AddBlogViewCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Blogs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Blogs");
        }
    }
}
