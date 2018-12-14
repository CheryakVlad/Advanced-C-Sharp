
namespace Downloader.Exception
{
	public class DownloaderException : System.Exception
	{
		public DownloaderException()
		{
		}

		public DownloaderException(string message) : base(message)
		{
		}

		public DownloaderException(string message, System.Exception inner) : base(message, inner)
		{
		}
	}
}
