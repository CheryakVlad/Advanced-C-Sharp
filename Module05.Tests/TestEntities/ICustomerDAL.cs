using Module05.Attributes;

namespace Module05.Tests.TestEntities
{
	public interface ICustomerDAL
	{
	}

	[Export(typeof(ICustomerDAL))]
	public class CustomerDAL : ICustomerDAL
	{
		public CustomerDAL()
		{
		}
	}
}
