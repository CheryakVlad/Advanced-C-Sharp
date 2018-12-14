
namespace Module05.Exception
{
	public class RegisteredException : System.Exception
	{
		public RegisteredException()
		{

		}

		public RegisteredException(string message) : base(message)
		{

		}

		public RegisteredException(string message, System.Exception exception) : base(message, exception)
		{

		}
	}
}
