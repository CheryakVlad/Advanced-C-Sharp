// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{

		private DataSource dataSource = new DataSource();

		[Category("Restriction Operators")]
		[Title("Where - Task 1")]
		[Description("This sample uses the where clause to find all elements of an array with a value less than 5.")]
		public void Linq1()
		{
			int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

			var lowNums =
				from num in numbers
				where num < 5
				select num;

			Console.WriteLine("Numbers < 5:");
			foreach (var x in lowNums)
			{
				Console.WriteLine(x);
			}
		}

		[Category("Restriction Operators")]
		[Title("Where - Task 2")]
		[Description("This sample return return all presented in market products")]

		public void Linq2()
		{
			var products =
				from p in dataSource.Products
				where p.UnitsInStock > 0
				select p;

			foreach (var p in products)
			{
				ObjectDumper.Write(p);
			}
		}

		private void WriteResult(IEnumerable result)
		{
			if (result == null)
			{
				ObjectDumper.Write("Collection is empty!");
				return;
			}

			foreach (var value in result)
			{
				ObjectDumper.Write(value);
			}
		}

		[Category(".NET Mentoring")]
		[Title("LINQ. Task 01")]
		[Description("List of customers with annual turnover more than some value.")]
		public void Linq01()
		{
			IEnumerable<string> GetCustomersWithTotalTurnoverMoreThanX(
				int x, IEnumerable<Customer> customers) =>
				customers.
					Where(customer => customer.Orders.Sum(o => o.Total) > x).
					Select(c => c.CompanyName);

			var productsWithTotalTurnoverMoreThan10000 =
				GetCustomersWithTotalTurnoverMoreThanX(10000, dataSource.Customers);
			var productsWithTotalTurnoverMoreThan5000 =
				GetCustomersWithTotalTurnoverMoreThanX(5000, dataSource.Customers);

			ObjectDumper.Write("Products With Annual Turnover More Than 10000:");
			WriteResult(productsWithTotalTurnoverMoreThan10000);
			ObjectDumper.Write("Products With Annual Turnover More Than 5000:");
			WriteResult(productsWithTotalTurnoverMoreThan5000);
		}

		[Category(".NET Mentoring")]
		[Title("LINQ. Task 02")]
		[Description("List of customers with corresponding suppliers located in the same country and same city")]
		public void Linq02()
		{
			var customers = dataSource.Customers.
				Select(customer => new {
					customer.CompanyName,
					customer.Country,
					customer.City,
					SupplierGroup = dataSource.Suppliers.
						Where(supplier => supplier.Country == customer.Country 
						&& supplier.City == customer.City)
				});
			
			WriteResult(customers);
		}

		[Category(".NET Mentoring")]
		[Title("LINQ. Task 03")]
		[Description("List of customers with total sum of orders more than some X")]
		public void Linq03()
		{
			var customers = dataSource.Customers.
				Where(customer => customer.Orders.Sum(order => order.Total) > 1000);

			ObjectDumper.Write("Customers with total sum of orders more than some 1000:");
			WriteResult(customers);
		}

		[Category(".NET Mentoring")]
		[Title("LINQ. Task 04")]
		[Description("List of customers with the indication of the beginning of which month of which year they became customers")]
		public void Linq04()
		{
			var result = dataSource.Customers.
				Select(customer => new {
					customer.CompanyName,
					DateOfOrdering =
						customer.Orders.Any() ?
						customer.Orders.Min(order => order.OrderDate) :
						default(DateTime)
				}).
				Select(c => new {
					c.CompanyName,
					YearOfFounding = c.DateOfOrdering.Year,
					MonthOfFounding = c.DateOfOrdering.Month
				});

			WriteResult(result);
		}

		[Category(".NET Mentoring")]
		[Title("LINQ. Task 05")]
		[Description("List of customers with the indication of the beginning of which month of which year they became customers with sorting")]
		public void Linq05()
		{
			var result = dataSource.Customers.
				Select(customer => new {
					customer,
					DateOfOrdering =
						customer.Orders.Any() ?
						customer.Orders.Min(order => order.OrderDate) :
						default(DateTime)
				}).
				Select(c => new {
					c.customer.CompanyName,
					YearOfFounding = c.DateOfOrdering.Year,
					MonthOfFounding = c.DateOfOrdering.Month,
					AnnualTurnover =
						c.customer.Orders.Sum(order => order.Total)
				});

			result = result.OrderBy(r => r.YearOfFounding)
				.ThenBy(r => r.MonthOfFounding)
				.ThenByDescending(r => r.AnnualTurnover)
				.ThenBy(r => r.CompanyName);

			WriteResult(result);
		}

		[Category(".NET Mentoring")]
		[Title("LINQ. Task 06")]
		[Description("List of customers without digital postal code or region or phone code")]
		public void Linq06()
		{
			string regexExpression = "^[0-9]+$";

			var result = dataSource.Customers.Where(c => string.IsNullOrWhiteSpace(c.Region) ||
			(c.Phone.IndexOf("(") != 0 && c.Phone.Contains("(")) ||
			string.IsNullOrWhiteSpace(c.PostalCode) ||
			Regex.IsMatch(c.PostalCode, regexExpression, RegexOptions.Compiled));

			WriteResult(result);
		}

		[Category(".NET Mentoring")]
		[Title("LINQ. Task 07")]
		[Description("Group product by category, unit in stock and price")]
		public void Linq07()
		{
			var result = dataSource.Products.
				GroupBy(product => product.Category).
				Select(categoryGrouping => new {
					Name = categoryGrouping.Key,
					Count = categoryGrouping.Count(),
					Products =
						categoryGrouping.
						GroupBy(p => p.UnitsInStock).
						Select(unitInStockGrouping => new {
							Name = unitInStockGrouping.Key,
							Count = unitInStockGrouping.Count(),
							Products = unitInStockGrouping.
								OrderBy(p => p.UnitPrice)
						})
				});
			
			WriteResult(result);
		}

		[Category(".NET Mentoring")]
		[Title("LINQ. Task 08")]
		[Description("Group product by price range")]
		public void Linq08()
		{
			var max = dataSource.Products.Max(p => p.UnitPrice);

			var ranges = new[]
			{
				new { Start = 0M , End = 10M},
				new { Start = 10M , End = 40M},
				new { Start = 40M , End = max}
			};

			var products = ranges
				.Select(range => new {
					Range = range,
					Values = dataSource.Products.
						Where(product =>
							product.UnitPrice > range.Start
							&& product.UnitPrice <= range.End)
				}).
				Select(range => new {
					range.Range.Start,
					range.Range.End,
					Count = range.Values.Count(),
					range.Values
				});
			
			WriteResult(products);
		}

		[Category(".NET Mentoring")]
		[Title("LINQ. Task 09")]
		[Description("Average total for cities and average amount of orders for customers")]
		public void Linq09()
		{
			var cities = dataSource.Customers.
				GroupBy(customer => customer.City).
				Select(cityGroup => new {
					cityGroup.Key,
					AverageTotal = cityGroup.
						Select(customer => customer.Orders).
						SelectMany(orders => orders).
						Select(order => order.Total).Average(),
					AverageOrdersAmount = cityGroup.
						Select(customer => customer.Orders.Length).Average()
				});

			WriteResult(cities);
		}

		[Category(".NET Mentoring")]
		[Title("LINQ. Task 10")]
		[Description("Statistics that mean annual customers activity at yearly, monthly or yearly and monthly")]
		public void Linq10()
		{
			var groupbyYear = dataSource.Customers.
				SelectMany(customer => customer.Orders).
				GroupBy(order => order.OrderDate.Year);

			var groupbyMonth = dataSource.Customers.
				SelectMany(customer => customer.Orders).
				GroupBy(order => order.OrderDate.Month);

			var groupbyMonthAndYear = dataSource.Customers.
				SelectMany(customer => customer.Orders).
				GroupBy(order => new {
					order.OrderDate.Year,
					order.OrderDate.Month
				});
			
			ObjectDumper.Write("Group by Year:");
			WriteResult(groupbyYear);
			ObjectDumper.Write("Group by Month:");
			WriteResult(groupbyMonth);
			ObjectDumper.Write("Group by Month & Year:");
			WriteResult(groupbyMonthAndYear);
		}

	}
}
