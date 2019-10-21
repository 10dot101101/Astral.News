using News.Abstractions.ValueTypes;
using System;

namespace News.WebAPI.Models
{
	/// <summary>
	/// Represents a model of data of a user.
	/// </summary>
	public class UserDataModel
	{
		/// <summary>
		/// Gets or sets the login of the user.
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// Gets or sets the login of the user.
		/// </summary>
		public string Login { get; set; }
		/// <summary>
		/// Gets or sets the surname of the user.
		/// </summary>
		public string Surname { get; set; }
		/// <summary>
		/// Gets or sets the name of the user.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Gets or sets the patronymic of the user.
		/// </summary>
		public string Patronymic { get; set; }
		/// <summary>
		/// Gets or sets the date of birth of the user.
		/// </summary>
		public DateTime BirthDate { get; set; }
		/// <summary>
		/// Gets or sets the role of the user.
		/// </summary>
		public UserRole Role { get; set; }
	}
}