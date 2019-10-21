using Microsoft.AspNetCore.Identity;
using News.Abstractions.Entities;
using News.Abstractions.Models;
using News.Abstractions.Repositories;
using News.Abstractions.Services;
using News.Abstractions.ValueTypes;
using News.Infrastracture.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace News.Infrastracture.Services
{
	/// <summary>
	/// Represents a user management service base of a news portal.
	/// </summary>
	public class UserService : IUserService<int>
	{
		private readonly IUserRepository<int> _repository;
		private readonly PasswordService _passwordService;
		private readonly UserEmailConfirmationService _userEmailConfirmationService;
		private readonly IPasswordHasher<object> _passwordHasher;

		/// <summary>
		/// Initializes the <see cref="UserService"/>.
		/// </summary>
		/// <param name="repository">The repository of the users of the news portal.</param>
		/// <param name="passwordService">The <see cref="PasswordService"/> of the news portal.</param>
		/// <param name="userEmailConfirmationService">The <see cref="UserEmailConfirmationService"/> of the news portal.</param>
		/// <exception cref="ArgumentNullException"><paramref name="repository"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="passwordService"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="userEmailConfirmationService"/> is <see langword="null"/>.</exception>
		public UserService(IUserRepository<int> repository, PasswordService passwordService, UserEmailConfirmationService userEmailConfirmationService)
		{
			_repository = repository ?? throw new ArgumentNullException(nameof(repository));
			_passwordService = passwordService ?? throw new ArgumentNullException(nameof(passwordService));
			_userEmailConfirmationService = userEmailConfirmationService ?? throw new ArgumentNullException(nameof(userEmailConfirmationService));
			_passwordHasher = new PasswordHasher<object>();
		}

		/// <summary>
		/// Asynchronously adds a new user to the news portal.
		/// </summary>
		/// <param name="model">The model of the user.</param>
		/// <returns>A task that represents the add operation. The task result contains the user.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null"/>.</exception>
		public async Task<IEntity<int, IUserModel>> AddAsync(IUserModel model)
		{
			string password = _passwordService.Generate();
			model.PasswordHash = _passwordHasher.HashPassword(null, password);
			User user = (User)await _repository.AddAsync(model);
			if (user == null)
				return null;
			await _userEmailConfirmationService.AddEmailConfirmation(user, password);
			await _repository.Commit();
			return user;
		}
		/// <summary>
		/// Asynchronously gets a user by a login and a password.
		/// </summary>
		/// <param name="login">The login.</param>
		/// <param name="password">The password.</param>
		/// <returns>A task that represents the get operation. The task result contains the user.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="login"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="password"/> is <see langword="null"/>.</exception>
		public async Task<IEntity<int, IUserModel>> GetByAuthorizationDataAsync(string login, string password)
		{
			IEntity<int, IUserModel> user = await _repository.GetByLoginAsync(login);
			return user == null || _passwordHasher.VerifyHashedPassword(null, user.Model.PasswordHash, password) == PasswordVerificationResult.Success ? user : null;
		}
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
		public Task<IEnumerable<IEntity<int, IUserModel>>> GetAsync(UserSortKind sortKind, int offset, int count, string search)
		{
			if (sortKind < UserSortKind.Min || sortKind > UserSortKind.Max)
				throw new ArgumentOutOfRangeException(nameof(sortKind));
			if (offset < 0x0)
				throw new ArgumentOutOfRangeException(nameof(offset));
			if (count < 0x0)
				throw new ArgumentOutOfRangeException(nameof(offset));
			return _repository.GetAsync(sortKind, offset, count, search);
		}
		/// <summary>
		/// Asynchronously changes a user in the news portal.
		/// </summary>
		/// <param name="id">The identifier of the user.</param>
		/// <param name="model">The new model of the user.</param>
		/// <returns>A task that represents the change operation. If the task result is <see langword="true"/> the user has been changed; if the task result is <see langword="null"/> the user was not found; otherwise, the user with the specified email or login already exists.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null"/>.</exception>
		public async Task<bool?> ChangeAsync(int id, IUserModel model)
		{
			IEntity<int, IUserModel> user = await _repository.GetAsync(id);
			if (user == null)
				return null;
			if (await _repository.Contains(model.Email, model.Login))
				return false;
			user.Model.Email = model.Email;
			user.Model.Login = model.Login;
			user.Model.Surname = model.Surname;
			user.Model.Name = model.Name;
			user.Model.Patronymic = model.Patronymic;
			user.Model.BirthDate = model.BirthDate;
			user.Model.Role = model.Role;
			await _repository.UpdateAsync(user);
			await _repository.Commit();
			return true;
		}
		/// <summary>
		/// Asynchronously removes a user from the news portal.
		/// </summary>
		/// <param name="id">The identifier of the user.</param>
		/// <returns>A task that represents the remove operation. The task result contains value that indicates whether the user has been removed.</returns>
		public async Task<bool> RemoveAsync(int id)
		{
			if (!await _repository.RemoveAsync(id))
				return false;
			await _repository.Commit();
			return true;
		}
	}
}