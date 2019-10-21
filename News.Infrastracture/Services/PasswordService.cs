using System;
using System.Security.Cryptography;

namespace News.Infrastracture.Services
{
	/// <summary>
	/// Represents a service to generate random passwords.
	/// </summary>
	public class PasswordService
	{
		static private readonly char[] _chars;

		static PasswordService() => _chars = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

		private readonly int _length;
		private readonly RandomNumberGenerator _randomNumberGenerator;

		/// <summary>
		/// Initializes the <see cref="PasswordService"/>.
		/// </summary>
		/// <param name="length">The length of passwords.</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is less than 1.</exception>
		public PasswordService(int length)
		{
			_length = length >= 0x1 ? length : throw new ArgumentOutOfRangeException(nameof(length));
			_randomNumberGenerator = new RNGCryptoServiceProvider();
		}

		/// <summary>
		/// Generates a random password.
		/// </summary>
		public string Generate()
		{
			char[] chars = new char[_length];
			byte[] charIndexBytes = new byte[sizeof(int)];
			for (int i = 0x0; i != _length; i++)
			{
				_randomNumberGenerator.GetBytes(charIndexBytes);
				chars[i] = _chars[(BitConverter.ToInt32(charIndexBytes, 0x0) & 0x7FFFFFFF) % _chars.Length];
			}
			return new string(chars);
		}
	}
}