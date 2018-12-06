using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeFinderAPI.Migrations
{
    public partial class RemovedUsernameColumnFromRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "username",
                table: "Recipes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "Recipes",
                nullable: true);
        }
    }
}
