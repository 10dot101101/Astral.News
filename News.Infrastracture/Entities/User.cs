using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News.Abstractions.Entities;
using News.Infrastracture.Models;
using System;

namespace News.Infrastracture.Entities
{
	/// <summary>
	/// Represents a user of a news portal.
	/// </summary>
	public class User : IEntity<int, UserModel>
	{
		internal sealed class Configuration : IEntityTypeConfiguration<User>
		{
			static private void ConfigureUserModel(ReferenceOwnershipBuilder<User, UserModel> builder)
			{
				_ = builder.Property(a => a.Email).IsRequired();
				_ = builder.Property(a => a.Login).IsRequired();
				_ = builder.Property(a => a.PasswordHash).IsRequired();
				_ = builder.Property(a => a.Surname).IsRequired();
				_ = builder.Property(a => a.Name).IsRequired();
				_ = builder.Property(a => a.Patronymic).IsRequired();
				_ = builder.Property(a => a.BirthDate).IsRequired();
				_ = builder.Property(a => a.Role).IsRequired();
				_ = builder.HasIndex(a => a.Email).IsUnique();
				_ = builder.HasIndex(a => a.Login).IsUnique();
			}

			void IEntityTypeConfiguration<User>.Configure(EntityTypeBuilder<User> builder)
			{
				_ = builder.Property(a => a.Id).IsRequired();
				_ = builder.Property(a => a.EmailConfirmed).IsRequired();
				_ = builder.OwnsOne(a => a.Model, ConfigureUserModel);
				_ = builder.HasKey(a => a.Id);
			}
		}

		/// <summary>
		/// Initializes the <see cref="User"/>.
		/// </summary>
		protected User() { }
		/// <summary>
		/// Initializes the <see cref="User"/>.
		/// </summary>
		/// <param name="model">The model of the <see cref="User"/>.</param>
		/// <exception cref="ArgumentNullException"> <paramref name="model"/> is <see langword="null"/>.</exception>
		public User(UserModel model) => Model = model ?? throw new ArgumentNullException(nameof(model));

		/// <summary>
		/// Gets the identifier of the <see cref="User"/>.
		/// </summary>
		public int Id { get; private set; }
		/// <summary>
		/// Gets the value that indicates whether the email of the <see cref="User"/> is confirmed.
		/// </summary>
		public bool EmailConfirmed { get; set; }
		/// <summary>
		/// Gets the model of the <see cref="User"/>.
		/// </summary>
		public UserModel Model { get; private set; }
	}
}