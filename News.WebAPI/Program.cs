using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace News.WebAPI
{
	/// <summary>
	/// Represents the program.
	/// </summary>
	static public class Program
	{
		/// <summary>
		/// Starts the program.
		/// </summary>
		/// <param name="args">The arguments of the program start.</param>
		public static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();

		/// <summary>
		/// Creates a web host builder.
		/// </summary>
		/// <param name="args">The arguments of the program start.</param>
		/// <returns>An <see cref="IWebHostBuilder"/>.</returns>
		public static IWebHostBuilder CreateWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
	}
}
