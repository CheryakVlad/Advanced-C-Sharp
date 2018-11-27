using LinqToDB;
using LinqToDB.Data;
using Northwind.Linq2Db.Model.BusinessEntitites;

namespace Northwind.Linq2Db.Model
{
	public class NorthwindConnection : DataConnection
	{
		#region Constants

		private const string NORTHWIND_DB = "Northwind";

		#endregion

		#region Constructors

		public NorthwindConnection() : base(NORTHWIND_DB)
		{
		}

		public NorthwindConnection(string configurationString) : base(configurationString)
		{
		}

		public NorthwindConnection(string providerName, string connectionString) : base(providerName, connectionString)
		{
		}

		#endregion

		#region Properties

		public ITable<Category> Categories => GetTable<Category>();
		public ITable<Product> Products => GetTable<Product>();
		public ITable<Supplier> Suppliers => GetTable<Supplier>();
		public ITable<Region> Regions => GetTable<Region>();
		public ITable<Territory> Territories => GetTable<Territory>();
		public ITable<EmployeeTerritory> EmployeeTerritories => GetTable<EmployeeTerritory>();
		public ITable<Employee> Employees => GetTable<Employee>();
		public ITable<Shipper> Shippers => GetTable<Shipper>();
		public ITable<Order> Orders => GetTable<Order>();
		public ITable<OrderDetail> OrderDetails => GetTable<OrderDetail>();

		#endregion
	}
}
