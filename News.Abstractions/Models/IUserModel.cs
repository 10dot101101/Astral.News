using News.Abstractions.ValueTypes;
using System;

namespace News.Abstractions.Models
{
	/// <summary>
	/// Represents a model of a user of a news portal.
	/// </summary>
	public interface IUserModel
	{
		/// <summary>
		/// Gets or sets the login of the user.
		/// </summary>
		/// <exception cref="ArgumentNullException">The specified value is <see langword="null"/>.</exception>
		string Email { get; set; }
		/// <summary>
		/// Gets or sets the login of the user.
		/// </summary>
		/// <exception cref="ArgumentNullException">The specified value is <see langword="null"/>.</exception>
		string Login { get; set; }
		/// <summary>
		/// Gets or sets the hash of the password of the user.
		/// </summary>
		/// <exception cref="ArgumentNullException">The specified value is <see langword="null"/>.</exception>
		string PasswordHash { get; set; }
		/// <summary>
		/// Gets or sets the surname of the user.
		/// </summary>
		string Surname { get; set; }
		/// <summary>
		/// Gets or sets the name of the user.
		/// </summary>
		string Name { get; set; }
		/// <summary>
		/// Gets or sets the patronymic of the user.
		/// </summary>
		string Patronymic { get; set; }
		/// <summary>
		/// Gets or sets the date of birth of the user.
		/// </summary>
		DateTime BirthDate { get; set; }
		/// <summary>
		/// Gets or sets the role of the user.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">The specified value is out of range of valid values.</exception>
		UserRole Role { get; set; }
	}
}