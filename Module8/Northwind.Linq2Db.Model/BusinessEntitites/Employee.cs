using LinqToDB.Mapping;

namespace Northwind.Linq2Db.Model.BusinessEntitites
{
	[Table("[dbo].[Employees]")]
	public class Employee
	{
		[Column("EmployeeID")]
		[PrimaryKey]
		[Identity]
		public int EmployeeId
		{
			get; set;
		}

		[Column("LastName")]
		public string LastName
		{
			get; set;
		}

		[Column("FirstName")]
		public string FirstName
		{
			get; set;
		}
	}
}
