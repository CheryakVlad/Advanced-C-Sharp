using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Downloader.Enums;
using Downloader.Logger;
using HtmlAgilityPack;

namespace Downloader
{
	public class LinkService
	{
		private readonly HtmlNode _htmlNode;
		private readonly string _rootFolder;
		private readonly string _rootPath;
		private readonly string _protocol;
		private readonly int _level;
		private readonly int _nestLevelLimit;
		private readonly string[] _extensionConstraints;
		private readonly ILogger _logger;
		private Option _option;
		private List<string> _links;

		public LinkService(HtmlNode htmlNode, string rootFolder, string rootPath,
			string[] extensionConstraints, int level, int nestLevelLimit, ILogger logger, Option option)
		{
			_option = option;
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_htmlNode = htmlNode;
			_rootFolder = rootFolder;
			_rootPath = RemoveParamsFromUrl(rootPath);
			_protocol = rootPath.Substring(0, rootPath.IndexOf('/'));
			_extensionConstraints = extensionConstraints;
			_level = level;
			_nestLevelLimit = nestLevelLimit;
			_links = new List<string>(0);
		}

		public List<string> Search()
		{
			Search(_htmlNode);
			return _links;
		}

		private void Search(HtmlNode parent)
		{
			foreach (var child in parent.ChildNodes)
			{
				GetRefs(child.Attributes);
				GetFiles(child.Attributes);
				Search(child);
			}
		}

		public string RemoveParamsFromUrl(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException(nameof(url));
			}

			return url.Split('?')[0];
		}

		private void GetRefs(HtmlAttributeCollection htmlAttributes)
		{
			var hrefAttr = htmlAttributes["href"];
			if (hrefAttr != null)
			{
				var link = hrefAttr.Value;

				var path = RemoveParamsFromUrl(link);
				var extension = Path.GetExtension(path);

				if (_extensionConstraints.Contains(extension))
				{
					var fileSaver = new FileSaver(path, _rootPath, _rootFolder, _protocol, _logger);
					fileSaver.SaveSource();
					return;
				}

				_logger.Log("Link founded " + link);
				if (!link.StartsWith("http"))
				{
					if (link.StartsWith("//"))
					{
						link = _protocol + link;
					}
					else
					{
						link = _rootPath + link;
					}
				}

				var newLink = SiteDownloader.GetDirectoryName(link);
				if (_level + 1 <= _nestLevelLimit)
				{
					if (_option == Option.WithinDomain && new Uri(link).Host != new Uri(_rootPath).Host)
					{
						return;
					}
					hrefAttr.Value = Path.Combine(newLink, "index.html");
					_links.Add(link);
				}
			}
		}

		private void GetFiles(HtmlAttributeCollection htmlAttributes)
		{
			var srcAttr = htmlAttributes["src"];
			if (srcAttr == null)
			{
				return;
			}

			var link = srcAttr.Value;
			var urlWithoutParams = RemoveParamsFromUrl(link);

			if (String.IsNullOrEmpty(urlWithoutParams))
			{
				return;
			}

			if (_extensionConstraints.Length == 0 ||
				_extensionConstraints.Contains(Path.GetExtension(urlWithoutParams)))
			{
				var fileSaver = new FileSaver(urlWithoutParams, _rootPath, _rootFolder, _protocol, _logger);
				fileSaver.SaveSource();
			}
		}
	}
}
