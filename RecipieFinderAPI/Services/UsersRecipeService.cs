using Microsoft.EntityFrameworkCore;
using RecipeFinderAPI.Model;
using RecipeFinderAPI.Services.Interfaces;
using RecipieFinderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeFinderAPI.Services
{
	public class UsersRecipeService : IUserRecipeService
	{
		private readonly RecipieFinderContext context;
		public UsersRecipeService(RecipieFinderContext _context)
		{
			context = _context;
		}
		public UserRecipe GetUserRecipe(UserRecipe userRecipe)
		{
			return context.UserRecipes.SingleOrDefault(r => r.RecipeId == userRecipe.RecipeId && r.UserId == userRecipe.UserId);
		}
		public List<UserRecipe> GetUserRecipes(int id)
		{
			return context.UserRecipes.Include(r => r.Recipe).Include(r => r.Recipe.ingredients).Where(u => u.UserId == id).ToList();
		}
	}
}
