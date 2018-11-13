using System.Collections;
using System.Collections.Generic;
using Module6.Entities;

namespace Module06.Tests.TestEnvironment
{
	public class BookComparer : IComparer, IComparer<Book>
	{
		public int Compare(Book x, Book y)
		{
			return x.PagesCount == y.PagesCount
				   && x.Name == y.Name
				   && x.ISBN == y.ISBN
				   && x.Note == y.Note
				   && x.PagesCount == y.PagesCount
				   && x.PublishYear == y.PublishYear
				   && x.PublicationPlace == y.PublicationPlace
				   && x.PublishingHouse == y.PublishingHouse ? 0 : 1;
		}

		public int Compare(object x, object y)
		{
			return Compare(x as Book, y as Book);
		}
	}
}
