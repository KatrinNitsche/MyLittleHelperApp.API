using Microsoft.EntityFrameworkCore.Migrations;

namespace MyHelpersApp.DAL.Migrations
{
    public partial class UpdatedToDoTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "ToDos",
                newName: "DueDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "ToDos",
                newName: "Date");
        }
    }
}
