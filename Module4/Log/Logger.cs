using System;

namespace Module4.Log
{
	public class Logger : ILogger
	{
		public void Log(string message)
		{
			Console.WriteLine(message);
		}
	}
}
