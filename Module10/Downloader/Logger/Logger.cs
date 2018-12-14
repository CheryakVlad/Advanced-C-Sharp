using System;

namespace Downloader.Logger
{
	public class Logger : ILogger
	{
		#region Private Fields

		private readonly bool _isLogEnabled;

		#endregion

		#region Constructors

		public Logger(bool isLogEnabled)
		{
			_isLogEnabled = isLogEnabled;
		}

		#endregion

		#region Implementation ILogger

		public void Log(string message)
		{
			if (_isLogEnabled)
			{
				Console.WriteLine(message);
			}
		}

		#endregion
	}
}
