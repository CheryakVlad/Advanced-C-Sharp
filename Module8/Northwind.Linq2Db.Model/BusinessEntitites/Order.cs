using System;
using LinqToDB.Mapping;

namespace Northwind.Linq2Db.Model.BusinessEntitites
{
	[Table("[dbo].[Orders]")]
	public class Order
	{
		[Column("OrderID")]
		[Identity]
		[PrimaryKey]
		public int OrderId
		{
			get; set;
		}

		[Column("ShipVia")]
		public int? ShipperId
		{
			get; set;
		}

		[Column("ShippedDate")]
		public DateTime? ShippedDate
		{
			get; set;
		}

		[Column("EmployeeID")]
		public int? EmployeeId
		{
			get; set;
		}

		[Association(ThisKey = nameof(EmployeeId), OtherKey = nameof(BusinessEntitites.Employee.EmployeeId), CanBeNull = true)]
		public Employee Employee
		{
			get; set;
		}

		[Association(ThisKey = nameof(ShipperId), OtherKey = nameof(BusinessEntitites.Shipper.ShipperId), CanBeNull = true)]
		public Shipper Shipper
		{
			get; set;
		}

	}
}
