using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeFinderAPI.Services.Interfaces;
using static RecipeFinderAPI.Model.EdamamModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipeFinderAPI.Controllers
{

	[Route("api/[controller]")]
	public class RecipeController : Controller
	{
		private readonly IRecipeService recipeService;
		public RecipeController(IRecipeService _recipeService)
		{
			recipeService = _recipeService;
		}

		// GET: api/<controller>
		[HttpGet()]
		[ProducesResponseType(typeof(IEnumerable<Recipe>), 200)]
		public async Task<IEnumerable<Recipe>> Get() => await recipeService.GetRecipesAsync("fresh picks");


		// GET api/recipe/chicken
		[HttpGet("{query}", Name = nameof(Get))]
		[ProducesResponseType(typeof(Recipe), 200)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> Get(string query)
		{
			List<Recipe> recipes = await recipeService.GetRecipesAsync(query);
			if (recipes == null)
			{
				return NotFound();
			}
			else
			{
				return new ObjectResult(recipes);
			}
		}

		// POST api/<controller>
		[HttpPost]
		public void Post([FromBody]string value)
		{
		}

		// PUT api/<controller>/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/<controller>/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
