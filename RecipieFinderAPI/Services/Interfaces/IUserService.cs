using RecipeFinderAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeFinderAPI.Services.Interfaces
{
	public interface IUserService
	{
		User GetUserByEmail(string email);
		Task<int> SaveAsync(User user);
	}
}
