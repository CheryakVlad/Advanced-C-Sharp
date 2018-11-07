using Module05.Attributes;

namespace Module05.Tests.TestEntities
{
	[ImportConstructor]
	public class CustomerBLL
	{
		public CustomerBLL(ICustomerDAL dal, Logger logger)
		{
		}
	}

	public class CustomerBLLNew
	{
		[Import]
		public ICustomerDAL CustomerDAL
		{
			get; set;
		}
		[Import]
		public Logger Logger
		{
			get; set;
		}
	}
}
