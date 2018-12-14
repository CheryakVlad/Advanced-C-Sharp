using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Module06.Tests.TestEnvironment;
using Module6;
using Module6.Bases;
using Module6.Entities;
using Module6.Readers;
using Module6.Writers;
using Xunit;

namespace Module06.Tests
{
	public class CatalogTests
	{
		#region Private fields 

		private Catalog _target;

		#endregion

		#region Test context

		public CatalogTests()
		{
			_target = new Catalog();
			_target.AddReaders(new BookReader(), new NewspaperReader(), new PatentReader());
			_target.AddWriters(new BookWriter(), new NewspaperWriter(), new PatentWriter());
		}

		private Newspaper CreateNewspaper()
		{
			return new Newspaper
			{
				Name = "NY Times",
				PublicationPlace = "NY",
				PublishingHouse = "NY typography",
				PublishYear = 2018,
				PagesCount = 16,
				Date = new DateTime(2018, 11, 11),
				ISSN = "0812-9574",
				Number = 11,
				Note = "NY Times"
			};
		}

		private Patent CreatePatent()
		{
			return new Patent
			{
				Name = "Airplane",
				Country = "USA",
				RegNumber = "D0000126",
				FilingDate = new DateTime(1905, 12, 24),
				PublicationDate = new DateTime(1906, 1, 20),
				PagesNumber = 100,
				Note = "First plane",
				Creators = new List<Creator>
				{
					new Creator {FirstName = "Orville", LastName = "Wright"},
					new Creator {FirstName = "Wilbur", LastName = "Wright"}
				}
			};
		}

		private Book CreateBook()
		{
			return new Book
			{
				Name = "CLR via C#",
				PublicationPlace = "Saint-Petersburg",
				PublishingHouse = "Piter",
				PublishYear = 2013,
				PagesCount = 896,
				ISBN = "978-5-496-00433-6",
				Note = "Microsoft",
				Authors = new List<Author>
				{
					new Author {FirstName = "Jeffrey", LastName = "Richter"}
				}
			};
		}

		private string GetBookXml()
		{
			return @"<book name=""CLR via C#"" " +
					   @"publicationPlace=""Saint-Petersburg"" " +
					   @"publishingHouse=""Piter"" " +
					   @"publishYear=""2013"" " +
					   @"pagesCount=""896"" " +
					   @"ISBN=""978-5-496-00433-6"">" +
					   "<authors>" +
							@"<author firstName=""Jeffrey"" lastName=""Richter"" />" +
					   "</authors>" +
					   "<note>Microsoft</note>" +
				   "</book>";
		}

		private string GetNewspaperXml()
		{
			return "<newspaper name=\"NY Times\" " +
					   "publicationPlace=\"NY\" " +
					   "publishingHouse=\"NY typography\" " +
					   "publishYear=\"2018\" " +
					   "pagesCount=\"16\" " +
					   "date=\"11/11/2018\" " +
					   "ISSN=\"0812-9574\" " +
					   "number=\"11\">" +
					   "<note>NY Times</note>" +
				   "</newspaper>";
		}
		

		private string GetPatentXml()
		{
			return "<patent name=\"Airplane\" " +
					   "country=\"USA\" " +
					   "regNumber=\"D0000126\" " +
					   "filingDate=\"12/24/1905\" " +
					   "publicationDate=\"01/20/1906\" " +
					   "pagesNumber=\"100\">" +
					   "<note>First plane</note>" +
					   "<creators>" +
							"<creator firstName=\"Orville\" lastName=\"Wright\" />" +
							"<creator firstName=\"Wilbur\" lastName=\"Wright\" />" +
					   "</creators>" +
				   "</patent>";
		}

		#endregion

		#region Tests

		[Fact]
		public void Test_it_should_Books_Read()
		{
			#region ARRANGE

			TextReader sr = new StringReader(@"<?xml version=""1.0"" encoding=""utf-16""?>" +
											 "<catalog>" +
											 GetBookXml() +
											 "</catalog>");

			#endregion

			#region ACT

			List<IEntity> books = _target.ReadFrom(sr).ToList();

			#endregion

			#region ASSERT

			Assert.Equal(books, new List<Book>
			{
				CreateBook()
			});

			sr.Dispose();

			#endregion
		}

		[Fact]
		public void Test_it_should_Newspapers_Read()
		{
			#region ARRANGE

			TextReader sr = new StringReader(@"<?xml version=""1.0"" encoding=""utf-16""?>" +
											 "<catalog>" +
											 GetNewspaperXml() +
											 "</catalog>");

			#endregion

			#region ACT

			IEnumerable<IEntity> newspapers = _target.ReadFrom(sr);

			#endregion

			#region ASSERT

			Assert.Equal(newspapers, new[]
			{
				CreateNewspaper()
			});

			sr.Dispose();

			#endregion
		}

		[Fact]
		public void Test_it_should_Patents_Read()
		{
			#region ARRANGE

			TextReader sr = new StringReader(@"<?xml version=""1.0"" encoding=""utf-16""?>" +
											 "<catalog>" +
												GetPatentXml() +
											 "</catalog>");

			#endregion

			#region ACT

			IEnumerable<IEntity> newspapers = _target.ReadFrom(sr);

			#endregion

			#region ASSERT

			Assert.Equal(newspapers, new[]
			{
				CreatePatent()
			});

			sr.Dispose();

			#endregion
		}

		[Fact]
		public void Test_it_should_MixedEntities_Read()
		{
			#region ARRANGE

			TextReader sr = new StringReader(@"<?xml version=""1.0"" encoding=""utf-16""?>" +
											 "<catalog>" +
												GetPatentXml() +
												GetBookXml() +
												GetNewspaperXml() +
											 "</catalog>");

			#endregion

			#region ACT

			List<IEntity> entities = _target.ReadFrom(sr).ToList();

			#endregion

			#region ASSERT

			Assert.True(new PatentComparer().Compare(entities[0], CreatePatent()) == 0);
			Assert.True(new BookComparer().Compare(entities[1], CreateBook()) == 0);
			Assert.True(new NewspaperComparer().Compare(entities[2], CreateNewspaper()) == 0);

			sr.Dispose();

			#endregion
		}

		[Fact]
		public void Test_it_should_MixedEntities_Write()
		{
			#region ARRANGE

			StringBuilder actualResult = new StringBuilder();
			StringWriter sw = new StringWriter(actualResult);
			Book book = CreateBook();
			Newspaper newspaper = CreateNewspaper();
			Patent patent = CreatePatent();

			var entities = new IEntity[]
			{
				book,
				newspaper,
				patent
			};

			string expectedResult = @"<?xml version=""1.0"" encoding=""utf-16""?>" +
				"<catalog>" +
					GetBookXml() +
					GetNewspaperXml() +
					GetPatentXml() +
				"</catalog>";

			#endregion

			#region ACT

			_target.WriteTo(sw, entities);
			sw.Dispose();

			#endregion

			#region ASSERT

			Assert.Equal(expectedResult, actualResult.ToString());

			#endregion
		}

		#endregion
	}
}
