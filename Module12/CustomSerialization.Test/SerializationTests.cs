using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using CustomSerialization.DB;
using CustomSerialization.Surrogates;
using CustomSerialization.TestHelper;
using Xunit;

namespace CustomSerialization.Test
{
	public class SerializationTests
	{
		#region Private fields

		Northwind _dbContext;

		#endregion

		#region Test initialize

		public SerializationTests()
		{
			_dbContext = new Northwind();
		}

		#endregion

		#region Tests

		[Fact]
		public void SerializationCallbacks()
		{
			#region ARRANGE

			_dbContext.Configuration.ProxyCreationEnabled = false;

			#endregion

			#region ACT

			var tester = new XmlDataContractSerializerTester<IEnumerable<Category>>(new NetDataContractSerializer(), true);
			var categories = _dbContext.Categories.ToList();

			var c = categories.First();

			#endregion

			#region ASSERT

			tester.SerializeAndDeserialize(categories);

			#endregion
		}

		[Fact]
		public void ISerializable()
		{
			#region ARRANGE

			_dbContext.Configuration.ProxyCreationEnabled = false;

			#endregion

			#region ACT

			var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(new NetDataContractSerializer(), true);
			var products = _dbContext.Products.ToList();

			#endregion

			#region ASSERT

			tester.SerializeAndDeserialize(products);

			#endregion
		}


		[Fact]
		public void ISerializationSurrogate()
		{
			#region ARRANGE

			_dbContext.Configuration.ProxyCreationEnabled = false;

			#endregion

			#region ACT

			var selector = new SurrogateSelector();
			selector.AddSurrogate(typeof(Order_Detail),
				new StreamingContext(StreamingContextStates.All, _dbContext),
				new OrderDetailSerializationSurrogate());

			var tester = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(
				new NetDataContractSerializer
				{
					SurrogateSelector = selector,
					Context = new StreamingContext(StreamingContextStates.All, _dbContext)
				},
				true);
			var orderDetails = _dbContext.Order_Details.ToList();

			#endregion

			#region ASSERT

			tester.SerializeAndDeserialize(orderDetails);

			#endregion
		}

		[Fact]
		public void IDataContractSurrogate()
		{
			#region ARRANGE

			_dbContext.Configuration.ProxyCreationEnabled = true;
			_dbContext.Configuration.LazyLoadingEnabled = true;

			#endregion

			#region ACT

			var settings = new DataContractSerializerSettings
			{
				DataContractSurrogate = new OrderDataContractSurrogate()
			};
			var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(
				new DataContractSerializer(
					typeof(IEnumerable<Order>),
					settings),
				true);
			var orders = _dbContext.Orders.ToList();

			#endregion

			#region ASSERT

			tester.SerializeAndDeserialize(orders);

			#endregion
		}

		#endregion

	}
}
