using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using News.Abstractions.Repositories;
using News.Abstractions.Services;
using News.Infrastracture.DataContexts;
using News.Infrastracture.Repositories;
using News.Infrastracture.Services;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace News.WebAPI
{
	/// <summary>
	/// Represents a configurator of the news portal server.
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Initializes the <see cref="Startup"/>.
		/// </summary>
		/// <param name="configuration">The configuration of the server.</param>
		public Startup(IConfiguration configuration) => Configuration = configuration;

		/// <summary>
		/// Gets the configuration of the server.
		/// </summary>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// Configures the services of the server.
		/// </summary>
		/// <param name="services">The services.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			_ = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = Configuration["JWT:Issuer"],
					ValidAudience = Configuration["JWT:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWT:Key"])),
				};
			});
			_ = services.AddDbContext<DataContext>(options => options.UseNpgsql(Configuration.GetConnectionString("Main")));
			_ = services.AddScoped<IStoryRepository<int>, StoryRepository>();
			_ = services.AddScoped<IUserRepository<int>, UserRepository>();
			_ = services.AddScoped<IModelFactory, ModelFactory>();
			_ = services.AddScoped<IStoryService<int>, StoryService>();
			_ = services.AddScoped<IUserService<int>, UserService>();
			_ = services.AddScoped(provider => new EmailService(Configuration["EmailService:Name"], Configuration["EmailService:EmailAddress"], Configuration["EmailService:Password"], Configuration["EmailService:Address"], Convert.ToInt32(Configuration["EmailService:Port"]), Convert.ToBoolean(Configuration["EmailService:UseSsl"])));
			_ = services.AddScoped(provider => new UserEmailConfirmationService(provider.GetService<DataContext>(), provider.GetService<IUserRepository<int>>(), provider.GetService<EmailService>(), "https://localhost:44318/api/useremailconfirmation/"));
			_ = services.AddSingleton(provider => new PasswordService(Convert.ToInt32(Configuration["PasswordService:Length"])));
			_ = services.AddSingleton(provider => new ImageService(Configuration["Image:Directory"]));
			_ = services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options =>
			{
				options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
				options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
			}); ;
			_ = services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info { Title = "News Portal API", Version = "v1" });
				c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
			});
		}
		/// <summary>
		/// Configures the pipeline of HTTP requests handling.
		/// </summary>
		/// <param name="app">The configurator of the application.</param>
		/// <param name="env">The environment of the application.</param>
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			_ = env.IsDevelopment() ? app.UseDeveloperExceptionPage() : app.UseHsts();
			_ = app.UseHttpsRedirection();
			_ = app.UseStaticFiles();
			_ = app.UseAuthentication();
			_ = app.UseSwagger();
			_ = app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
			_ = app.UseMvc();
		}
	}
}