using System;
using Northwind.Linq2Db.Model.Queries;

namespace Northwind.Linq2Db.ConsoleTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Task2:");
			Console.WriteLine("List Of Products With Category And Suppliers:");
			Task2.ListOfProductsWithCategoryAndSuppliers();

			Console.WriteLine();
			Console.WriteLine("List Of Employees With Region:");
			Task2.ListOfEmployeesWithRegion();

			Console.WriteLine();
			Console.WriteLine("Count Of Employees By Region:");
			Task2.CountOfEmployeesByRegion();

			Console.WriteLine();
			Console.WriteLine("List Of Employees And Shippers:");
			Task2.ListOfEmployeesAndShippers();

			Console.WriteLine("Task3:");
			Console.WriteLine("Add New Employee With Territories");
			Task3.AddNewEmployeeWithTerritories();

			Console.WriteLine();
			Console.WriteLine("Move Products To Another Category");
			Task3.MoveProductsToAnotherCategory();

			Console.WriteLine();
			Console.WriteLine("Add Products");
			Task3.AddProducts();

			Console.WriteLine();
			Console.WriteLine("Replace Product With Analog");
			Task3.ReplaceProductWithAnalog();

			Console.ReadKey();
		}
	}
}
