using System;
using Northwind.EntityFramework.Model.Queries;

namespace Northwind.EF.ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine();
			Console.WriteLine("List Of Orders With Product Category");
			Task1.ListOfOrdersWithProductCategory();

			Console.ReadKey();
		}
	}
}
