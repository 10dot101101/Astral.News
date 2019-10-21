using Microsoft.AspNetCore.Mvc;
using News.Infrastracture.Services;
using System.Threading.Tasks;

namespace News.WebAPI.Controllers
{
	/// <summary>
	/// Represents a controller of user email confirmations.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class UserEmailConfirmationController : ControllerBase
	{
		private readonly UserEmailConfirmationService _userEmailConfirmationService;

		/// <summary>
		/// Initializes the <see cref="UserEmailConfirmationController"/>.
		/// </summary>
		/// <param name="userEmailConfirmationService">A <see cref="UserEmailConfirmationService"/> of the news portal.</param>
		public UserEmailConfirmationController(UserEmailConfirmationService userEmailConfirmationService) => _userEmailConfirmationService = userEmailConfirmationService;

		/// <summary>
		/// Confirms an email of a user by an identifier of the confirmation.
		/// </summary>
		/// <param name="id">The identifier of the configrmation.</param>
		/// <response code="200">The email was successfully confirmed.</response>
		/// <response code="404">The confirmation with the specified identifier was not found.</response>
		[HttpGet("{id}")]
		public async Task<ActionResult> Get([FromRoute] string id) => await _userEmailConfirmationService.ConfirmAsync(id) ? Ok() : (ActionResult)NotFound();
	}
}