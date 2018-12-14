using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using ClosedXML.Excel;
using HttpHandler.Northwind;

namespace HttpHandler.Handlers
{
	public class OrderReportHandler : IHttpHandler
	{
		#region Constants

		private const string CUSTOMER = "customer";
		private const string CUSTOMER_ID = "customerID";
		private const string DATE_TO = "dateTo";
		private const string DATE_FROM = "dateFrom";
		private const string TAKE = "take";
		private const string SKIP = "skip";
		private const string ORDERS = "orders";
		private const string ORDER = "order";
		private const string SHIP_NAME = "shipName";
		private const string SHIP_COUNTRY = "shipCountry";

		#endregion

		#region IHttpHandler elements

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}

		public void ProcessRequest(HttpContext context)
		{
			string customerID = Convert.ToString(context.Request.Params.Get(CUSTOMER));
			DateTime dateTo = Convert.ToDateTime(context.Request.Params.Get(DATE_TO));
			DateTime dateFrom = Convert.ToDateTime(context.Request.Params.Get(DATE_FROM));
			int take = Convert.ToInt32(context.Request.Params.Get(TAKE));
			int skip = Convert.ToInt32(context.Request.Params.Get(SKIP));

			using (var db = new Northwind.Northwind())
			{
				List<Order> orders = db.Orders.ToList();

				if (!string.IsNullOrWhiteSpace(customerID))
				{
					orders = orders.Where(x => x.CustomerID == customerID).ToList();
				}
				if (dateFrom != DateTime.MinValue)
				{
					orders = orders.Where(x => x.RequiredDate >= dateFrom).ToList();
				}
				if (dateTo != DateTime.MinValue)
				{
					orders = orders.Where(x => x.RequiredDate <= dateTo).ToList();
				}
				if (skip != 0)
				{
					orders = orders.Skip(skip).ToList();
				}
				if (take != 0)
				{
					orders = orders.Take(take).ToList();
				}

				if (context.Request.Headers.Get(1).Contains("xml"))
				{
					ShowXmlFile(orders.ToList(), context);
				}
				else
				{
					ShowExcelFile(orders.ToList(), context);
				}
			}
		}

		#endregion

		#region Internal implementation

		private void ShowXmlFile(List<Order> orders, HttpContext context)
		{
			XDocument xdoc = new XDocument();
			XElement ordersXML = new XElement(ORDERS);

			foreach (var order in orders)
			{
				XElement orderElement = new XElement(ORDER);
				
				XAttribute customerIDAttr = new XAttribute(CUSTOMER_ID, order.CustomerID);
				XElement shipName = new XElement(SHIP_NAME, order.ShipName);
				XElement shipCountry = new XElement(SHIP_COUNTRY, order.ShipCountry);
				
				orderElement.Add(customerIDAttr);
				orderElement.Add(shipName);
				orderElement.Add(shipCountry);

				ordersXML.Add(orderElement);
			}

			xdoc.Add(ordersXML);

			string myName = context.Server.UrlEncode("Test" + "_" + DateTime.Now.ToShortDateString() + ".xml");
			MemoryStream stream = GetStream(xdoc);
			context.Response.Clear();
			context.Response.Buffer = true;
			context.Response.AddHeader("content-disposition", "attachment; filename=" + myName);
			context.Response.ContentType = "text/xml";
			context.Response.BinaryWrite(stream.ToArray());
			context.Response.End();
		}

		private void ShowExcelFile(List<Order> orders, HttpContext context)
		{
			int i = 2;
			
			using (XLWorkbook workbook = new XLWorkbook())
			{
				var worksheet = workbook.Worksheets.Add("Sheet1");

				worksheet.Cell("A" + 1).Value = CUSTOMER_ID;
				worksheet.Cell("B" + 1).Value = SHIP_NAME;
				worksheet.Cell("C" + 1).Value = SHIP_COUNTRY;

				foreach (Order order in orders)
				{
					worksheet.Cell("A" + i).Value = order.CustomerID;
					worksheet.Cell("B" + i).Value = order.ShipName;
					worksheet.Cell("C" + i).Value = order.ShipCountry;
					i++;
				}

				string myName = context.Server.UrlEncode("Test" + "_" + DateTime.Now.ToShortDateString() + ".xlsx");
				MemoryStream stream = GetStream(workbook);
				context.Response.Clear();
				context.Response.Buffer = true;
				context.Response.AddHeader("content-disposition", "attachment; filename=" + myName);
				context.Response.ContentType = "application/vnd.ms-excel";
				context.Response.BinaryWrite(stream.ToArray());
				context.Response.End();
			}
		}

		private MemoryStream GetStream(XLWorkbook excelWorkbook)
		{
			MemoryStream fs = new MemoryStream();
			excelWorkbook.SaveAs(fs);
			fs.Position = 0;
			return fs;
		}

		private MemoryStream GetStream(XDocument xdoc)
		{
			MemoryStream fs = new MemoryStream();
			xdoc.Save(fs);
			fs.Position = 0;
			return fs;
		}

		#endregion
	}
}