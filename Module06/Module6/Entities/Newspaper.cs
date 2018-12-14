using System;
using Module6.Bases;

namespace Module6.Entities
{
	public class Newspaper : IEntity, IEquatable<Newspaper>
	{
		public string Name
		{
			get; set;
		}

		public string PublicationPlace
		{
			get; set;
		}

		public string PublishingHouse
		{
			get; set;
		}

		public int PublishYear
		{
			get; set;
		}

		public int PagesCount
		{
			get; set;
		}

		public string Note
		{
			get; set;
		}

		public int Number
		{
			get; set;
		}

		public DateTime Date
		{
			get; set;
		}

		public string ISSN
		{
			get; set;
		}

		#region IEquatable members

		public bool Equals(Newspaper other)
		{
			return PagesCount == other.PagesCount
				   && Name == other.Name
				   && ISSN == other.ISSN
				   && Note == other.Note
				   && PagesCount == other.PagesCount
				   && PublishYear == other.PublishYear
				   && PublicationPlace == other.PublicationPlace
				   && PublishingHouse == other.PublishingHouse;
		}

		#endregion
	}
}
