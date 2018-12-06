using RecipeFinderAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeFinderAPI.Services.Interfaces
{
	public interface IRecipeService
	{
		Task<List<Recipe>> GetRecipesAsync(string searchitem);
		Recipe GetRecipeById(int id);
		List<Recipe> GetRecipeByRecipeId(int id);
		Task UpdateRecipe(Recipe recipe);
	}
}
