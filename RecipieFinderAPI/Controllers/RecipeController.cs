using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecipeFinderAPI.Model;
using RecipeFinderAPI.Services.Interfaces;
using RecipieFinderAPI.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipeFinderAPI.Controllers
{

	[Route("api/[controller]")]
	public class RecipeController : Controller
	{
		private readonly RecipieFinderContext context;
		private readonly IRecipeService recipeService;
		private readonly IUserService userService;
		private readonly IUserRecipeService userRecipeService;
		public RecipeController(IRecipeService _recipeService, IUserService _userService, IUserRecipeService _userRecipeService, RecipieFinderContext _context)
		{
			recipeService = _recipeService;
			userService = _userService;
			userRecipeService = _userRecipeService;
			context = _context;
		}

		// GET: api/<controller>
		[HttpGet()]
		[ProducesResponseType(typeof(IEnumerable<Recipe>), 200)]
		[ProducesResponseType(404)]
		public async Task<IEnumerable<Recipe>> Get() => await recipeService.GetRecipesAsync("fresh picks");


		// GET api/recipe/chicken
		[HttpGet("query/{query}", Name = nameof(Get))]
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
				return Ok(recipes);
			}
		}
		// GET api/recipe/id/id
		[HttpGet("id/{id}", Name = nameof(GetById))]
		[ProducesResponseType(typeof(Recipe), 200)]
		[ProducesResponseType(404)]
		public IActionResult GetById(int id)
		{
			Recipe recipe = recipeService.GetRecipeById(id);
			if (recipe == null)
			{
				return NotFound();
			}
			else
			{
				return Ok(recipe);
			}
		}
		// GET api/recipe/id/id
		[HttpGet("userrecipe/{email}", Name = nameof(GetUserRecipe))]
		[ProducesResponseType(typeof(Recipe), 200)]
		[ProducesResponseType(404)]
		public IActionResult GetUserRecipe(string email)
		{
			var user = userService.GetUserByEmail(email);
			List<Recipe> recipes = new List<Recipe>();
			if (user != null)
			{
				List<UserRecipe> userRecipes = userRecipeService.GetUserRecipes(user.Id);

				foreach (var userrecipe in userRecipes)
				{
					recipes.Add(userrecipe.Recipe);
				}
			}
			if (recipes == null)
			{
				return NotFound();
			}
			else
			{
				return Ok(recipes);
			}

		}
		// POST api/<controller>
		[HttpPost]
		[ProducesResponseType(400)]
		public async Task Post([FromBody]PostRecipe postRecipe)
		{
			var recipeid = postRecipe.RecipeId;
			var email = postRecipe.Email;
			try
			{
				if (recipeid != null && email != null)
				{
					var olduser = userService.GetUserByEmail(email);
					var userId = 0;
					if (olduser == null)
					{
						User user = new User
						{
							Email = email
						};
						userId = await userService.SaveAsync(user);
						if (userId == 0)
						{
							Console.WriteLine("Error Saving User");
						}
					}
					else
					{
						userId = olduser.Id;
					}

					var recipe = recipeService.GetRecipeById(Convert.ToInt32(recipeid));
					UserRecipe userRecipe = new UserRecipe()
					{
						RecipeId = recipe.Id,
						UserId = userId
					};
					if (userRecipeService.GetUserRecipe(userRecipe) == null)
					{
						context.Add(userRecipe);
						await context.SaveChangesAsync();
					}
				}
			}
			catch (Exception e)
			{

				Console.WriteLine(e.StackTrace, e.Message);
			}
		}

		// PUT api/<controller>/5
		[HttpPut("{id}/{email}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task Put(int id, string email, [FromBody]Recipe recipe)
		{
			var recipeid = recipe.Id.ToString();
			try
			{
				if (recipeid != null && email != null)
				{
					await recipeService.UpdateRecipe(recipe);
				}
			}
			catch (Exception e)
			{

				Console.WriteLine(e.StackTrace, e.Message);
			}
		}

		// DELETE api/<controller>/5/email
		[HttpDelete("{id}/{email}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task Delete(int id, string email)
		{
			try
			{
				if (id != 0 && email != null)
				{
					var user = userService.GetUserByEmail(email);
					var recipe = recipeService.GetRecipeById(id);
					if (user != null && recipe != null)
					{
						UserRecipe ur = new UserRecipe()
						{
							RecipeId = recipe.Id,
							UserId = user.Id
						};
						var userRecipe = userRecipeService.GetUserRecipe(ur);
						if (userRecipe != null)
						{
							context.Remove(userRecipe);
							await context.SaveChangesAsync();
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
	}
}
