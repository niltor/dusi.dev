using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFramework.Migrator.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    NameValue = table.Column<string>(type: "text", nullable: false),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false),
                    Icon = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    RealName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordSalt = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false),
                    LastLoginTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    RetryCount = table.Column<int>(type: "integer", nullable: false),
                    Avatar = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    IdNumber = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: true),
                    Sex = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EntityLibraries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityLibraries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityLibraries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityModels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Comment = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    AccessModifier = table.Column<int>(type: "integer", nullable: false),
                    CodeExample = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    CodeLanguage = table.Column<int>(type: "integer", nullable: true),
                    ParentEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    EntityLibraryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityModels_EntityLibraries_EntityLibraryId",
                        column: x => x.EntityLibraryId,
                        principalTable: "EntityLibraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityModels_EntityModels_ParentEntityId",
                        column: x => x.ParentEntityId,
                        principalTable: "EntityModels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EntityMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Comment = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    DefaultValue = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AccessModifier = table.Column<int>(type: "integer", nullable: false),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    IsEnum = table.Column<bool>(type: "boolean", nullable: false),
                    IsList = table.Column<bool>(type: "boolean", nullable: false),
                    IsObject = table.Column<bool>(type: "boolean", nullable: false),
                    CanSet = table.Column<bool>(type: "boolean", nullable: false),
                    NeedInit = table.Column<bool>(type: "boolean", nullable: false),
                    DictionaryKeyType = table.Column<int>(type: "integer", nullable: true),
                    DictionaryValueType = table.Column<int>(type: "integer", nullable: true),
                    MemberType = table.Column<int>(type: "integer", nullable: false),
                    ObjectTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    EntityModelId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityMembers_EntityModels_EntityModelId",
                        column: x => x.EntityModelId,
                        principalTable: "EntityModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityMembers_EntityModels_ObjectTypeId",
                        column: x => x.ObjectTypeId,
                        principalTable: "EntityModels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EntityMemberConstraints",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MaxLength = table.Column<int>(type: "integer", nullable: true),
                    MinLength = table.Column<int>(type: "integer", nullable: true),
                    Length = table.Column<int>(type: "integer", nullable: true),
                    Min = table.Column<int>(type: "integer", nullable: true),
                    Max = table.Column<long>(type: "bigint", nullable: true),
                    EntityMemberId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityMemberConstraints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntityMemberConstraints_EntityMembers_EntityMemberId",
                        column: x => x.EntityMemberId,
                        principalTable: "EntityMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityLibraries_IsPublic",
                table: "EntityLibraries",
                column: "IsPublic");

            migrationBuilder.CreateIndex(
                name: "IX_EntityLibraries_Name",
                table: "EntityLibraries",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_EntityLibraries_UserId",
                table: "EntityLibraries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityMemberConstraints_EntityMemberId",
                table: "EntityMemberConstraints",
                column: "EntityMemberId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityMembers_AccessModifier",
                table: "EntityMembers",
                column: "AccessModifier");

            migrationBuilder.CreateIndex(
                name: "IX_EntityMembers_EntityModelId",
                table: "EntityMembers",
                column: "EntityModelId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityMembers_Name",
                table: "EntityMembers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_EntityMembers_ObjectTypeId",
                table: "EntityMembers",
                column: "ObjectTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityModels_AccessModifier",
                table: "EntityModels",
                column: "AccessModifier");

            migrationBuilder.CreateIndex(
                name: "IX_EntityModels_CodeLanguage",
                table: "EntityModels",
                column: "CodeLanguage");

            migrationBuilder.CreateIndex(
                name: "IX_EntityModels_EntityLibraryId",
                table: "EntityModels",
                column: "EntityLibraryId");

            migrationBuilder.CreateIndex(
                name: "IX_EntityModels_Name",
                table: "EntityModels",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_EntityModels_ParentEntityId",
                table: "EntityModels",
                column: "ParentEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedTime",
                table: "Users",
                column: "CreatedTime");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsDeleted",
                table: "Users",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityMemberConstraints");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "EntityMembers");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "EntityModels");

            migrationBuilder.DropTable(
                name: "EntityLibraries");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
