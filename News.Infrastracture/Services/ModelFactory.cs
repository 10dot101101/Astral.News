using News.Abstractions.Models;
using News.Abstractions.Services;
using News.Infrastracture.Models;

namespace News.Infrastracture.Services
{
	/// <summary>
	/// Represents a service of creation of data models of a news portal.
	/// </summary>
	public class ModelFactory : IModelFactory
	{
		/// <summary>
		/// Creates an <see cref="IStoryModel"/>.
		/// </summary>
		/// <returns>An <see cref="IStoryModel"/>.</returns>
		public IStoryModel CreateStory() => new StoryModel();
		/// <summary>
		/// Creates an <see cref="IUserModel"/>.
		/// </summary>
		/// <returns>An <see cref="IUserModel"/>.</returns>
		public IUserModel CreateUser() => new UserModel();
	}
}