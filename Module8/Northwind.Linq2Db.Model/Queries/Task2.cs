using System;
using System.Linq;
using LinqToDB;
using ORM.Shared.Constants;

namespace Northwind.Linq2Db.Model.Queries
{
	public class Task2
	{
		public static void ListOfProductsWithCategoryAndSuppliers()
		{
			using (var db = new NorthwindConnection())
			{
				foreach (var product in db.Products.LoadWith(p => p.Category).LoadWith(p => p.Supplier))
				{
					Console.WriteLine(string.Concat(ConstantsHelper.PRODUCT_NAME, ConstantsHelper.DELIMITER) + product.ProductName + " " 
						+ string.Concat(ConstantsHelper.CATEGORY, ConstantsHelper.DELIMITER) + product.Category.CategoryName + " " 
						+ string.Concat(ConstantsHelper.SUPPLIER, ConstantsHelper.DELIMITER) + product.Supplier.CompanyName);
				}
			}
		}

		public static void ListOfEmployeesWithRegion()
		{
			using (var db = new NorthwindConnection())
			{
				foreach (var employee in db.Employees.Join(db.EmployeeTerritories, e => e.EmployeeId, et => et.EmployeeId,
					(e, et) => new { Name = e.LastName + " " + e.FirstName, TerritoryID = et.TerritoryId })
					.Join(db.Territories, e => e.TerritoryID, t => t.TerritoryId,
					(e, t) => new { Name = e.Name, RegionID = t.RegionId })
					.Join(db.Regions, e => e.RegionID, r => r.RegionId,
					(e, r) => new { Name = e.Name, Region = r.RegionDescription }))
				{
					Console.WriteLine(string.Concat(ConstantsHelper.EMPLOYEE, ConstantsHelper.DELIMITER) + employee.Name + " "
						+ string.Concat(ConstantsHelper.REGION, ConstantsHelper.DELIMITER) + employee.Region);
				}
			}
		}

		public static void CountOfEmployeesByRegion()
		{
			using (var db = new NorthwindConnection())
			{
				var employees = db.Employees.Join(db.EmployeeTerritories, e => e.EmployeeId, et => et.EmployeeId,
					(e, et) => new { Name = e.LastName + " " + e.FirstName, TerritoryID = et.TerritoryId })
					.Join(db.Territories, e => e.TerritoryID, t => t.TerritoryId,
					(e, t) => new { Name = e.Name, RegionID = t.RegionId });

				foreach (var region in db.Regions.GroupJoin(employees, r => r.RegionId, e => e.RegionID,
					(r, er) => new { Region = r.RegionDescription, CountOfEmployees = er.Count() }))
				{
					Console.WriteLine(string.Concat(ConstantsHelper.REGION, ConstantsHelper.DELIMITER) + region.Region + " " +
						string.Concat(ConstantsHelper.COUNT_OF_EMPLOYEES, ConstantsHelper.DELIMITER) + region.CountOfEmployees);
				}
			}
		}

		public static void ListOfEmployeesAndShippers()
		{
			using (var db = new NorthwindConnection())
			{
				foreach (var orders in db.Orders.Select(o => new {
					EmployeeName = o.Employee.LastName + ", " + o.Employee.LastName,
					ShipperName = o.Shipper.CompanyName
				}).Distinct().GroupBy(o => o.EmployeeName))
				{
					foreach (var order in orders)
					{
						Console.WriteLine(string.Concat(ConstantsHelper.EMPLOYEE, ConstantsHelper.DELIMITER) + orders.Key + " "
							+ string.Concat(ConstantsHelper.SHIPPER_NAME, ConstantsHelper.DELIMITER) + order.ShipperName);
					}
				}
			}
		}
	}
}
