using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RecipeFinderAPI.Services;
using RecipeFinderAPI.Services.Interfaces;
using RecipieFinderAPI.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace RecipieFinderAPI
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<RecipieFinderContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
				.AddJsonOptions(options =>
				{
					options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
				});
			//services.AddDbContext<TodoContext>(opt => opt.UseInMemoryDatabase("TodoList"));
			//Singleton lifetime services are created the first time they're requested 
			services.AddTransient<IRecipeService, RecipeService>();
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IUserRecipeService, UsersRecipeService>();
			//register the swagger generator
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "RecipieFinderAPI", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "RecipieFinderAPI");
			});

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
