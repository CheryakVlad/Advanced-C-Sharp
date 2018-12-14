using System;
using System.Collections.Generic;
using Module6.Bases;

namespace Module6.Entities
{
	public class Patent : IEntity, IEquatable<Patent>
	{
		public string Name
		{
			get; set;
		}

		public List<Creator> Creators
		{
			get; set;
		}

		public string Country
		{
			get; set;
		}

		public string RegNumber
		{
			get; set;
		}

		public DateTime FilingDate
		{
			get; set;
		}

		public DateTime PublicationDate
		{
			get; set;
		}

		public int PagesNumber
		{
			get; set;
		}

		public string Note
		{
			get; set;
		}

		#region IEquatable members

		public bool Equals(Patent other)
		{
			return Country == other.Country
				   && Name == other.Name
				   && FilingDate == other.FilingDate
				   && Note == other.Note
				   && PagesNumber == other.PagesNumber
				   && PublicationDate == other.PublicationDate
				   && RegNumber == other.RegNumber;
		}

		#endregion
	}
}
