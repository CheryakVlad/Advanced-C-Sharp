using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.BusinessEntities;
using MongoDB.Driver;
using Xunit;

namespace MongoDB
{
	public class Task : IDisposable
	{
		#region Constatnts

		private const int COUNT_OF_BOOKS_WHICH_COUNT_MORE_ONE = 4;
		private const int COUNT_OF_RETRIEVING_BOOKS = 3;
		private const int COUNT_OF_COPY_BOOKS_WHICH_COUNT_MORE_ONE = 29;
		private const int COUNT_OF_AUTHORS = 3;
		private const int COUNT_OF_BOOKS_WITHOUT_AUTHORS = 2;
		private const int COUNT_OF_BOOKS_WHICH_COUNT_OF_COPY_LESS_3 = 4;

		#endregion

		#region Private fields

		private IMongoCollection<Book> books;

		#endregion

		#region Test context

		public Task()
		{
			var client = new MongoClient("mongodb://localhost:27017");

			var db = client.GetDatabase("Demo");

			books = db.GetCollection<Book>("Books");

			InitBooksCollection();
		}

		private void InitBooksCollection()
		{
			if (books.Count(FilterDefinition<Book>.Empty) == 0)
			{
				books.InsertMany(new[]
				{
					new Book
					{
						Name = "Hobbit",
						Author = "Tolkien",
						Count = 5,
						Genre = new[] { "fantasy" },
						Year = 2014
					},
					new Book
					{
						Name = "Lord of the rings",
						Author = "Tolkien",
						Count = 3,
						Genre = new[] { "fantasy" },
						Year = 2015
					},
					new Book
					{
						Name = "Kolobok",
						Count = 10,
						Genre = new[] { "kids" },
						Year = 2000
					},
					new Book
					{
						Name = "Repka",
						Count = 11,
						Genre = new[] { "kids" },
						Year = 2000
					},
					new Book
					{
						Name = "Dyadya Stiopa",
						Author = "Mihalkov",
						Count = 1,
						Genre = new[] { "kids" },
						Year = 2001
					}
				});
			}
		}

		#endregion

		#region Tests

		[Fact]
		public void Test_it_should_check_names_of_books_count_of_which_more_than_one()
		{
			#region ARRANGE

			#endregion

			#region ACT

			var booksWithCountMoreThanOne = books.Find(b => b.Count > 1).ToList();

			#endregion

			#region ASSERT

			Assert.Equal(COUNT_OF_BOOKS_WHICH_COUNT_MORE_ONE, booksWithCountMoreThanOne.Count);

			#endregion
		}

		[Fact]
		public void Test_it_should_check_sorting_names_of_books_count_of_which_more_than_one()
		{
			#region ARRANGE

			#endregion

			#region ACT

			var booksSortedByName = books.Find(b => b.Count > 1).SortBy(b => b.Name).ToList();

			#endregion

			#region ASSERT

			Assert.Equal(books.Find(b => b.Count > 1).ToList().Count, booksSortedByName.Count);
			Assert.Equal("Hobbit", booksSortedByName[0].Name);
			Assert.Equal("Kolobok", booksSortedByName[1].Name);
			Assert.Equal("Lord of the rings", booksSortedByName[2].Name);
			Assert.Equal("Repka", booksSortedByName[3].Name);

			#endregion
		}

		[Fact]
		public void Test_it_should_check_retrieve_3_books_count_of_which_more_than_one()
		{
			#region ARRANGE

			#endregion

			#region ACT

			var booksLimitCopy = books.Find(b => b.Count > 1).Limit(COUNT_OF_RETRIEVING_BOOKS).ToList();

			#endregion

			#region ASSERT

			Assert.Equal(COUNT_OF_RETRIEVING_BOOKS, booksLimitCopy.Count);

			#endregion
		}

