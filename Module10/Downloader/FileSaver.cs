using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Downloader.Exception;
using Downloader.Logger;

namespace Downloader
{
	public class FileSaver
	{
		private const int _bufferSize = 2000;
		private readonly string _rootPath;
		private readonly string _folder;
		private readonly string _url;
		private readonly string _protocol;
		private readonly ILogger _logger;

		public FileSaver(string url, string rootPath, string folder, string protocol, ILogger logger)
		{
			_url = url;
			_rootPath = rootPath;
			_folder = folder;
			_protocol = protocol;
			_logger = logger;
		}

		public void SaveSource()
		{
			SaveSourceAsync(_url);
		}

		private async void SaveSourceAsync(string sourceUrl)
		{
			_logger.Log("Resource founded");
			var fileName = Path.GetFileName(sourceUrl);

			fileName = "_" + fileName;
			var fullName = Path.Combine(_folder, fileName);

			if (!sourceUrl.StartsWith("http"))
			{
				if (sourceUrl.StartsWith("//"))
				{
					sourceUrl = _protocol + sourceUrl;
				}
				else
				{
					sourceUrl = _rootPath + sourceUrl.Substring(1);
				}
			}

			try
			{
				await DownloadAsync(sourceUrl, fullName);
			}
			catch (System.Exception ex)
			{
				_logger.Log("Error while downloading");
			}
		}

		private async Task DownloadAsync(string requestUri, string filename)
		{
			if (requestUri == null)
			{
				throw new DownloaderException(nameof(requestUri));
			}

			await DownloadAsync(new Uri(requestUri), filename);
		}

		private async Task DownloadAsync(Uri requestUri, string filename)
		{
			if (filename == null)
			{
				throw new DownloaderException(nameof(filename));
			}

			using (var httpClient = new HttpClient())
			{
				using (var request = new HttpRequestMessage(HttpMethod.Get, requestUri))
				{
					using (Stream contentStream = await (await httpClient.SendAsync(request)).Content.ReadAsStreamAsync(),
							stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None, _bufferSize, true))
					{
						await contentStream.CopyToAsync(stream);
					}
				}
			}
		}
	}
}
