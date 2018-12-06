using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeFinderAPI.Model
{
	public class UserRecipe
	{
		[Key]
		public int Id { get; set; }
		public int UserId { get; set; }
		public int RecipeId { get; set; }

		[ForeignKey("UserId")]
		public virtual User User { get; set; }
		[ForeignKey("RecipeId")]
		public virtual Recipe Recipe { get; set; }
	}
}
