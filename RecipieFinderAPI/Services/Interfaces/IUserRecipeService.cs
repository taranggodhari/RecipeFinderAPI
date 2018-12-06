using RecipeFinderAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeFinderAPI.Services.Interfaces
{
	public interface IUserRecipeService
	{
		UserRecipe GetUserRecipe(UserRecipe userRecipe);
		List<UserRecipe> GetUserRecipes(int id);
	}
}
