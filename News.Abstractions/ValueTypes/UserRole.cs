namespace News.Abstractions.ValueTypes
{
	/// <summary>
	/// Specifies the roles of users of a news portal.
	/// </summary>
	public enum UserRole
	{
		/// <summary>
		/// Specifies the minimum role value.
		/// </summary>
		Min = 0x0,
		/// <summary>
		/// Specifies the maximum role value.
		/// </summary>
		Max = 0x1,
		/// <summary>
		/// Specifies the administrator role of a news portal.
		/// </summary>
		Admin = 0x0,
		/// <summary>
		/// Specifies the editor role of a news portal.
		/// </summary>
		Editor = 0x1,
	}
}