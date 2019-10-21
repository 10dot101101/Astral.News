using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using News.Abstractions.Entities;
using News.Abstractions.Models;
using News.Abstractions.Services;
using News.Abstractions.ValueTypes;
using News.WebAPI.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace News.WebAPI.Controllers
{
	/// <summary>
	/// Represents a controller of users of the news portal.
	/// </summary>
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IModelFactory _modelFactory;
		private readonly IUserService<int> _userService;

		/// <summary>
		/// Initializes the <see cref="UserController"/>.
		/// </summary>
		/// <param name="modelFactory">An <see cref="IModelFactory"/> of models of the news portal.</param>
		/// <param name="userService">An <see cref="IUserService{TID}"/> of the news portal.</param>
		public UserController(IModelFactory modelFactory, IUserService<int> userService)
		{
			_modelFactory = modelFactory;
			_userService = userService;
		}

		/// <summary>
		/// Gets the users of the news portal.
		/// </summary>
		/// <param name="sortKind">A value that specifies the kind of sorting of getting users.</param>
		/// <param name="offset">The number of the first of sorted users to get.</param>
		/// <param name="count">The number of users to get.</param>
		/// <param name="search">The search string.</param>
		/// <returns>The users of the news portal.</returns>
		/// <response code="200">The stories were successfully gotten.</response>
		/// <response code="400">sortKind is out of range of valid values or offset is less than 0 or count is less than 0.</response>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<UserEntityModel>>> Get([Required] [FromQuery] UserSortKind sortKind, [Required] [FromQuery] int offset, [Required] [FromQuery] int count, [FromQuery] string search)
		{
			if (sortKind < UserSortKind.Min || sortKind > UserSortKind.Max || offset < 0x0 || count < 0x0)
				return BadRequest();
			IEnumerable<IEntity<int, IUserModel>> users = await _userService.GetAsync(sortKind, offset, count, search);
			return new ActionResult<IEnumerable<UserEntityModel>>(users.Select(a => new UserEntityModel(a.Id)
			{
				Data = new UserDataModel
				{
					Email = a.Model.Email,
					Login = a.Model.Login,
					Surname = a.Model.Surname,
					Name = a.Model.Name,
					Patronymic = a.Model.Patronymic,
					BirthDate = a.Model.BirthDate,
					Role = a.Model.Role
				}
			}));
		}
		/// <summary>
		/// Adds a new user to the news portal.
		/// </summary>
		/// <param name="data">The data of the user.</param>
		/// <returns>The identifier of the user.</returns>
		/// <response code="200">The user was successfully added.</response>
		/// <response code="400">email of login is null or the user with the specified email or login already exists.</response>
		[HttpPost]
		public async Task<ActionResult<UserEntityModel>> Post([FromBody] UserDataModel data)
		{
			if (data.Email == null || data.Login == null)
				return BadRequest();
			IUserModel model = _modelFactory.CreateUser();
			model.Email = data.Email;
			model.Login = data.Login;
			model.Surname = data.Surname;
			model.Name = data.Name;
			model.Patronymic = data.Patronymic;
			model.BirthDate = data.BirthDate;
			model.Role = data.Role;
			IEntity<int, IUserModel> user = await _userService.AddAsync(model);
			return user != null ? new UserEntityModel(user.Id) : (ActionResult<UserEntityModel>)BadRequest();
		}
		/// <summary>
		/// Changes the data of a user.
		/// </summary>
		/// <param name="id">The identifier of the user.</param>
		/// <param name="data">The new data of the user.</param>
		/// <response code="200">The data were successfully changed.</response>
		/// <response code="404">The user was not found.</response>
		/// <response code="400">email of login is null or the user with the specified email or login already exists.</response>
		[HttpPut("{id}")]
		public async Task<ActionResult> Put([FromRoute] int id, [FromBody] UserDataModel data)
		{
			if (data.Email == null || data.Login == null)
				return BadRequest();
			IUserModel model = _modelFactory.CreateUser();
			model.Email = data.Email;
			model.Login = data.Login;
			model.Surname = data.Surname;
			model.Name = data.Name;
			model.Patronymic = data.Patronymic;
			model.BirthDate = data.BirthDate;
			model.Role = data.Role;
			bool? result = await _userService.ChangeAsync(id, model);
			return result == null ? NotFound() : result == false ? BadRequest() : (ActionResult)Ok();
		}
		/// <summary>
		/// Deletes a user from the news portal.
		/// </summary>
		/// <param name="id">The identifier of the user.</param>
		/// <response code="200">The user was successfully deleted.</response>
		[HttpDelete("{id}")]
		public Task Delete([FromRoute] int id) => _userService.RemoveAsync(id);
	}
}