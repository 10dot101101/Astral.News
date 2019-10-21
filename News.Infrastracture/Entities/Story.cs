using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Abstractions.Entities;
using News.Infrastracture.Models;
using System;

namespace News.Infrastracture.Entities
{
	/// <summary>
	/// Represents a story to be read by clients of a news portal.
	/// </summary>
	public class Story : IEntity<int, StoryModel>
	{
		internal sealed class Configuration : IEntityTypeConfiguration<Story>
		{
			static private void ConfigureStoryModel(ReferenceOwnershipBuilder<Story, StoryModel> builder)
			{
				_ = builder.Property(a => a.Title).IsRequired();
				_ = builder.Property(a => a.Summary).IsRequired();
				_ = builder.Property(a => a.Text).IsRequired();
				_ = builder.Property(a => a.PictureUrl);
			}

			void IEntityTypeConfiguration<Story>.Configure(EntityTypeBuilder<Story> builder)
			{
				_ = builder.Property(a => a.Id).IsRequired();
				_ = builder.OwnsOne(a => a.Model, ConfigureStoryModel);
				_ = builder.HasKey(a => a.Id);
			}
		}

		/// <summary>
		/// Initializes the <see cref="Story"/>.
		/// </summary>
		protected Story() { }
		/// <summary>
		/// Initializes the <see cref="Story"/>.
		/// </summary>
		/// <param name="model">The model of the <see cref="Story"/>.</param>
		/// <exception cref="ArgumentNullException"> <paramref name="model"/> is <see langword="null"/>.</exception>
		public Story(StoryModel model) => Model = model ?? throw new ArgumentNullException(nameof(model));

		/// <summary>
		/// Gets the identifier of the <see cref="Story"/>.
		/// </summary>
		public int Id { get; private set; }
		/// <summary>
		/// Gets the model of the <see cref="Story"/>.
		/// </summary>
		public StoryModel Model { get; private set; }
	}
}