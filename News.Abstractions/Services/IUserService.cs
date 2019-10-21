using News.Abstractions.Entities;
using News.Abstractions.Models;
using News.Abstractions.ValueTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.Abstractions.Services
{
	/// <summary>
	/// Represents a user management service base of a news portal.
	/// </summary>
	/// <typeparam name="TID">The type of user identifiers.</typeparam>
	public interface IUserService<TID>
	{
		/// <summary>
		/// Asynchronously adds a new user to the news portal.
		/// </summary>
		/// <param name="model">The model of the user.</param>
		/// <returns>A task that represents the add operation. The task result contains the user whether the user has been added.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null"/>.</exception>
		Task<IEntity<TID, IUserModel>> AddAsync(IUserModel model);
		/// <summary>
		/// Asynchronously gets a user by a login and a password.
		/// </summary>
		/// <param name="login">The login.</param>
		/// <param name="password">The password.</param>
		/// <returns>A task that represents the get operation. The task result contains the user.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="login"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="password"/> is <see langword="null"/>.</exception>
		Task<IEntity<int, IUserModel>> GetByAuthorizationDataAsync(string login, string password);
		/// <summary>
		/// Asynchronously gets a number of users filterd by a search string from the news portal.
		/// </summary>
		/// <param name="sortKind">A <see cref="UserSortKind"/> that specifies the kind of sorting of getting users.</param>
		/// <param name="offset">The number of the first of sorted users to get.</param>
		/// <param name="count">The number of users to get.</param>
		/// <param name="search">The search string.</param>
		/// <returns>A task that represents the get operation. The task result contains the users.</returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="sortKind"/> is out of range of valid values.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="offset"/> is less than 0.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than 0.</exception>
		Task<IEnumerable<IEntity<TID, IUserModel>>> GetAsync(UserSortKind sortKind, int offset, int count, string search);
		/// <summary>
		/// Asynchronously changes a user in the news portal.
		/// </summary>
		/// <param name="id">The identifier of the user.</param>
		/// <param name="model">The new model of the user.</param>
		/// <returns>A task that represents the change operation. If the task result is <see langword="true"/> the user has been changed; if the task result is <see langword="null"/> the user was not found; otherwise, the user with the specified email or login already exists.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="id"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null"/>.</exception>
		Task<bool?> ChangeAsync(TID id, IUserModel model);
		/// <summary>
		/// Asynchronously removes a user from the news portal.
		/// </summary>
		/// <param name="id">The identifier of the user.</param>
		/// <returns>A task that represents the remove operation. The task result contains value that indicates whether the user has been removed.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="id"/> is <see langword="null"/>.</exception>
		Task<bool> RemoveAsync(TID id);
	}
}