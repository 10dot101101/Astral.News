namespace News.WebAPI.Models
{
	/// <summary>
	/// Represents a model of a user of the news portal.
	/// </summary>
	public class UserEntityModel
	{
		/// <summary>
		/// Initializes the <see cref="UserEntityModel"/>.
		/// </summary>
		/// <param name="id">The identifier of the user.</param>
		public UserEntityModel(int id) => Id = id;

		/// <summary>
		/// Gets the identifier of the user.
		/// </summary>
		public int Id { get; }
		/// <summary>
		/// Gets the data of the user.
		/// </summary>
		public UserDataModel Data { get; set; }
	}
}