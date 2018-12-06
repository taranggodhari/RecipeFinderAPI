using RecipeFinderAPI.Model;
using RecipeFinderAPI.Services.Interfaces;
using RecipieFinderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeFinderAPI.Services
{
	public class UserService : IUserService
	{
		private readonly RecipieFinderContext context;
		public UserService(RecipieFinderContext _context)
		{
			context = _context;
		}

		public User GetUserByEmail(string email)
		{
			return context.Users.SingleOrDefault(r => r.Email == email);
		}
		public async Task<int> SaveAsync(User user)
		{

			try
			{
				context.Users.Add(user);
				await context.SaveChangesAsync();
				return user.Id;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return 0;
			}

		}
	}
}
