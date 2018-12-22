using BasicSerialization.BusinessEntities;
using Xunit;

namespace BasicSerialization.Tests
{
	public class BookSerializerTests
	{
		#region Private fields

		BookSerializer serializer;
		Catalog res;

		#endregion

		#region Test initialize

		public BookSerializerTests()
		{
			serializer = new BookSerializer();
			res = serializer.Deserialize("books.xml");
		}

		#endregion

		#region Tests

		[Fact]
		public void Serialize()
		{
			Assert.Equal("2016-02-05", res.DateString);
			Assert.True(res.Books.Exists(book => book.Author == "Löwy, Juval"));
		}

		[Fact]
		public void Deserialize()
		{
			serializer.Serialize("DeserialitionResult.xml", res);
			Catalog deserializationCatalog = serializer.Deserialize("DeserialitionResult.xml");

			Assert.Equal(deserializationCatalog.Date, res.Date);
			Assert.Equal(res.Books.Exists(book => book.Author == "Vanya)"),
				deserializationCatalog.Books.Exists(book => book.Author == "Vanya)"));
		}

		#endregion
	}
}
