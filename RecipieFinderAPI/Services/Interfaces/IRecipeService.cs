using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static RecipeFinderAPI.Model.EdamamModel;

namespace RecipeFinderAPI.Services.Interfaces
{
	public interface IRecipeService
	{
		Task<List<Recipe>> GetRecipesAsync(string searchitem);
	}
}
