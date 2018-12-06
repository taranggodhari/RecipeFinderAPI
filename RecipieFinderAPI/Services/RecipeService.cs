using Newtonsoft.Json;
using RecipeFinderAPI.Services.Interfaces;
using RecipieFinderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RecipeFinderAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace RecipeFinderAPI.Services
{
	public class RecipeService : IRecipeService
	{
		private readonly RecipieFinderContext context;
		public RecipeService(RecipieFinderContext _context)
		{
			context = _context;
		}

		public async Task<List<Recipe>> GetRecipesAsync(string searchitem)
		{
			string[] terms = searchitem.Split();

			var searchrecipe = (from p in context.Recipes.Include(i => i.ingredients)
								where (terms.Any(r => p.label.Contains(r))) ||
									  (terms.Any(r => p.image.Contains(r))) ||
									  (terms.Any(r => p.source.Contains(r))) ||
									   (terms.Any(r => p.url.Contains(r))) ||
									   (terms.Any(r => p.ingredients.Any(i => i.text.Contains(r)))) ||
									   (terms.Any(r => p.url.Contains(r)))
								select p).Distinct().ToList();
			if (searchrecipe.Count == 0)
			{
				EdamamModel.Rootobject RootObject = new EdamamModel.Rootobject();
				var appkey = "531909d58814fa2f57ebd012084dc0af";
				var appid = "0abdef49";
				var fromto = "&from=0&to=3";
				using (var client = new HttpClient())
				{

					client.BaseAddress = new Uri("https://api.edamam.com/");
					client.DefaultRequestHeaders.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
					//client.DefaultRequestHeaders.Add("app_id", "531909d58814fa2f57ebd012084dc0af");
					//client.DefaultRequestHeaders.Add("app_key", "0abdef49");


					var uri = "search?q=" + searchitem + "&app_id=" + appid + "&app_key=" + appkey + fromto;
					HttpResponseMessage Res = await client.GetAsync(uri);


					if (Res.IsSuccessStatusCode)
					{
						var name = Res.Content.ReadAsStringAsync().Result;
						RootObject = JsonConvert.DeserializeObject<EdamamModel.Rootobject>(name);

					}
					else
					{
						return null;
					}
					List<Recipe> recipes = new List<Recipe>();
					foreach (EdamamModel.Hit hit in RootObject.hits)
					{
						var newrecipe = hit.recipe;
						var recipeinDB = context.Recipes.FirstOrDefault(r => r.label == newrecipe.label);
						if (recipeinDB == null)
						{
							List<Ingredient> ingredients = new List<Ingredient>();
							foreach (var ing in hit.recipe.ingredients)
							{
								Ingredient newing = new Ingredient
								{
									text = ing.text,
									weight = ing.weight
								};
								ingredients.Add(newing);
							}

							Recipe recipe = new Recipe()
							{
								calories = newrecipe.calories,
								image = newrecipe.image,
								ingredients = ingredients,
								label = newrecipe.label,
								source = newrecipe.source,
								totalWeight = newrecipe.totalWeight,
								url = newrecipe.url,
								yield = newrecipe.yield

							};
							recipes.Add(recipe);
							context.Recipes.Add(recipe);
						}

					}

					await context.SaveChangesAsync();
					return recipes.ToList();
				}


			}
			else
			{
				return searchrecipe;
			}
		}
		public Recipe GetRecipeById(int id)
		{
			return context.Recipes.Include(i => i.ingredients).SingleOrDefault(r => r.Id == id);
		}
		public List<Recipe> GetRecipeByRecipeId(int id)
		{
			return context.Recipes.Include(i => i.ingredients).Where(r => r.Id == id).ToList();
		}


	}
}
