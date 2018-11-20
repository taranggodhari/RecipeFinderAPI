using Newtonsoft.Json;
using RecipeFinderAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static RecipeFinderAPI.Model.EdamamModel;

namespace RecipeFinderAPI.Services
{
	public class RecipeService : IRecipeService
	{
		public async Task<List<Recipe>> GetRecipesAsync(string searchitem)
		{
			Rootobject RootObject = new Rootobject();
			var appkey = "531909d58814fa2f57ebd012084dc0af";
			var appid = "0abdef49";
			var fromto = "&from=0&to=5";

			using (var client = new HttpClient())
			{

				client.BaseAddress = new Uri("https://api.edamam.com/");
				client.DefaultRequestHeaders.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var uri = "search?q=" + searchitem + "&app_id=" + appid + "&app_key=" + appkey + fromto;
				HttpResponseMessage Res = await client.GetAsync(uri);


				if (Res.IsSuccessStatusCode)
				{


					var name = Res.Content.ReadAsStringAsync().Result;
					RootObject = JsonConvert.DeserializeObject<Rootobject>(name);

				}
				List<Recipe> recipes = new List<Recipe>();
				foreach (Hit hit in RootObject.hits)
				{
					recipes.Add(hit.recipe);
				}
				//returning the employee list to view  
				return recipes.ToList();
			}
		}
	}
}
