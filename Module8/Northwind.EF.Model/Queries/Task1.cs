using System;
using System.Linq;
using ORM.Shared.Constants;

namespace Northwind.EntityFramework.Model.Queries
{
	public class Task1
	{
		public static void ListOfOrdersWithProductCategory()
		{
			using (var context = new Northwind())
			{
				foreach (var category in context.Categories)
				{
					foreach (var product in category.Products)
					{
						var orderDetails = context.Order_Details.Where(o => o.ProductID == product.ProductID);
						foreach (var od in orderDetails)
						{
							var productOrders = context.Orders.Where(o => o.OrderID == od.OrderID);

							foreach (var categoryOrder in productOrders)
							{
								Console.WriteLine(od.Product.Category.CategoryName);
								Console.WriteLine(string.Concat(ConstantsHelper.PRODUCT_NAME, ConstantsHelper.DELIMITER) + od.Product.ProductName);
								Console.WriteLine(string.Concat(ConstantsHelper.COMPANY_NAME, ConstantsHelper.DELIMITER) + categoryOrder.Customer.CompanyName);
								Console.WriteLine();
							}
						}
					}
				}
			}
		}
	}
}
