using News.Abstractions.Entities;
using News.Abstractions.Models;
using News.Abstractions.ValueTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.Abstractions.Repositories
{
	/// <summary>
	/// Represents a repository of users of a news portal.
	/// </summary>
	/// <typeparam name="TID">The type of user identifiers.</typeparam>
	public interface IUserRepository<TID> : IEntityRepository<TID, IUserModel>
	{
		/// <summary>
		/// Asynchronously determines whether the user with a login or an email is contained.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <param name="login">The login.</param>
		/// <returns>A task that represents the detemine operation. The task result contains the value that indicates whether the user is contained.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="login"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="email"/> is <see langword="null"/>.</exception>
		Task<bool> Contains(string email, string login);
		/// <summary>
		/// Asynchronously gets a user by a login.
		/// </summary>
		/// <param name="login">The login.</param>
		/// <returns>A task that represents the get operation. The task result contains the user.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="login"/> is <see langword="null"/>.</exception>
		Task<IEntity<int, IUserModel>> GetByLoginAsync(string login);
		/// <summary>
		/// Asynchronously gets a number of users from the repository filtered by a search string.
		/// </summary>
		/// <param name="sortKind">A <see cref="UserSortKind"/> that specifies the kind of sorting of getting users.</param>
		/// <param name="offset">The number of the first of sorted users to get.</param>
		/// <param name="count">The number of users to get.</param>
		/// <param name="search">The search string.</param>
		/// <returns>A task that represents the get operation. The task result contains the users.</returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="sortKind"/> is out of range of valid values.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> is less than 0.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than 0.</exception>
		Task<IEnumerable<IEntity<int, IUserModel>>> GetAsync(UserSortKind sortKind, int offset, int count, string search);
	}
}