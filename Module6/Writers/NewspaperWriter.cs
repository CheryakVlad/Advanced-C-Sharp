using System;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using Module6.Bases;
using Module6.Constants;
using Module6.Entities;

namespace Module6.Writers
{
	public class NewspaperWriter : BaseWriter
	{
		public override Type TypeToWrite => typeof(Newspaper);

		public override void WriteEntity(XmlWriter xmlWriter, IEntity entity)
		{
			Newspaper newspaper = entity as Newspaper;

			if (entity == null)
			{
				throw new ArgumentNullException(nameof(Newspaper));
			}

			XElement element = new XElement(ConstantsHelper.NEWSPAPER_ELEMENT);

			AddAttribute(element, ConstantsHelper.NAME_ATTRIBUTE, newspaper.Name);
			AddAttribute(element, ConstantsHelper.PUBLICATION_PLACE_ATTRIBUTE, newspaper.PublicationPlace);
			AddAttribute(element, ConstantsHelper.PUBLISHING_HOUSE_ATTRIBUTE, newspaper.PublishingHouse);
			AddAttribute(element, ConstantsHelper.PUBLISH_YEAR_ATTRIBUTE, newspaper.PublishYear.ToString());
			AddAttribute(element, ConstantsHelper.PAGES_COUNT_ATTRIBUTE, newspaper.PagesCount.ToString());
			AddAttribute(element, ConstantsHelper.DATE_ATTRIBUTE, newspaper.Date.ToString(CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern, CultureInfo.InvariantCulture));
			AddAttribute(element, ConstantsHelper.ISSN_ATTRIBUTE, newspaper.ISSN);
			AddAttribute(element, ConstantsHelper.NUMBER_ATTRIBUTE, newspaper.Number.ToString());
			AddElement(element, ConstantsHelper.NOTE_ELEMENT, newspaper.Note);

			element.WriteTo(xmlWriter);
		}
	}
}
