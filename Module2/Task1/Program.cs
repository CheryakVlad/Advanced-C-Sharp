using System;

namespace Task1
{
	class Program
	{
		static void Main(string[] args)
		{
			string enterString = string.Empty;

			do
			{
				Console.WriteLine("Enter string: ");
				enterString = Console.ReadLine();

				try
				{
					Console.WriteLine($"The first symbol: {enterString[0]}");
				}
				catch (IndexOutOfRangeException)
				{
					Console.WriteLine("You entered empty string!");
				}

			} while (true);

		}
	}
}
