using LinqToDB.Mapping;


namespace Northwind.Linq2Db.Model.BusinessEntitites
{
	[Table("[dbo].[EmployeeTerritories]")]
	public class EmployeeTerritory
	{
		[Column("EmployeeID")]
		[NotNull]
		public int EmployeeId
		{
			get; set;
		}

		[Column("TerritoryID")]
		[NotNull]
		public int TerritoryId
		{
			get; set;
		}

		[Association(ThisKey = nameof(EmployeeId), OtherKey = nameof(BusinessEntitites.Employee.EmployeeId))]
		public Employee Employee
		{
			get; set;
		}

		[Association(ThisKey = nameof(TerritoryId), OtherKey = nameof(BusinessEntitites.Territory.TerritoryId))]
		public Territory Territory
		{
			get; set;
		}
	}
}
