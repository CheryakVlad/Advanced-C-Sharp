using System;
using System.Collections.Generic;
using Module6.Bases;

namespace Module6.Entities
{
	public class Book : IEntity, IEquatable<Book>
	{
		public string Name
		{
			get; set;
		}

		public List<Author> Authors
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

		public string ISBN
		{
			get; set;
		}

		#region IEquatable members

		public bool Equals(Book other)
		{
			return PagesCount == other.PagesCount
				   && Name == other.Name
				   && ISBN == other.ISBN
				   && Note == other.Note
				   && PagesCount == other.PagesCount
				   && PublishYear == other.PublishYear
				   && PublicationPlace == other.PublicationPlace
				   && PublishingHouse == other.PublishingHouse;
		}

		#endregion
	}
}
