using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Http.API.Migrations
{
    /// <inheritdoc />
    public partial class AddNewsStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NewsStatus",
                table: "ThirdNews",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewsStatus",
                table: "ThirdNews");
        }
    }
}
