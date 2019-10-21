using News.Abstractions.Entities;
using News.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.Abstractions.Services
{
	/// <summary>
	/// Represents a story management service base of a news portal.
	/// </summary>
	/// <typeparam name="TID">The type of story identifiers.</typeparam>
	public interface IStoryService<TID>
	{
		/// <summary>
		/// Asynchronously adds a new story to the news portal.
		/// </summary>
		/// <param name="model">The model of the story.</param>
		/// <returns>A task that represents the add operation. The task result contains the story.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null"/>.</exception>
		Task<IEntity<TID, IStoryModel>> AddAsync(IStoryModel model);
		/// <summary>
		/// Asynchronously gets stories from the news portal.
		/// </summary>
		/// <returns>A task that represents the get operation. The task result contains the stories.</returns>
		Task<IEnumerable<IEntity<TID, IStoryModel>>> GetAsync();
		/// <summary>
		/// Asynchronously gets a story from the news portal.
		/// </summary>
		/// <param name="id">The identifier of the story.</param>
		/// <returns>A task that represents the get operation. The task result contains the story.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="id"/> is <see langword="null"/>.</exception>
		Task<IEntity<TID, IStoryModel>> GetAsync(TID id);
		/// <summary>
		/// Asynchronously changes a story in the news portal.
		/// </summary>
		/// <param name="id">The identifier of the story.</param>
		/// <param name="model">The new model of the story.</param>
		/// <returns>A task that represents the change operation. The task result contains value that indicates whether the story has been changed.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="id"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null"/>.</exception>
		Task<bool> ChangeAsync(TID id, IStoryModel model);
		/// <summary>
		/// Asynchronously removes a story from the news portal.
		/// </summary>
		/// <param name="id">The identifier of the story.</param>
		/// <returns>A task that represents the remove operation. The task result contains value that indicates whether the story has been removed.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="id"/> is <see langword="null"/>.</exception>
		Task<bool> RemoveAsync(TID id);
	}
}