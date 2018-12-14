using System.Reflection;
using Module05.Tests.TestEntities;
using Xunit;

namespace Module05.Tests
{
	public class ContainerTests
	{
		#region Test context

		private Container _container;

		public ContainerTests()
		{
			_container = new Container();
			_container.AddAssembly(Assembly.GetExecutingAssembly());
		}

		#endregion

		#region Tests

		[Fact]
		public void Test_GenericCreateInstance_AssemblyAttributes_ConstructorInjection()
		{
			var customerBll = _container.CreateInstance<CustomerBLL>();

			Assert.NotNull(customerBll);
			Assert.True(customerBll.GetType() == typeof(CustomerBLL));
		}

		[Fact]
		public void Test_CreateInstance_AssemblyAttributes_ConstructorInjection()
		{
			var customerBll = _container.CreateInstance<CustomerBLL>();

			Assert.NotNull(customerBll);
			Assert.True(customerBll.GetType() == typeof(CustomerBLL));
		}

		[Fact]
		public void Test_CreateInstance_ExplicitSet_ConstructorInjection()
		{
			_container.AddType(typeof(CustomerBLL));
			_container.AddType(typeof(Logger));
			_container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

			var customerBll = (CustomerBLL)_container.CreateInstance(typeof(CustomerBLL));

			Assert.NotNull(customerBll);
			Assert.True(customerBll.GetType() == typeof(CustomerBLL));
		}

		[Fact]
		public void Test_GenericCreateInstance_ExplicitSet_ConstructorInjection()
		{
			_container.AddType(typeof(CustomerBLL));
			_container.AddType(typeof(Logger));
			_container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

			var customerBll = _container.CreateInstance<CustomerBLL>();

			Assert.NotNull(customerBll);
			Assert.True(customerBll.GetType() == typeof(CustomerBLL));
		}

		[Fact]
		public void Test_GenericCreateInstance_AssemblyAttributes_PropertiesInjection()
		{
			var customerBll = _container.CreateInstance<CustomerBLLNew>();

			Assert.NotNull(customerBll);
			Assert.True(customerBll.GetType() == typeof(CustomerBLLNew));
			Assert.NotNull(customerBll.CustomerDAL);
			Assert.True(customerBll.CustomerDAL.GetType() == typeof(CustomerDAL));
			Assert.NotNull(customerBll.Logger);
			Assert.True(customerBll.Logger.GetType() == typeof(Logger));
		}

		[Fact]
		public void Test_CreateInstance_AssemblyAttributes_PropertiesInjection()
		{
			var customerBll = _container.CreateInstance<CustomerBLLNew>();

			Assert.NotNull(customerBll);
			Assert.True(customerBll.GetType() == typeof(CustomerBLLNew));
			Assert.NotNull(customerBll.CustomerDAL);
			Assert.True(customerBll.CustomerDAL.GetType() == typeof(CustomerDAL));
			Assert.NotNull(customerBll.Logger);
			Assert.True(customerBll.Logger.GetType() == typeof(Logger));
		}

		[Fact]
		public void Test_CreateInstance_ExplicitSet_PropertiesInjection()
		{
			_container.AddType(typeof(CustomerBLLNew));
			_container.AddType(typeof(Logger));
			_container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

			var customerBll = (CustomerBLLNew)_container.CreateInstance(typeof(CustomerBLLNew));

			Assert.NotNull(customerBll);
			Assert.True(customerBll.GetType() == typeof(CustomerBLLNew));
			Assert.NotNull(customerBll.CustomerDAL);
			Assert.True(customerBll.CustomerDAL.GetType() == typeof(CustomerDAL));
			Assert.NotNull(customerBll.Logger);
			Assert.True(customerBll.Logger.GetType() == typeof(Logger));
		}

		[Fact]
		public void Test_GenericCreateInstance_ExplicitSet_PropertiesInjection()
		{
			_container.AddType(typeof(CustomerBLLNew));
			_container.AddType(typeof(Logger));
			_container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));

			var customerBll = _container.CreateInstance<CustomerBLLNew>();

			Assert.NotNull(customerBll);
			Assert.True(customerBll.GetType() == typeof(CustomerBLLNew));
			Assert.NotNull(customerBll.CustomerDAL);
			Assert.True(customerBll.CustomerDAL.GetType() == typeof(CustomerDAL));
			Assert.NotNull(customerBll.Logger);
			Assert.True(customerBll.Logger.GetType() == typeof(Logger));
		}

		#endregion
	}
}

