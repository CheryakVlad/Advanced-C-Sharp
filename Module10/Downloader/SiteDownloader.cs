using System;
using System.IO;
using System.Net.Http;
using System.Text;
using Downloader.Enums;
using Downloader.Exception;
using Downloader.Logger;
using HtmlAgilityPack;

namespace Downloader
{
	public class SiteDownloader
    {
		#region Private fields

		private const int _pathLimit = 70;
		private int _analasisLevel;
		private string _startUrl;
		private string _path;
		private ILogger _logger;
		private readonly string[] _extensionConstraints;
		private readonly Option _option;

		#endregion

		#region Constructors

		public SiteDownloader(ILogger logger, string startUrl, string storagePath,
			int nestLevelLimit,
			Option option,
			string[] extensionConstraints)
		{
			_logger = logger;
			_startUrl = startUrl;
			_path = storagePath;
			_analasisLevel = nestLevelLimit;
			_option = option;
			_extensionConstraints = extensionConstraints;
		}

		#endregion

		#region Public methods

		public void DownloadSite()
		{
			DownloadSite(_startUrl, 0, _path);
		}

		public static string GetDirectoryName(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException(nameof(path));
			}

			try
			{
				var uri = new Uri(path);
				var url = uri.OriginalString.Replace(".", "").Replace(@"/", "").Replace(":", "")
					.Replace("?", "").Replace("|", "");

				return url.Substring(url.Length - _pathLimit > 0 ? url.Length - _pathLimit : 0);
			}
			catch (UriFormatException)
			{
				throw new DownloaderException("Uri format problems");
			}
		}

		#endregion

		#region Private methods

		private void DownloadSite(string startUrl, int level, string folder)
		{
			if (level > _analasisLevel)
			{
				return;
			}

			var document = new HtmlDocument();
			var url = new Uri(startUrl);

			LoadHtmlDocument(url, document);

			string path = Path.Combine(folder, GetDirectoryName(startUrl));
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			var linkService = new LinkService(document.DocumentNode, path, url.AbsoluteUri,
				_extensionConstraints, level, _analasisLevel, _logger, _option);

			var linkList = linkService.Search();

			document.Save(Path.Combine(path, "index.html"));

			foreach (var link in linkList)
			{
				_logger.Log(link);
				DownloadSite(link, level + 1, path);
			}
		}

		private void LoadHtmlDocument(Uri url, HtmlDocument document)
		{
			using (HttpClient client = new HttpClient())
			{
				var response =
					client.GetAsync(url.AbsoluteUri).Result;

				try
				{
					response.EnsureSuccessStatusCode();
				}
				catch (HttpRequestException e)
				{
					_logger.Log(e.Message);
				}

				using (var stream = response.Content.ReadAsStreamAsync().Result)
				{
					document.Load(stream, Encoding.UTF8);
				}

				document.Save(Path.Combine(_path, "index.html"));
			}
		}

		#endregion
	}
}
