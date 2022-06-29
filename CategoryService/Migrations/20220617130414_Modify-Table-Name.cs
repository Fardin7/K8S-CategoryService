using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CategoryService.Migrations
{
    public partial class ModifyTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MyProperty",
                table: "MyProperty");

            migrationBuilder.RenameTable(
                name: "MyProperty",
                newName: "NewsCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewsCategory",
                table: "NewsCategory",
                column: "Id");

            migrationBuilder.InsertData(
                table: "NewsCategory",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "this category is about Sport", "Sport" });

            migrationBuilder.InsertData(
                table: "NewsCategory",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "this category is about Art", "Art" });

            migrationBuilder.InsertData(
                table: "NewsCategory",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "this category is about Technology", "Technology" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_NewsCategory",
                table: "NewsCategory");

            migrationBuilder.DeleteData(
                table: "NewsCategory",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NewsCategory",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "NewsCategory",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameTable(
                name: "NewsCategory",
                newName: "MyProperty");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyProperty",
                table: "MyProperty",
                column: "Id");
        }
    }
}
