using Microsoft.EntityFrameworkCore;
using News.Infrastracture.Entities;

namespace News.Infrastracture.DataContexts
{
	/// <summary>
	/// Represents a context of data of a news portal.
	/// </summary>
	public class DataContext : DbContext
	{
		/// <summary>
		/// Initializes the <see cref="DataContext"/>.
		/// </summary>
		/// <param name="options">The options to configure the <see cref="DataContext"/>.</param>
		public DataContext(DbContextOptions options) : base(options)
		{
			Stories = Set<Story>();
			Users = Set<User>();
			UserEmailConfirmations = Set<UserEmailConfirmation>();
		}

		/// <summary>
		/// Gets the <see cref="DbSet{TEntity}"/> of stories of the news portal.
		/// </summary>
		public DbSet<Story> Stories { get; }
		/// <summary>
		/// Gets the <see cref="DbSet{TEntity}"/> of users of the news portal.
		/// </summary>
		public DbSet<User> Users { get; private set; }
		/// <summary>
		/// Gets the <see cref="DbSet{TEntity}"/> of user email confirmations the news portal.
		/// </summary>
		public DbSet<UserEmailConfirmation> UserEmailConfirmations { get; }

		/// <summary>
		/// Configures the schema needed for the identity framework.
		/// </summary>
		/// <param name="builder">The builder being used to construct the model for this context.</param>
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			_ = builder.ApplyConfiguration(new Story.Configuration());
			_ = builder.ApplyConfiguration(new User.Configuration());
			_ = builder.ApplyConfiguration(new UserEmailConfirmation.Configuration());
		}
	}
}