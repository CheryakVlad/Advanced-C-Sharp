using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Module4.Log;
using Module4.Resources;

namespace Module4
{
	public class Watcher
	{
		#region Constants

		private const string defaultFilter = "*.*";
		private const string dateFormat = "yy-MM-dd-hh-mm-ss";
		private const string delimiter = "_";

		#endregion

		#region Private fields

		private static readonly WatcherSettings _settings =
			(WatcherSettings)ConfigurationManager.
			GetSection("FileSystemWatcherSettings");

		private readonly ILogger _logger = new Logger();

		private List<RuleModel> _rules;

		#endregion

		#region Public methods

		public void Run()
		{
			CultureInfo.DefaultThreadCurrentCulture = _settings.CultureInfo;
			CultureInfo.DefaultThreadCurrentUICulture = _settings.CultureInfo;

			Thread.CurrentThread.CurrentUICulture = _settings.CultureInfo;
			Thread.CurrentThread.CurrentCulture = _settings.CultureInfo;
			
			var directoryPaths = from Directory directory in _settings.Directories
										 select directory.Path;

			_rules = (from Rule rule in _settings.Rules
					select new RuleModel
					{
						FilePattern = rule.FilePattern,
						DestinationFolder = rule.DestinationFolder,
						FileNameChanged = rule.FileNameChanged,
						Counter = 0
					}).ToList();

			foreach (string path in directoryPaths)
			{
				WatcherInitialize(path);
			}
		}

		#endregion

		#region Private methods

		private void WatcherInitialize(string path)
		{
			FileSystemWatcher watcher = new FileSystemWatcher();
			watcher.Path = path;

			watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
			   | NotifyFilters.FileName | NotifyFilters.DirectoryName;

			watcher.Filter = defaultFilter;

			watcher.Changed += new FileSystemEventHandler(OnChanged);
			watcher.Created += new FileSystemEventHandler(OnChanged);
			watcher.Deleted += new FileSystemEventHandler(OnChanged);
			watcher.Renamed += new RenamedEventHandler(OnRenamed);

			watcher.EnableRaisingEvents = true;
		}

		private string SearchDirByRule(string fileFullPath)
		{
			string dirPath = _rules.Where(x => Regex.IsMatch(fileFullPath, x.FilePattern, RegexOptions.Compiled)).FirstOrDefault().DestinationFolder;
			
			if (!string.IsNullOrWhiteSpace(dirPath))
			{
				_logger.Log(string.Format(Messages.FileMoveMessage, dirPath, DateTimeOffset.Now));
			}
			return dirPath;
		}

		private string RenameFile(string fileFullPath)
		{
			RuleModel rule = _rules.Where(x => Regex.IsMatch(fileFullPath, x.FilePattern, RegexOptions.Compiled)).FirstOrDefault();

			switch (rule.FileNameChanged)
			{
				case FileNameChanged.Date:
					fileFullPath = DateTimeOffset.Now.ToString(dateFormat) + fileFullPath;
					break;

				case FileNameChanged.Index:
					fileFullPath = ++rule.Counter + delimiter + fileFullPath;
					break;
			}

			return fileFullPath;
		}

		private void MoveFile(string fileFullPath, Func<string, string> dirPath)
		{
			FileInfo fileInf = new FileInfo(fileFullPath);
			
			if (fileInf.Exists)
			{
				string newPath = string.Concat(dirPath(fileFullPath), @"\", RenameFile(fileInf.Name));

				try
				{
					fileInf.MoveTo(newPath);
					_logger.Log(string.Format(Messages.FileMovedMessage, fileFullPath, newPath, DateTimeOffset.Now));
				}
				catch (IOException ex)
				{
					_logger.Log(string.Format(Messages.FileProcessingMessage, DateTimeOffset.Now));
				}
			}
		}

		#region Event handlers

		private void OnChanged(object source, FileSystemEventArgs e)
		{
			FileInfo fileInfo = new FileInfo(e.FullPath);
			Func<string, string> searchDirByRule = SearchDirByRule;
			if (fileInfo.Exists)
			{
				_logger.Log(string.Format(Messages.ChangedMessage, e.FullPath, fileInfo.CreationTime, e.ChangeType, DateTimeOffset.Now));
				MoveFile(e.FullPath, searchDirByRule);
			}
		}

		private void OnRenamed(object source, RenamedEventArgs e)
		{
			_logger.Log(string.Format(Messages.RenamedMessage, e.OldFullPath, e.FullPath));
		}

		#endregion

		#endregion
	}
}
