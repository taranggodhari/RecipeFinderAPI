using Microsoft.EntityFrameworkCore.Migrations;

namespace RecipeFinderAPI.Migrations
{
    public partial class AddedUsernameColumnToRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "shareAs",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "uri",
                table: "Recipes",
                newName: "username");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "username",
                table: "Recipes",
                newName: "uri");

            migrationBuilder.AddColumn<string>(
                name: "shareAs",
                table: "Recipes",
                nullable: true);
        }
    }
}
