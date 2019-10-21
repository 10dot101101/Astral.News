using News.Abstractions.Models;
using News.Abstractions.ValueTypes;
using System;

namespace News.Infrastracture.Models
{
	/// <summary>
	/// Represents a model of a user of a news portal.
	/// </summary>
	public class UserModel : IUserModel
	{
		private string _email;
		private string _login;
		private string _passwordHash;
		private UserRole _role;

		/// <summary>
		/// Gets or sets the login of a user.
		/// </summary>
		/// <exception cref="ArgumentNullException">The specified value is <see langword="null"/>.</exception>
		public string Email { get => _email; set => _email = value ?? throw new ArgumentNullException(nameof(value)); }
		/// <summary>
		/// Gets or sets the login of a user.
		/// </summary>
		/// <exception cref="ArgumentNullException">The specified value is <see langword="null"/>.</exception>
		public string Login { get => _login; set => _login = value ?? throw new ArgumentNullException(nameof(value)); }
		/// <summary>
		/// Gets the hash of the password of a user.
		/// </summary>
		/// <exception cref="ArgumentNullException">The specified value is <see langword="null"/>.</exception>
		public string PasswordHash { get => _passwordHash; set => _passwordHash = value ?? throw new ArgumentNullException(nameof(value)); }
		/// <summary>
		/// Gets or sets the surname of a user.
		/// </summary>
		public string Surname { get; set; }
		/// <summary>
		/// Gets or sets the name of a user.
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Gets or sets the patronymic of a user.
		/// </summary>
		public string Patronymic { get; set; }
		/// <summary>
		/// Gets or sets the date of birth of a user.
		/// </summary>
		public DateTime BirthDate { get; set; }
		/// <summary>
		/// Gets or sets the role of a user.
		/// </summary>
		/// <exception cref="ArgumentNullException">The specified value is <see langword="null"/>.</exception>
		public UserRole Role { get => _role; set => _role = value >= UserRole.Min && value <= UserRole.Max ? value : throw new ArgumentOutOfRangeException(nameof(value)); }
	}
}