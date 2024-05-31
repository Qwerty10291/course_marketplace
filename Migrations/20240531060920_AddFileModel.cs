using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace course_marketplace.Migrations
{
    /// <inheritdoc />
    public partial class AddFileModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e0110a6-4b4d-404d-81be-f8d95f468cea");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "567a9070-b7b6-4a78-98af-6f6408321c93");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e1fb4b2f-5f10-4f54-884f-cdd0143246cd");

            migrationBuilder.CreateTable(
                name: "FileModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    FilePath = table.Column<string>(type: "TEXT", nullable: false),
                    CourseContentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileModel_CourseContents_CourseContentId",
                        column: x => x.CourseContentId,
                        principalTable: "CourseContents",
                        principalColumn: "ContentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "358bf2bd-aa83-49b2-be6b-28f9bcd21bbf", null, "seller", "Продавец" },
                    { "ac678005-7b4c-42b8-85b2-68d86b68a478", null, "admin", "Администратор" },
                    { "c6290967-4500-430a-8db0-79ff3aa5861b", null, "client", "Клиент" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileModel_CourseContentId",
                table: "FileModel",
                column: "CourseContentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileModel");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "358bf2bd-aa83-49b2-be6b-28f9bcd21bbf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac678005-7b4c-42b8-85b2-68d86b68a478");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c6290967-4500-430a-8db0-79ff3aa5861b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1e0110a6-4b4d-404d-81be-f8d95f468cea", null, "client", "Клиент" },
                    { "567a9070-b7b6-4a78-98af-6f6408321c93", null, "admin", "Администратор" },
                    { "e1fb4b2f-5f10-4f54-884f-cdd0143246cd", null, "seller", "Продавец" }
                });
        }
    }
}
