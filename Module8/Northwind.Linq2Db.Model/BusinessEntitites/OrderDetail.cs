using LinqToDB.Mapping;

namespace Northwind.Linq2Db.Model.BusinessEntitites
{
	[Table("[dbo].[Order Details]")]
	public class OrderDetail
	{
		[Column("OrderID")]
		[PrimaryKey]
		public int OrderId
		{
			get; set;
		}

		[Column("ProductID")]
		[PrimaryKey]
		public int ProductId
		{
			get; set;
		}

		[Column("UnitPrice")]
		[NotNull]
		public decimal? UnitPrice
		{
			get; set;
		}

		[Association(ThisKey = nameof(ProductId), OtherKey = nameof(BusinessEntitites.Product.ProductId))]
		public Product Product
		{
			get; set;
		}

		[Association(ThisKey = nameof(OrderId), OtherKey = nameof(BusinessEntitites.Order.OrderId))]
		public Order Order
		{
			get; set;
		}
	}
}
