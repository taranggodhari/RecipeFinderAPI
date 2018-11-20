using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipieFinderAPI.Models
{
	public class RecipieFinderContext : DbContext
	{
		public RecipieFinderContext(DbContextOptions<RecipieFinderContext> options) : base(options) { }

		//public DbSet<BookChapter> Chapters { get; set; }
	}
}
