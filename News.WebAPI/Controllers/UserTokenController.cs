using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using News.Abstractions.Entities;
using News.Abstractions.Models;
using News.Abstractions.Services;
using News.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace News.WebAPI.Controllers
{
	/// <summary>
	/// Represents a controller of user tokens.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class UserTokenController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly IUserService<int> _userService;

		/// <summary>
		/// Initializes the <see cref="UserTokenController"/>.
		/// </summary>
		/// <param name="configuration">An <see cref="IConfiguration"/> of the news portal.</param>
		/// <param name="userService">An <see cref="IUserService{TID}"/> of the news portal.</param>
		public UserTokenController(IConfiguration configuration, IUserService<int> userService)
		{
			_configuration = configuration;
			_userService = userService;
		}

		/// <summary>
		/// Authorizes a user by authorization data.
		/// </summary>
		/// <param name="data">The authorization data of the user.</param>
		/// <returns>The authorization token of the user.</returns>
		/// <response code="200">The user was successfully authorized.</response>
		/// <response code="401">The user was not authorized.</response>
		/// <response code="400">login or password is null.</response>
		[HttpPost]
		public async Task<ActionResult<string>> Post([FromBody] UserAuthDataModel data)
		{
			if (data.Login == null || data.Password == null)
				return BadRequest();
			IEntity<int, IUserModel> user = await _userService.GetByAuthorizationDataAsync(data.Login, data.Password);
			if (user == null )
				return Unauthorized();
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Model.Login),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Model.Role.ToString())
			};
			ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			SigningCredentials credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Key"])), SecurityAlgorithms.HmacSha512);
			DateTime now = DateTime.UtcNow;
			JwtSecurityToken token = new JwtSecurityToken(_configuration["JWT:Issuer"], _configuration["JWT:Audience"], claimsIdentity.Claims, now, now.AddMinutes(Convert.ToInt32(_configuration["JWT:Lifetime"])), credentials);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}