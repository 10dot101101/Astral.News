using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace News.Infrastracture.Entities
{
	/// <summary>
	/// Represents a user of a news portal.
	/// </summary>
	public class UserEmailConfirmation
	{
		internal sealed class Configuration : IEntityTypeConfiguration<UserEmailConfirmation>
		{
			void IEntityTypeConfiguration<UserEmailConfirmation>.Configure(EntityTypeBuilder<UserEmailConfirmation> builder)
			{
				_ = builder.Property(a => a.Id).IsRequired().ValueGeneratedNever();
				_ = builder.HasOne(a => a.ConfirmationUser).WithOne().IsRequired();
				_ = builder.HasKey(a => a.Id);
			}
		}

		/// <summary>
		/// Initializes the <see cref="UserEmailConfirmation"/>.
		/// </summary>
		protected UserEmailConfirmation() { }
		/// <summary>
		/// Initializes the <see cref="ConfirmationUser"/>.
		/// </summary>
		/// <param name="id">The id of the confirmation.</param>
		/// <param name="user">The user whose email is to be confirmed.</param>
		/// <exception cref="ArgumentNullException"> <paramref name="user"/> is <see langword="null"/>.</exception>
		public UserEmailConfirmation(Guid id, User user)
		{
			Id = id;
			ConfirmationUser = user ?? throw new ArgumentNullException(nameof(user));
		}

		/// <summary>
		/// Gets the id of the confirmation.
		/// </summary>
		public Guid Id { get; private set; }
		/// <summary>
		/// Gets the identifier of the <see cref="ConfirmationUser"/>.
		/// </summary>
		public int ConfirmationUserID { get; private set; }
		/// <summary>
		/// Gets the user whose email is to be confirmed.
		/// </summary>
		public User ConfirmationUser { get; private set; }
	}
}