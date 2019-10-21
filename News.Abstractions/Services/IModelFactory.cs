using News.Abstractions.Models;

namespace News.Abstractions.Services
{
	/// <summary>
	/// Represents a service of creation of data models of a news portal.
	/// </summary>
	public interface IModelFactory
	{
		/// <summary>
		/// Creates an <see cref="IStoryModel"/>.
		/// </summary>
		/// <returns>An <see cref="IStoryModel"/>.</returns>
		IStoryModel CreateStory();
		/// <summary>
		/// Creates an <see cref="IUserModel"/>.
		/// </summary>
		/// <returns>An <see cref="IUserModel"/>.</returns>
		IUserModel CreateUser();
	}
}