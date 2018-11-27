﻿using LinqToDB.Mapping;

namespace Northwind.Linq2Db.Model.BusinessEntitites
{
	[Table("[dbo].[Products]")]
	public class Product
	{
		[Column("ProductID")]
		[Identity]
		[PrimaryKey]
		public int ProductId
		{
			get; set;
		}

		[Column("ProductName")]
		[NotNull]
		public string ProductName
		{
			get; set;
		}

		[Association(ThisKey = nameof(CategoryId), OtherKey = nameof(BusinessEntitites.Category.CategoryId), CanBeNull = true)]
		public Category Category
		{
			get; set;
		}

		[Column("CategoryID")]
		public int CategoryId
		{
			get; set;
		}

		[Association(ThisKey = nameof(SupplierId), OtherKey = nameof(BusinessEntitites.Supplier.SupplierId), CanBeNull = true)]
		public Supplier Supplier
		{
			get; set;
		}

		[Column("SupplierID")]
		public int SupplierId
		{
			get; set;
		}

		[Column("QuantityPerUnit")]
		public string QuantityPerUnit
		{
			get; set;
		}

		[Column("UnitPrice")]
		public decimal? UnitPrice
		{
			get; set;
		}
	}
}
