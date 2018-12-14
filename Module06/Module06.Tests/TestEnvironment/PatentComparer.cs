using System.Collections;
using System.Collections.Generic;
using Module6.Entities;

namespace Module06.Tests.TestEnvironment
{
	public class PatentComparer : IComparer, IComparer<Patent>
	{
		public int Compare(Patent x, Patent y)
		{
			return x.Country == y.Country
				   && x.Name == y.Name
				   && x.FilingDate == y.FilingDate
				   && x.Note == y.Note
				   && x.PagesNumber == y.PagesNumber
				   && x.PublicationDate == y.PublicationDate
				   && x.RegNumber == y.RegNumber ? 0 : 1;
		}

		public int Compare(object x, object y)
		{
			return Compare(x as Patent, y as Patent);
		}
	}
}
