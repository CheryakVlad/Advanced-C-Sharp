using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Module6.Bases;
using Module6.Constants;
using Module6.Entities;
using Module6.Interfaces;

namespace Module6.Readers
{
	public class PatentReader : IReaderElement
	{
		public string ElementName => ConstantsHelper.PATENT_ELEMENT;

		public IEntity ReadEntity(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException(nameof(element));
			}

			Patent patent = new Patent
			{
				Name = element.Attribute(ConstantsHelper.NAME_ATTRIBUTE).Value,
				Country = element.Attribute(ConstantsHelper.COUNTRY_ATTRIBUTE).Value,
				FilingDate = DateTime.ParseExact(element.Attribute(ConstantsHelper.FILING_DATE_ATTRIBUTE).Value, CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern,
				CultureInfo.InvariantCulture.DateTimeFormat),
				PublicationDate = DateTime.ParseExact(element.Attribute(ConstantsHelper.PUBLICATION_DATE_ATTRIBUTE).Value, CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern,
				CultureInfo.InvariantCulture.DateTimeFormat),
				PagesNumber = int.Parse(element.Attribute(ConstantsHelper.PAGES_NUMBER_ATTRIBUTE).Value),
				RegNumber = element.Attribute(ConstantsHelper.REG_NUMBER_ATTRIBUTE).Value,
				Note = element.Element(ConstantsHelper.NOTE_ELEMENT).Value,
				Creators = element.Element(ConstantsHelper.CREATORS_ELEMENT).Elements(ConstantsHelper.CREATOR_ELEMENT).Select(x => new Creator
				{
					FirstName = x.Attribute(ConstantsHelper.FIRSTNAME_ATTRIBUTE).Value,
					LastName = x.Attribute(ConstantsHelper.LASTNAME_ATTRIBUTE).Value
				}).ToList()
			};

			return patent;
		}
	}
}
