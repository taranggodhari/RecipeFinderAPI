using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeFinderAPI.Model
{
	public class EdamamModel
	{
		public class Rootobject
		{
			public string q { get; set; }
			public int from { get; set; }
			public int to { get; set; }
			public Params _params { get; set; }
			public bool more { get; set; }
			public int count { get; set; }
			public Hit[] hits { get; set; }
		}

		public class Params
		{
			public object[] sane { get; set; }
			public string[] q { get; set; }
			public string[] app_key { get; set; }
			public string[] health { get; set; }
			public string[] from { get; set; }
			public string[] to { get; set; }
			public string[] calories { get; set; }
			public string[] app_id { get; set; }
		}

		public class Hit
		{
			public Recipe recipe { get; set; }
			public bool bookmarked { get; set; }
			public bool bought { get; set; }
		}

		public class Recipe
		{
			public string uri { get; set; }
			public string label { get; set; }
			public string image { get; set; }
			public string source { get; set; }
			public string url { get; set; }
			public string shareAs { get; set; }
			public float yield { get; set; }
			public string[] dietLabels { get; set; }
			public string[] healthLabels { get; set; }
			public object[] cautions { get; set; }
			public string[] ingredientLines { get; set; }
			public Ingredient[] ingredients { get; set; }
			public float calories { get; set; }
			public float totalWeight { get; set; }
		}


		public class Ingredient
		{
			public string text { get; set; }
			public float weight { get; set; }
		}
	}
}
