
using Module4.Resources;
using System;

namespace Module4
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e) {
				e.Cancel = true;
			};

			Watcher watcher = new Watcher();
			watcher.Run();

			Console.WriteLine(Messages.ExitMessage);
			Console.ReadLine();
		}
	}
}
