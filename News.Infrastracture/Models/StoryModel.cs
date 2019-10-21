using News.Abstractions.Models;
using System;

namespace News.Infrastracture.Models
{
	/// <summary>
	/// Represents a model of a story to be read by clients of a news portal.
	/// </summary>
	public class StoryModel : IStoryModel
	{
		private string _title;
		private string _summary;
		private string _text;
		private string _pictureUrl;

		/// <summary>
		/// Gets or sets the title of the story.
		/// </summary>
		/// <exception cref="ArgumentNullException">The specified value is <see langword="null"/>.</exception>
		public string Title { get => _title; set => _title = value ?? throw new ArgumentNullException(nameof(value)); }
		/// <summary>
		/// Gets or sets the summary of the story.
		/// </summary>
		/// <exception cref="ArgumentNullException">The specified value is <see langword="null"/>.</exception>
		public string Summary { get => _summary; set => _summary = value ?? throw new ArgumentNullException(nameof(value)); }
		/// <summary>
		/// Gets or sets the text of the story.
		/// </summary>
		/// <exception cref="ArgumentNullException">The specified value is <see langword="null"/>.</exception>
		public string Text { get => _text; set => _text = value ?? throw new ArgumentNullException(nameof(value)); }
		/// <summary>
		/// Gets or sets the URL of the picture describing the story.
		/// </summary>
		/// <exception cref="ArgumentNullException">The specified value is <see langword="null"/>.</exception>
		public string PictureUrl { get => _pictureUrl; set => _pictureUrl = value ?? throw new ArgumentNullException(nameof(value)); }
	}
}