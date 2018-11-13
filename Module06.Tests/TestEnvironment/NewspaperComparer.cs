using System.Collections;
using System.Collections.Generic;
using Module6.Entities;

namespace Module06.Tests.TestEnvironment
{
	public class NewspaperComparer : IComparer, IComparer<Newspaper>
	{
		public int Compare(Newspaper x, Newspaper y)
		{
			return x.PagesCount == y.PagesCount
				   && x.Name == y.Name
				   && x.ISSN == y.ISSN
				   && x.Note == y.Note
				   && x.PagesCount == y.PagesCount
				   && x.PublishYear == y.PublishYear
				   && x.PublicationPlace == y.PublicationPlace
				   && x.PublishingHouse == y.PublishingHouse ? 0 : 1;
		}

		public int Compare(object x, object y)
		{
			return Compare(x as Newspaper, y as Newspaper);
		}
	}
}
