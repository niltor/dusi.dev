using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Http.API.Migrations;

/// <inheritdoc />
public partial class UpdateAndTest : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Authors",
            table: "OpenSourceProducts",
            type: "character varying(60)",
            maxLength: 60,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(200)",
            oldMaxLength: 200,
            oldNullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "Authors",
            table: "OpenSourceProducts",
            type: "character varying(200)",
            maxLength: 200,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(60)",
            oldMaxLength: 60,
            oldNullable: true);
    }
}
