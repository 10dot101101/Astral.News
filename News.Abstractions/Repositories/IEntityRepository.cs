using News.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.Abstractions.Repositories
{
	/// <summary>
	/// Represents a repository of entities of a news portal.
	/// </summary>
	/// <typeparam name="TID">The type of entity identifiers.</typeparam>
	/// <typeparam name="TModel">The type of the model of the entities.</typeparam>
	public interface IEntityRepository<TID, TModel>
	{
		/// <summary>
		/// Commits the made changes in the repository.
		/// </summary>
		/// <returns>A task that represents the commit operation.</returns>
		Task Commit();
		/// <summary>
		/// Asynchronously adds a entity to the repository.
		/// </summary>
		/// <param name="model">The model of the entity.</param>
		/// <returns>A task that represents the add operation. The task result contains the entity.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null"/>.</exception>
		Task<IEntity<TID, TModel>> AddAsync(TModel model);
		/// <summary>
		/// Asynchronously gets entities from the repository.
		/// </summary>
		/// <returns>A task that represents the get operation. The task result contains the entities.</returns>
		Task<IEnumerable<IEntity<TID, TModel>>> GetAsync();
		/// <summary>
		/// Asynchronously gets an entity from the repository.
		/// </summary>
		/// <param name="id">The identifier of the entity.</param>
		/// <returns>A task that represents the get operation. The task result contains the entity.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="id"/> is <see langword="null"/>.</exception>
		Task<IEntity<TID, TModel>> GetAsync(TID id);
		/// <summary>
		/// Asynchronously updates the model of an entity in the repository.
		/// </summary>
		/// <param name="entity">The entity.</param>
		/// <returns>A task that represents the update operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="entity"/> is <see langword="null"/>.</exception>
		Task UpdateAsync(IEntity<TID, TModel> entity);
		/// <summary>
		/// Asynchronously removes an entity from the repository.
		/// </summary>
		/// <param name="id">The identifier of the entity.</param>
		/// <returns>A task that represents the remove operation. The task result contains value that indicates whether the entity has been removed.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="id"/> is <see langword="null"/>.</exception>
		Task<bool> RemoveAsync(TID id);
	}
}