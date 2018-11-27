using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB;
using Northwind.Linq2Db.Model.BusinessEntitites;

namespace Northwind.Linq2Db.Model.Queries
{
	public class Task3
	{
		public static void AddNewEmployeeWithTerritories()
		{
			using (var db = new NorthwindConnection())
			{
				var id = Convert.ToInt32(db.InsertWithIdentity(
					new Employee { FirstName = "Alex", LastName = "Moralez" }));

				var territories = db.Territories.Select(x => x.TerritoryId).Skip(20).Take(2).ToArray();
				db.Insert(new EmployeeTerritory
				{
					EmployeeId = id,
					TerritoryId = territories[0]
				});
				db.Insert(new EmployeeTerritory
				{
					EmployeeId = id,
					TerritoryId = territories[1]
				});
			}
		}

		public static void MoveProductsToAnotherCategory()
		{
			using (var db = new NorthwindConnection())
			{
				var category = db.Categories.First();
				var product = db.Products.First(
					p => p.CategoryId != category.CategoryId);

				product.CategoryId = category.CategoryId;

				db.Update(product);
			}
		}

		public static void AddProducts()
		{
			using (var db = new NorthwindConnection())
			{
				var products = new List<Product>
				{
					new Product
					{
						ProductName = "SomeProduct",
						Category = new Category { CategoryName = "SomeCategory" },
						Supplier = new Supplier { CompanyName = "SomeSupplier" }
					},
					new Product
					{
						ProductName = "AnotherProduct",
						Category = new Category { CategoryName = "AnotherCategory" },
						Supplier = new Supplier { CompanyName = "AnotherSupplier" }
					}
				};

				foreach (var p in products)
				{
					if (db.Categories.Any(c => c.CategoryName == p.Category.CategoryName))
					{
						p.CategoryId = db.Categories.
							First(c => c.CategoryName == p.Category.CategoryName).CategoryId;
					}
					else
					{
						p.CategoryId = Convert.ToInt32(
							db.InsertWithIdentity(
								new Category { CategoryName = p.Category.CategoryName }));
					}

					if (db.Suppliers.Any(s => s.CompanyName == p.Supplier.CompanyName))
					{
						p.SupplierId = db.Suppliers.
							First(s => s.CompanyName == p.Supplier.CompanyName).SupplierId;
					}
					else
					{
						p.SupplierId = Convert.ToInt32(
							db.InsertWithIdentity(
								new Supplier { CompanyName = p.Supplier.CompanyName }));
					}

					db.Insert(p);
				}
			}
		}

		public static void ReplaceProductWithAnalog()
		{
			using (var db = new NorthwindConnection())
			{
				var updatedRows = db.OrderDetails.LoadWith(od => od.Order).LoadWith(od => od.Product)
				.Where(od => od.Order.ShippedDate == null).Update(
					od => new OrderDetail
					{
						ProductId = db.Products.First(p => p.CategoryId == od.Product.CategoryId && p.ProductId > od.ProductId) != null
							? db.Products.First(p => p.CategoryId == od.Product.CategoryId && p.ProductId > od.ProductId).ProductId
							: db.Products.First(p => p.CategoryId == od.Product.CategoryId).ProductId
					});
				Console.WriteLine($"{updatedRows} rows updated");
			}
		}

		private static int FindAnalog(NorthwindConnection db, OrderDetail notShippedOrder)
		{
			var analogProduct = notShippedOrder.ProductId++;
			if (!db.Products.Any(p => p.ProductId == analogProduct))
			{
				analogProduct = notShippedOrder.ProductId--;
			}
			return analogProduct;
		}
	}
}