		[Fact]
		public void Test_it_should_check_count_of_books_count_of_which_more_than_one()
		{
			#region ARRANGE

			var booksWithCountMoreThanOne = books.Find(b => b.Count > 1).ToList();

			#endregion

			#region ACT

			var booksCount = booksWithCountMoreThanOne.Aggregate(0, (x, y) => x + y.Count);

			#endregion

			#region ASSERT

			Assert.Equal(COUNT_OF_COPY_BOOKS_WHICH_COUNT_MORE_ONE, booksCount);

			#endregion
		}

		[Fact]
		public void Test_it_should_check_book_with_max_count()
		{
			#region ARRANGE

			#endregion

			#region ACT

			var bookWithMaxCount = books.Find(b => b.Count > 0).SortByDescending(b => b.Count).FirstOrDefault();

			#endregion

			#region ASSERT

			Assert.Equal("Repka", bookWithMaxCount.Name);

			#endregion
		}

		[Fact]
		public void Test_it_should_check_book_with_min_count()
		{
			#region ARRANGE

			#endregion

			#region ACT

			var bookWithMinCount = books.Find(b => b.Count > 0).SortBy(b => b.Count).FirstOrDefault();

			#endregion

			#region ASSERT

			Assert.Equal("Dyadya Stiopa", bookWithMinCount.Name);

			#endregion
		}

		[Fact]
		public void Test_it_should_check_list_of_authors()
		{
			#region ARRANGE

			#endregion

			#region ACT

			var authors = books.Distinct<string>("Author", new BsonDocument()).ToList();

			#endregion

			#region ASSERT

			Assert.Equal(COUNT_OF_AUTHORS, authors.Count);

			#endregion
		}

		[Fact]
		public void Test_it_should_check_books_without_author()
		{
			#region ARRANGE

			#endregion

			#region ACT

			var booksWithoutAuthor = books.Find(b => b.Author == null);

			#endregion

			#region ASSERT

			Assert.Equal(COUNT_OF_BOOKS_WITHOUT_AUTHORS, booksWithoutAuthor.Count());

			#endregion
		}

		[Fact]
		public void Test_it_should_check_that_books_count_increments_by_one()
		{
			#region ARRANGE

			#endregion

			#region ACT

			books.UpdateMany(FilterDefinition<Book>.Empty, Builders<Book>.Update.Inc(b => b.Count, 1));

			#endregion

			#region ASSERT

			Assert.Equal(2, books.Find(b => b.Name == "Dyadya Stiopa").First().Count);
			Assert.Equal(6, books.Find(b => b.Name == "Hobbit").First().Count);
			Assert.Equal(4, books.Find(b => b.Name == "Lord of the rings").First().Count);
			Assert.Equal(11, books.Find(b => b.Name == "Kolobok").First().Count);
			Assert.Equal(12, books.Find(b => b.Name == "Repka").First().Count);

			#endregion
		}

		[Fact]
		public void Test_it_should_check_add_genre()
		{
			#region ARRANGE

			#endregion

			#region ACT

			books.UpdateMany(Builders<Book>.Filter.Where(b => b.Genre.Any(g => g == "fantasy")),

			Builders<Book>.Update.AddToSet(b => b.Genre, "favority"));

			#endregion

			#region ASSERT

			Assert.Contains("favority", books.Find(Builders<Book>.Filter.Where(b => b.Genre.Any(g => g == "fantasy"))).First().Genre);

			#endregion
		}

		[Fact]
		public void Test_it_should_check_remove_books_which_count_less_3()
		{
			#region ARRANGE

			#endregion

			#region ACT

			books.DeleteMany(Builders<Book>.Filter.Lt(b => b.Count, 3));

			#endregion

			#region ASSERT

			Assert.Equal(COUNT_OF_BOOKS_WHICH_COUNT_OF_COPY_LESS_3, books.Find(b => b.Count > 1).Count());

			#endregion
		}

		#endregion

		#region IDisposable implemented

		public void Dispose()
		{
			books.DeleteMany(Builders<Book>.Filter.Empty);
		}

		#endregion
	}
}
