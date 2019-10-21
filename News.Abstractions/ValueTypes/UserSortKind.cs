namespace News.Abstractions.ValueTypes
{
	/// <summary>
	/// Specifies the kinds of user sorting.
	/// </summary>
	public enum UserSortKind
	{
		/// <summary>
		/// Specifies the minimum sort kind.
		/// </summary>
		Min = 0x0,
		/// <summary>
		/// Specifies the maximum sort kind.
		/// </summary>
		Max = 0x7,
		/// <summary>
		/// Specifies the lack of any sorting.
		/// </summary>
		None = 0x0,
		/// <summary>
		/// Specifies the sorting by user emails.
		/// </summary>
		Email = 0x1,
		/// <summary>
		/// Specifies the sorting by user logins.
		/// </summary>
		Login = 0x2,
		/// <summary>
		/// Specifies the sorting by user surnames.
		/// </summary>
		Surname = 0x3,
		/// <summary>
		/// Specifies the sorting by user names.
		/// </summary>
		Name = 0x4,
		/// <summary>
		/// Specifies the sorting by user patronymics.
		/// </summary>
		Patronymic = 0x5,
		/// <summary>
		/// Specifies the sorting by user birth dates.
		/// </summary>
		BirthDate = 0x6,
		/// <summary>
		/// Specifies the sorting by user roles.
		/// </summary>
		Role = 0x7,
	}
}