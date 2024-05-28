using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace course_marketplace.Migrations
{
    /// <inheritdoc />
    public partial class EditCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0afca1f6-9536-4d57-b297-e247b93e045a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a540973-8d26-4cb8-bb22-7ae16bbbba35");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6a41532e-b42f-4de8-a12a-1fdff3778472");

            migrationBuilder.CreateTable(
                name: "CourseContents",
                columns: table => new
                {
                    ContentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseContents", x => x.ContentId);
                    table.ForeignKey(
                        name: "FK_CourseContents_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1e0110a6-4b4d-404d-81be-f8d95f468cea", null, "client", "Клиент" },
                    { "567a9070-b7b6-4a78-98af-6f6408321c93", null, "admin", "Администратор" },
                    { "e1fb4b2f-5f10-4f54-884f-cdd0143246cd", null, "seller", "Продавец" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseContents_CourseId",
                table: "CourseContents",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseContents");

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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0afca1f6-9536-4d57-b297-e247b93e045a", null, "seller", "Продавец" },
                    { "1a540973-8d26-4cb8-bb22-7ae16bbbba35", null, "admin", "Администратор" },
                    { "6a41532e-b42f-4de8-a12a-1fdff3778472", null, "client", "Клиент" }
                });
        }
    }
}
