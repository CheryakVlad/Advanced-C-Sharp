using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cache.Fibonacchi.Cache;
using Cache.Fibonacchi.Redis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NorthwindLibrary;

namespace CachingSolutionsSamples
{
	[TestClass]
	public class EntitiesManagerTests
	{
		#region Constants

		private const string CATEGORIES_PREFIX = "Categories";
		private const string ORDERS_PREFIX = "Orders";
		private const string EMPLOYEES_PREFIX = "Employees";

		#endregion

		#region Tests

		[TestMethod]
		public void Test_should_check_count_of_entities_from_runtime_cache_from_Categories_table()
		{
			var entitiesManager = new EntityManager<Category>(new Cache<IEnumerable<Category>>((CATEGORIES_PREFIX)));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(entitiesManager.GetEntities().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void Test_should_check_count_of_entities_from_Redis_cache_from_Categories_table()
		{
			var entitiesManager = new EntityManager<Category>(new RedisCache<IEnumerable<Category>>("localhost", CATEGORIES_PREFIX));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(entitiesManager.GetEntities().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void Test_should_check_count_of_entities_from_runtime_cache_from_Orders_table()
		{
			var entitiesManager = new EntityManager<Order>(new Cache<IEnumerable<Order>>(ORDERS_PREFIX));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(entitiesManager.GetEntities().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void Test_should_check_count_of_entities_from_Redis_cache_from_Orders_table()
		{
			var entitiesManager = new EntityManager<Order>(new RedisCache<IEnumerable<Order>>("localhost", ORDERS_PREFIX));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(entitiesManager.GetEntities().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void Test_should_check_count_of_entities_from_runtime_cache_from_Employees_table()
		{
			var entitiesManager = new EntityManager<Employee>(new Cache<IEnumerable<Employee>>(EMPLOYEES_PREFIX));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(entitiesManager.GetEntities().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void Test_should_check_count_of_entities_from_Redis_cache_from_Employees_table()
		{
			var entitiesManager = new EntityManager<Employee>(new RedisCache<IEnumerable<Employee>>("localhost", EMPLOYEES_PREFIX));

			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(entitiesManager.GetEntities().Count());
				Thread.Sleep(100);
			}
		}

		[TestMethod]
		public void Test_should_check_runtime_cache_with_SqlMonitors()
		{
			var entitiesManager = new EntitiesMemoryCache<Supplier>(new Cache<IEnumerable<Supplier>>(EMPLOYEES_PREFIX),
				"SELECT SupplierID, CompanyName, ContactName, ContactTitle FROM [dbo].[Suppliers]");
			for (var i = 0; i < 10; i++)
			{
				Console.WriteLine(entitiesManager.GetEntities().Count());
				Thread.Sleep(1000);
			}
		}

		#endregion 
	}
}
