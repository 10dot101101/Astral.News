using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace News.Infrastracture.Services
{
	/// <summary>
	/// Represents a service to store images of a news portal.
	/// </summary>
	public class ImageService
	{
		private readonly string _directory;

		/// <summary>
		/// Initializes the <see cref="ImageService"/>.
		/// </summary>
		/// <param name="directory">The directory to store the images.</param>
		/// <exception cref="ArgumentNullException"><paramref name="directory"/> is <see langword="null"/>.</exception>
		public ImageService(string directory) => _directory = directory ?? throw new ArgumentNullException(nameof(directory));

		/// <summary>
		/// Adds an image to the news portal.
		/// </summary>
		/// <param name="formFile">The file .</param>
		/// <returns>The file name of the image.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="formFile"/> is <see langword="null"/>.</exception>
		public async Task<string> Add(IFormFile formFile)
		{
			if (formFile == null)
				throw new ArgumentNullException(nameof(formFile));
			string fileName;
			FileStream stream;
			string extension = Path.GetExtension(formFile.FileName);
			for (Guid guid = Guid.NewGuid(); ; guid = Guid.NewGuid())
			{
				string path = Path.Combine(_directory, fileName = guid + extension);
				try
				{
					stream = new FileStream(path, FileMode.Create, FileAccess.Write);
					break;
				}
				catch (IOException) { }
			}
			using (stream)
				await formFile.CopyToAsync(stream);
			return fileName;
		}
	}
}