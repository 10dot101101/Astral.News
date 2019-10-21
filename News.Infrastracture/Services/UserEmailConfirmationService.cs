using Microsoft.EntityFrameworkCore;
using News.Abstractions.Repositories;
using News.Infrastracture.DataContexts;
using News.Infrastracture.Entities;
using System;
using System.Threading.Tasks;

namespace News.Infrastracture.Services
{
	/// <summary>
	/// Represents a service to control user email confirmation.
	/// </summary>
	public class UserEmailConfirmationService
	{
		private readonly DataContext _dataContext;
		private readonly IUserRepository<int> _userRepository;
		private readonly EmailService _emailService;
		private readonly string _controllerUrl;

		/// <summary>
		/// Initializes the <see cref="UserEmailConfirmationService"/>.
		/// </summary>
		/// <param name="dataContext">The <see cref="DataContext"/> of data of the news portal.</param>
		/// <param name="userRepository">The repository of the users of the news portal.</param>
		/// <param name="emailService">The <see cref="EmailService"/> of the news portal.</param>
		/// <param name="controllerUrl">The URL of the controller of email confirmations.</param>
		/// <exception cref="ArgumentNullException"><paramref name="dataContext"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="userRepository"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="emailService"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="controllerUrl"/> is <see langword="null"/>.</exception>
		public UserEmailConfirmationService(DataContext dataContext, IUserRepository<int> userRepository, EmailService emailService, string controllerUrl)
		{
			_dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
			_userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
			_emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
			_controllerUrl = controllerUrl ?? throw new ArgumentNullException(nameof(controllerUrl));
		}

		/// <summary>
		/// Adds a user whose email is to be confirmed.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="password">The password of the user.</param>
		/// <returns>The identifier of the confirmation.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="user"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentNullException"><paramref name="password"/> is <see langword="null"/>.</exception>
		public async Task AddEmailConfirmation(User user, string password)
		{
			if (user == null)
				throw new ArgumentNullException(nameof(user));
			if (password == null)
				throw new ArgumentNullException(nameof(password));
			Guid guid = Guid.NewGuid();
			_ = _dataContext.UserEmailConfirmations.Add(new UserEmailConfirmation(guid, user));
			await _emailService.SendAsync(user.Model.Name, user.Model.Email, "Email Confirmation", $"Your password: {password}. Follow the link to confirm you email: {_controllerUrl}{guid.ToString()}");
		}
		/// <summary>
		/// Asynchronously confirms a user email.
		/// </summary>
		/// <param name="id">The identifier of the email confirmation.</param>
		/// <returns>A task that represents the confirm operation. The task result contains the value that indicates whether the email has been confirmed.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="id"/> is <see langword="null"/>.</exception>
		public async Task<bool> ConfirmAsync(string id)
		{
			if (id == null)
				throw new ArgumentNullException(nameof(id));
			Guid guid = Guid.Parse(id);
			UserEmailConfirmation userEmailConfirmation = await _dataContext.UserEmailConfirmations.AsNoTracking().Include(a => a.ConfirmationUser).FirstOrDefaultAsync(a => a.Id == guid);
			if (userEmailConfirmation == null)
				return false;
			userEmailConfirmation.ConfirmationUser.EmailConfirmed = true;
			_ = _dataContext.UserEmailConfirmations.Remove(userEmailConfirmation);
			await _userRepository.UpdateAsync(userEmailConfirmation.ConfirmationUser);
			await _userRepository.Commit();
			return true;
		}
	}
}