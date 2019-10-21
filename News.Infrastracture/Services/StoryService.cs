using News.Abstractions.Entities;
using News.Abstractions.Models;
using News.Abstractions.Repositories;
using News.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.Infrastracture.Services
{
	/// <summary>
	/// Represents a story management service base of a news portal.
	/// </summary>
	public class StoryService : IStoryService<int>
	{
		private readonly IStoryRepository<int> _repository;

		/// <summary>
		/// Initializes the <see cref="StoryService"/>.
		/// </summary>
		/// <param name="repository">The repository of the stories of the news portal.</param>
		/// <exception cref="ArgumentNullException"><paramref name="repository"/> is <see langword="null"/>.</exception>
		public StoryService(IStoryRepository<int> repository) => _repository = repository ?? throw new ArgumentNullException(nameof(repository));


		/// <summary>
		/// Asynchronously adds a new story to the news portal.
		/// </summary>
		/// <param name="model">The model of the story.</param>
		/// <returns>A task that represents the add operation. The task result contains the story.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null"/>.</exception>
		public async Task<IEntity<int, IStoryModel>> AddAsync(IStoryModel model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));
			IEntity<int, IStoryModel> story = await _repository.AddAsync(model);
			await _repository.Commit();
			return story;
		}
		/// <summary>
		/// Asynchronously gets stories from the news portal.
		/// </summary>
		/// <returns>A task that represents the get operation. The task result contains the stories.</returns>
		public Task<IEnumerable<IEntity<int, IStoryModel>>> GetAsync() => _repository.GetAsync();
		/// <summary>
		/// Asynchronously gets a story from the news portal.
		/// </summary>
		/// <param name="id">The identifier of the story.</param>
		/// <returns>A task that represents the get operation. The task result contains the story.</returns>
		public Task<IEntity<int, IStoryModel>> GetAsync(int id) => _repository.GetAsync(id);
		/// <summary>
		/// Asynchronously changes a story in the news portal.
		/// </summary>
		/// <param name="id">The identifier of the story.</param>
		/// <param name="model">The new model of the story.</param>
		/// <returns>A task that represents the change operation. The task result contains value that indicates whether the story has been changed.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null"/>.</exception>
		public async Task<bool> ChangeAsync(int id, IStoryModel model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));
			IEntity<int, IStoryModel> story = await _repository.GetAsync(id);
			if (story == null)
				return false;
			story.Model.Title = model.Title;
			story.Model.Summary = model.Summary;
			story.Model.Text = model.Text;
			story.Model.PictureUrl = model.PictureUrl;
			await _repository.UpdateAsync(story);
			await _repository.Commit();
			return true;
		}
		/// <summary>
		/// Asynchronously removes a story from the news portal.
		/// </summary>
		/// <param name="id">The identifier of the story.</param>
		/// <returns>A task that represents the remove operation. The task result contains value that indicates whether the story has been removed.</returns>
		public async Task<bool> RemoveAsync(int id)
		{
			if (!await _repository.RemoveAsync(id))
				return false;
			await _repository.Commit();
			return true;
		}
	}
}