using System;
using System.Globalization;
using System.Xml.Linq;
using Module6.Bases;
using Module6.Constants;
using Module6.Entities;
using Module6.Interfaces;

namespace Module6.Readers
{
	public class NewspaperReader : IReaderElement
	{
		public string ElementName => ConstantsHelper.NEWSPAPER_ELEMENT;

		public IEntity ReadEntity(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException(nameof(element));
			}

			Newspaper newspaper = new Newspaper
			{
				Name = element.Attribute(ConstantsHelper.NAME_ATTRIBUTE).Value,
				PublicationPlace = element.Attribute(ConstantsHelper.PUBLICATION_PLACE_ATTRIBUTE).Value,
				PublishingHouse = element.Attribute(ConstantsHelper.PUBLISHING_HOUSE_ATTRIBUTE).Value,
				PagesCount = int.Parse(element.Attribute(ConstantsHelper.PAGES_COUNT_ATTRIBUTE).Value),
				PublishYear = int.Parse(element.Attribute(ConstantsHelper.PUBLISH_YEAR_ATTRIBUTE).Value),
				ISSN = element.Attribute(ConstantsHelper.ISSN_ATTRIBUTE).Value,
				Date = DateTime.ParseExact(element.Attribute(ConstantsHelper.DATE_ATTRIBUTE).Value, CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern,
				CultureInfo.InvariantCulture.DateTimeFormat),
				Number = int.Parse(element.Attribute(ConstantsHelper.NUMBER_ATTRIBUTE).Value),
				Note = element.Element(ConstantsHelper.NOTE_ELEMENT).Value
			};

			return newspaper;
		}
	}
}
