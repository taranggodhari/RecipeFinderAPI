﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeFinderAPI.Model
{
	public class User
	{
		[Key]
		public int Id { get; set; }
		public string Email { get; set; }
		public virtual ICollection<UserRecipe> UserRecipes { get; set; }
	}
}
