using Microsoft.EntityFrameworkCore;
using News.Abstractions.Entities;
using News.Abstractions.Models;
using News.Abstractions.Repositories;
using News.Infrastracture.DataContexts;
using News.Infrastracture.Entities;
using News.Infrastracture.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.Infrastracture.Repositories
{
	/// <summary>
	/// Represents a repository of stories of a news portal.
	/// </summary>
	public class StoryRepository : IStoryRepository<int>
	{
		private readonly DataContext _dataContext;

		/// <summary>
		/// Initializes the <see cref="UserRepository"/>.
		/// </summary>
		/// <param name="dataContext">The <see cref="DataContext"/> of data of the news portal.</param>
		/// <exception cref="ArgumentNullException"><paramref name="dataContext"/> is <see langword="null"/>.</exception>
		public StoryRepository(DataContext dataContext) => _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));

		/// <summary>
		/// Commits the made changes in the repository.
		/// </summary>
		/// <returns>A task that represents the commit operation.</returns>
		public Task Commit() => _dataContext.SaveChangesAsync();
		/// <summary>
		/// Asynchronously adds a user to the repository.
		/// </summary>
		/// <param name="model">The model of the user.</param>
		/// <returns>A task that represents the add operation. The task result contains the user.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null"/>.</exception>
		public async Task<IEntity<int, IStoryModel>> AddAsync(IStoryModel model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));
			StoryModel storyModel = new StoryModel
			{
				Title = model.Title,
				Summary = model.Summary,
				Text = model.Text,
				PictureUrl = model.PictureUrl
			};
			Story user = new Story(storyModel);
			_ = await _dataContext.AddAsync(user);
			return user;
		}
		/// <summary>
		/// Asynchronously gets users from the repository.
		/// </summary>
		/// <returns>A task that represents the get operation. The task result contains the users.</returns>
		public async Task<IEnumerable<IEntity<int, IStoryModel>>> GetAsync() => await _dataContext.Stories.AsNoTracking().Include(nameof(Story.Model)).ToArrayAsync();
		/// <summary>
		/// Asynchronously gets an user from the repository.
		/// </summary>
		/// <param name="id">The identifier of the user.</param>
		/// <returns>A task that represents the get operation. The task result contains the user.</returns>
		public async Task<IEntity<int, IStoryModel>> GetAsync(int id) => await _dataContext.Stories.AsNoTracking().Include(nameof(Story.Model)).FirstOrDefaultAsync(a => a.Id == id);
		/// <summary>
		/// Asynchronously updates the model of an user in the repository.
		/// </summary>
		/// <param name="story">The user.</param>
		/// <returns>A task that represents the update operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="story"/> is <see langword="null"/>.</exception>
		public Task UpdateAsync(IEntity<int, IStoryModel> story)
		{
			if (story == null)
				throw new ArgumentNullException(nameof(story));
			_dataContext.Entry(story).State = EntityState.Modified;
			_dataContext.Entry(story.Model).State = EntityState.Modified;
			return Task.CompletedTask;
		}
		/// <summary>
		/// Asynchronously removes an user from the repository.
		/// </summary>
		/// <param name="id">The identifier of the user.</param>
		/// <returns>A task that represents the remove operation. The task result contains value that indicates whether the story has been removed.</returns>
		public async Task<bool> RemoveAsync(int id)
		{
			IEntity<int, IStoryModel> story = await GetAsync(id);
			if (story == null)
				return false;
			_ = _dataContext.Stories.Remove((Story)story);
			return true;
		}
	}
}