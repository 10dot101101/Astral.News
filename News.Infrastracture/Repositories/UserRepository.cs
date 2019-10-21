using Microsoft.EntityFrameworkCore;
using News.Abstractions.Entities;
using News.Abstractions.Models;
using News.Abstractions.Repositories;
using News.Abstractions.ValueTypes;
using News.Infrastracture.DataContexts;
using News.Infrastracture.Entities;
using News.Infrastracture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace News.Infrastracture.Repositories
{
	/// <summary>
	/// Represents a repository of users of a news portal.
	/// </summary>
	public class UserRepository : IUserRepository<int>
	{
		private readonly DataContext _dataContext;

		/// <summary>
		/// Initializes the <see cref="UserRepository"/>.
		/// </summary>
		/// <param name="dataContext">The <see cref="DataContext"/> of data of the news portal.</param>
		/// <exception cref="ArgumentNullException"><paramref name="dataContext"/> is <see langword="null"/>.</exception>
		public UserRepository(DataContext dataContext) => _dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));

		/// <summary>
		/// Commits the made changes in the repository.
		/// </summary>
		/// <returns>A task that represents the commit operation.</returns>
		public Task Commit() => _dataContext.SaveChangesAsync();
		/// <summary>
		/// Asynchronously adds a user to the repository.
		/// </summary>
		/// <param name="model">The model of the user.</param>
		/// <returns>A task that represents the add operation. The task result contains the user.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="model"/> is <see langword="null"/>.</exception>
		public async Task<IEntity<int, IUserModel>> AddAsync(IUserModel model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));
			if (await _dataContext.Users.AnyAsync(a => a.Model.Email == model.Email || a.Model.Login == model.Login))
				return null;
			UserModel userModel = new UserModel
			{
				Email = model.Email,
				Login = model.Login,
				PasswordHash = model.PasswordHash,
				Surname = model.Surname,
				Name = model.Name,
				Patronymic = model.Patronymic,
				BirthDate = model.BirthDate,
				Role = model.Role
			};
			User user = new User(userModel);
			_ = await _dataContext.AddAsync(user);
			return user;
		}
		/// <summary>
		/// Asynchronously gets users from the repository.
		/// </summary>
		/// <returns>A task that represents the get operation. The task result contains the users.</returns>
		public async Task<IEnumerable<IEntity<int, IUserModel>>> GetAsync() => await _dataContext.Users.AsNoTracking().Include(nameof(User.Model)).ToArrayAsync();
		/// <summary>
		/// Asynchronously gets an user from the repository.
		/// </summary>
		/// <param name="id">The identifier of the user.</param>
		/// <returns>A task that represents the get operation. The task result contains the user.</returns>
		public async Task<IEntity<int, IUserModel>> GetAsync(int id) => await _dataContext.Users.AsNoTracking().Include(nameof(User.Model)).FirstOrDefaultAsync(a => a.Id == id);
		/// <summary>
		/// Asynchronously determines whether the user with a login or an email is contained.
		/// </summary>
		/// <param name="email">The email.</param>
		/// <param name="login">The login.</param>
		/// <returns>A task that represents the detemine operation. The task result contains the value that indicates whether the user is contained.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="login"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="email"/> is <see langword="null"/>.</exception>
		public async Task<bool> Contains(string email, string login)
		{
			if (login == null)
				throw new ArgumentNullException(nameof(login));
			if (email == null)
				throw new ArgumentNullException(nameof(email));
			return await _dataContext.Users.AnyAsync(a => a.Model.Email == email || a.Model.Login == login);
		}
		/// <summary>
		/// Asynchronously gets a user by a login.
		/// </summary>
		/// <param name="login">The login.</param>
		/// <returns>A task that represents the get operation. The task result contains the user.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="login"/> is <see langword="null"/>.</exception>
		public async Task<IEntity<int, IUserModel>> GetByLoginAsync(string login)
		{
			if (login == null)
				throw new ArgumentNullException(nameof(login));
			return await _dataContext.Users.AsNoTracking().Include(nameof(User.Model)).FirstOrDefaultAsync(a => a.Model.Login == login);
		}
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
		public async Task<IEnumerable<IEntity<int, IUserModel>>> GetAsync(UserSortKind sortKind, int offset, int count, string search)
		{
			if (sortKind < UserSortKind.Min || sortKind > UserSortKind.Max)
				throw new ArgumentOutOfRangeException(nameof(sortKind));
			if (offset < 0x0)
				throw new ArgumentOutOfRangeException(nameof(offset));
			if (count < 0x0)
				throw new ArgumentOutOfRangeException(nameof(offset));
			IQueryable<User> userQuery = _dataContext.Users.AsNoTracking();
			switch (sortKind)
			{
				case UserSortKind.Email:
					userQuery = userQuery.OrderBy(a => a.Model.Email);
					break;
				case UserSortKind.Login:
					userQuery = userQuery.OrderBy(a => a.Model.Login);
					break;
				case UserSortKind.Surname:
					userQuery = userQuery.OrderBy(a => a.Model.Surname);
					break;
				case UserSortKind.Name:
					userQuery = userQuery.OrderBy(a => a.Model.Name);
					break;
				case UserSortKind.Patronymic:
					userQuery = userQuery.OrderBy(a => a.Model.Patronymic);
					break;
				case UserSortKind.BirthDate:
					userQuery = userQuery.OrderBy(a => a.Model.BirthDate);
					break;
				case UserSortKind.Role:
					userQuery = userQuery.OrderBy(a => a.Model.Role);
					break;
				default:
					break;
			}
			if (search != null)
				userQuery = userQuery.Where(a => a.Model.Email.Contains(search) || a.Model.Login.Contains(search) || a.Model.Surname.Contains(search) || a.Model.Name.Contains(search) || a.Model.Patronymic.Contains(search) || a.Model.BirthDate.ToString().Contains(search) || a.Model.Role.ToString().Contains(search));
			return await userQuery.Skip(offset).Take(count).ToArrayAsync();
		}
		/// <summary>
		/// Asynchronously updates the model of an user in the repository.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns>A task that represents the update operation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="user"/> is <see langword="null"/>.</exception>
		public Task UpdateAsync(IEntity<int, IUserModel> user)
		{
			if (user == null)
				throw new ArgumentNullException(nameof(user));
			_dataContext.Entry(user).State = EntityState.Modified;
			_dataContext.Entry(user.Model).State = EntityState.Modified;
			return Task.CompletedTask;
		}
		/// <summary>
		/// Asynchronously removes an user from the repository.
		/// </summary>
		/// <param name="id">The identifier of the user.</param>
		/// <returns>A task that represents the remove operation. The task result contains value that indicates whether the user has been removed.</returns>
		public async Task<bool> RemoveAsync(int id)
		{
			IEntity<int, IUserModel> user = await GetAsync(id);
			if (user == null)
				return false;
			_ = _dataContext.Users.Remove((User)user);
			return true;
		}
	}
}