using System;

namespace News.Abstractions.Models
{
	/// <summary>
	/// Represents a model of a story to be read by clients of a news portal.
	/// </summary>
	public interface IStoryModel
	{
		/// <summary>
		/// Gets or sets the title of the story.
		/// </summary>
		/// <exception cref="ArgumentNullException">The specified value is <see langword="null"/>.</exception>
		string Title { get; set; }
		/// <summary>
		/// Gets or sets the summary of the story.
		/// </summary>
		/// <exception cref="ArgumentNullException">The specified value is <see langword="null"/>.</exception>
		string Summary { get; set; }
		/// <summary>
		/// Gets or sets the text of the story.
		/// </summary>
		/// <exception cref="ArgumentNullException">The specified value is <see langword="null"/>.</exception>
		string Text { get; set; }
		/// <summary>
		/// Gets or sets the URL of the picture describing the story.
		/// </summary>
		/// <exception cref="ArgumentNullException">The specified value is <see langword="null"/>.</exception>
		string PictureUrl { get; set; }
	}
}