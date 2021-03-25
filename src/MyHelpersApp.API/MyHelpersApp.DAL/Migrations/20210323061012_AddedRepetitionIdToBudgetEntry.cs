using Microsoft.EntityFrameworkCore.Migrations;

namespace MyHelpersApp.DAL.Migrations
{
    public partial class AddedRepetitionIdToBudgetEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RepitionOfId",
                table: "BudgetEntries",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepitionOfId",
                table: "BudgetEntries");
        }
    }
}
