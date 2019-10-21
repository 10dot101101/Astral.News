using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using News.Abstractions.Entities;
using News.Abstractions.Models;
using News.Abstractions.Services;
using News.WebAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.WebAPI.Controllers
{
	/// <summary>
	/// Represents a controller of stories of the news portal.
	/// </summary>
	[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Editor")]
	[Route("api/[controller]")]
	[ApiController]
	public class StoryController : ControllerBase
	{
		private readonly IModelFactory _modelFactory;
		private readonly IStoryService<int> _storyService;

		/// <summary>
		/// Initializes the <see cref="StoryController"/>.
		/// </summary>
		/// <param name="modelFactory">An <see cref="IModelFactory"/> of the models of the news portal.</param>
		/// <param name="storyService">An <see cref="IStoryService{TID}"/> of the news portal.</param>
		public StoryController(IModelFactory modelFactory, IStoryService<int> storyService)
		{
			_modelFactory = modelFactory;
			_storyService = storyService;
		}

		/// <summary>
		/// Gets the stories of the news portal without the text.
		/// </summary>
		/// <returns>The stories of the news portal without the text.</returns>
		/// <response code="200">The stories were successfully gotten.</response>
		[AllowAnonymous]
		[HttpGet]
		public async Task<IEnumerable<StoryEntityModel>> Get() => (await _storyService.GetAsync()).Select(a => new StoryEntityModel(a.Id) { Data = new StoryDataModel { Title = a.Model.Title, Summary = a.Model.Summary, PictureUrl = a.Model.PictureUrl } });
		/// <summary>
		/// Gets a story.
		/// </summary>
		/// <param name="id">The identifier of the story.</param>
		/// <returns>The story.</returns>
		/// <response code="200">The story was successfully gotten.</response>
		/// <response code="404">The story was not found.</response>
		[AllowAnonymous]
		[HttpGet("{id}")]
		public async Task<ActionResult<StoryEntityModel>> Get(int id)
		{
			IEntity<int, IStoryModel> story = await _storyService.GetAsync(id);
			return story != null ? new StoryEntityModel(id) { Data = new StoryDataModel { Title = story.Model.Title, Summary = story.Model.Summary, Text = story.Model.Text, PictureUrl = story.Model.PictureUrl } } : (ActionResult<StoryEntityModel>)NotFound();
		}
		/// <summary>
		/// Adds a new story to the news portal.
		/// </summary>
		/// <param name="data">The data of the story.</param>
		/// <returns>The identifier of the story.</returns>
		/// <response code="200">The story was successfully added.</response>
		/// <response code="400">title or summary or text or pictureUrl is null.</response>
		[HttpPost]
		public async Task<ActionResult<StoryEntityModel>> Post([FromBody] StoryDataModel data)
		{
			if (data.Title == null || data.Summary == null || data.Text == null || data.PictureUrl == null)
				return BadRequest();
			IStoryModel model = _modelFactory.CreateStory();
			model.Title = data.Title;
			model.Summary = data.Summary;
			model.Text = data.Text;
			model.PictureUrl = data.PictureUrl;
			return new StoryEntityModel((await _storyService.AddAsync(model)).Id);
		}
		/// <summary>
		/// Changes the data of a story.
		/// </summary>
		/// <param name="id">The identifier of the story.</param>
		/// <param name="data">The new data of the story.</param>
		/// <response code="200">The data were successfully changed.</response>
		/// <response code="404">The story was not found.</response>
		/// <response code="400">title or summary or text or pictureUrl is null.</response>
		[HttpPut("{id}")]
		public async Task<ActionResult> Put([FromRoute] int id, [FromBody] StoryDataModel data)
		{
			if (data.Title == null || data.Summary == null || data.Text == null || data.PictureUrl == null)
				return BadRequest();
			IStoryModel model = _modelFactory.CreateStory();
			model.Title = data.Title;
			model.Summary = data.Summary;
			model.Text = data.Text;
			model.PictureUrl = data.PictureUrl;
			return await _storyService.ChangeAsync(id, model) ? Ok() : (ActionResult)NotFound();
		}
		/// <summary>
		/// Deletes a story from the news portal.
		/// </summary>
		/// <param name="id">The identifier of the story.</param>
		/// <response code="200">The story was successfully deleted.</response>
		[HttpDelete("{id}")]
		public Task Delete([FromRoute] int id) => _storyService.RemoveAsync(id);
	}
}