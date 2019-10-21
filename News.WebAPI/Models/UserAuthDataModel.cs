namespace News.WebAPI.Models
{
	/// <summary>
	/// Represents a model of authorization data of a user of the news portal.
	/// </summary>
	public class UserAuthDataModel
	{
		/// <summary>
		/// Gets the login of the user.
		/// </summary>
		public string Login { get; set; }
		/// <summary>
		/// Gets the password of the user.
		/// </summary>
		public string Password { get; set; }
	}
}