using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using System;
using System.Threading.Tasks;

namespace News.Infrastracture.Services
{
	/// <summary>
	/// Represents a service to send email messages.
	/// </summary>
	public class EmailService : IDisposable
	{
		private readonly string _name;
		private readonly string _emailAddress;
		private readonly string _password;
		private readonly string _address;
		private readonly int _port;
		private readonly bool _useSsl;
		private SmtpClient _client;

		/// <summary>
		/// Initializes the <see cref="EmailService"/>.
		/// </summary>
		/// <param name="name">The name of the service sending emails.</param>
		/// <param name="emailAddress">The email address of the service.</param>
		/// <param name="password">The password of the email address.</param>
		/// <param name="address">The address of the email server.</param>
		/// <param name="port">The port of the email server.</param>
		/// <param name="useSsl">The value that indicates whether the ssl connection should be established.</param>
		public EmailService(string name, string emailAddress, string password, string address, int port, bool useSsl)
		{
			_name = name;
			_emailAddress = emailAddress;
			_password = password;
			_address = address;
			_port = port;
			_useSsl = useSsl;
		}

		/// <summary>
		/// Asynchronously send a message.
		/// </summary>
		/// <param name="name">The name of the mailbox.</param>
		/// <param name="emailAddress">The address of the mailbox.</param>
		/// <param name="subject">The subject of the message.</param>
		/// <param name="message">The message.</param>
		/// <returns>A task that represents the asynchronous send operation.</returns>
		public async Task SendAsync(string name, string emailAddress, string subject, string message)
		{
			if (_client == null)
			{
				_client = new SmtpClient();
				await _client.ConnectAsync(_address, _port, _useSsl);
				await _client.AuthenticateAsync(_emailAddress, _password);
			}
			MimeMessage emailMessage = new MimeMessage();
			emailMessage.From.Add(new MailboxAddress(_name, _emailAddress));
			emailMessage.To.Add(new MailboxAddress(name, emailAddress));
			emailMessage.Subject = subject;
			emailMessage.Body = new TextPart(TextFormat.Text) { Text = message };
			await _client.SendAsync(emailMessage);
		}
		/// <summary>
		/// Releases all resources used by the <see cref="EmailService"/>.
		/// </summary>
		public void Dispose()
		{
			if (_client == null)
				return;
			_client.Dispose();
		}
	}
}