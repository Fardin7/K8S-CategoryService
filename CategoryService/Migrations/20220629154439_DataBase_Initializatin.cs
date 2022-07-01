using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CategoryService.Migrations
{
    public partial class DataBase_Initializatin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewsCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsCategory", x => x.Id);
                });

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
            migrationBuilder.DropTable(
                name: "NewsCategory");
        }
    }
}
