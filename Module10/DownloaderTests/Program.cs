using System;
using Downloader.Enums;
using Downloader.Exception;
using Downloader.Logger;

namespace ConsoleApp1
{
	class Program
	{
		private static readonly ILogger logger = new Logger(true);

		static void Main(string[] args)
		{
			var down = new Downloader.SiteDownloader(
				logger,
				"http://www.google.com",
				@"d:\",
				1,
				Option.WithoutConstraints,
				new string[0]);

			try
			{
				down.DownloadSite();
			}
			catch (DownloaderException e)
			{
				logger.Log(e.Message);
			}

			Console.ReadKey();
		}
	}
}
