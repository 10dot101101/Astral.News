namespace News.WebAPI.Models
{
	/// <summary>
	/// Represents a model of a story of the news portal.
	/// </summary>
	public class StoryEntityModel
	{
		/// <summary>
		/// Initailizes the <see cref="StoryEntityModel"/>.
		/// </summary>
		/// <param name="id">The identitifier of the story.</param>
		public StoryEntityModel(int id) => Id = id;

		/// <summary>
		/// Gets the identifier of the story.
		/// </summary>
		public int Id { get; }
		/// <summary>
		/// Gets the data of the story.
		/// </summary>
		public StoryDataModel Data { get; set; }
	}
}