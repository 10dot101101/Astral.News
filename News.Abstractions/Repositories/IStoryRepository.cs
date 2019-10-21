using News.Abstractions.Models;

namespace News.Abstractions.Repositories
{
	/// <summary>
	/// Represents a repository of stories of a news portal.
	/// </summary>
	/// <typeparam name="TID">The type of story identifiers.</typeparam>
	public interface IStoryRepository<TID> : IEntityRepository<TID, IStoryModel> { }
}