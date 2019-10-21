namespace News.WebAPI.Models
{
	/// <summary>
	/// Represents a model of data of a story of the news portal.
	/// </summary>
	public class StoryDataModel
	{
		/// <summary>
		/// Gets or sets the title of the story.
		/// </summary>
		public string Title { get; set; }
		/// <summary>
		/// Gets or sets the summary of the story.
		/// </summary>
		public string Summary { get; set; }
		/// <summary>
		/// Gets or sets the text of the story.
		/// </summary>
		public string Text { get; set; }
		/// <summary>
		/// Gets or sets the URL of the picture describing the story.
		/// </summary>
		public string PictureUrl { get; set; }
	}
}