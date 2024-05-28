using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace course_marketplace.Migrations
{
    /// <inheritdoc />
    public partial class CourseChangeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8d2f50a2-ca8b-4fb0-86e1-493aaec9e87a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "901a53e9-6ce8-4228-8fea-111a563c5b4e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97699539-a531-437c-a8c8-4e36e4a18cc9");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "Courses",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Courses",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8d2f50a2-ca8b-4fb0-86e1-493aaec9e87a", null, "seller", "Продавец" },
                    { "901a53e9-6ce8-4228-8fea-111a563c5b4e", null, "client", "Клиент" },
                    { "97699539-a531-437c-a8c8-4e36e4a18cc9", null, "admin", "Администратор" }
                });
        }
    }
}
