using LinqToDB.Mapping;

namespace Northwind.Linq2Db.Model.BusinessEntitites
{
	[Table("[dbo].[Region]")]
	public class Region
	{
		[Column("RegionID")]
		[Identity]
		[PrimaryKey]
		public int RegionId
		{
			get; set;
		}

		[Column("RegionDescription")]
		[NotNull]
		public string RegionDescription
		{
			get; set;
		}
	}
}
