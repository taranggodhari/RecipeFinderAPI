using Microsoft.EntityFrameworkCore;
using RecipeFinderAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipieFinderAPI.Models
{
	public class RecipieFinderContext : DbContext
	{
		public RecipieFinderContext(DbContextOptions<RecipieFinderContext> options) : base(options) { }
		public DbSet<Recipe> Recipes { get; set; }
		public DbSet<Ingredient> Ingredients { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<UserRecipe> UserRecipes { get; set; }
	}
}
