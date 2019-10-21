using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.Infrastracture.Services;
using System.Threading.Tasks;

namespace News.WebAPI.Controllers
{
	/// <summary>
	/// Represents a controller of images of the news portal.
	/// </summary>
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Editor")]
	[Route("api/[controller]")]
	[ApiController]
	public class ImageController : ControllerBase
	{
		private readonly ImageService _imageService;

		/// <summary>
		/// Initializes the <see cref="ImageController"/>
		/// </summary>
		/// <param name="imageService">The <see cref="ImageService"/> of the news portal.</param>
		public ImageController(ImageService imageService) => _imageService = imageService;

		/// <summary>
		/// Adds an image to the news portal.
		/// </summary>
		/// <param name="formFile">The file that represents the image.</param>
		/// <returns>The name of the file that represents the image in the server.</returns>
		/// <response code="200">The image was successfully uploaded to the server.</response>
		[HttpPost]
		public async Task<string> Post(IFormFile formFile) => await _imageService.Add(formFile);
	}
}