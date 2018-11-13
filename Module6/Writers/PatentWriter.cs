using System;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Module6.Bases;
using Module6.Constants;
using Module6.Entities;

namespace Module6.Writers
{
	public class PatentWriter : BaseWriter
	{
		public override Type TypeToWrite => typeof(Patent);

		public override void WriteEntity(XmlWriter xmlWriter, IEntity entity)
		{
			Patent patent = entity as Patent;

			if (patent == null)
			{
				throw new ArgumentNullException(nameof(Patent));
			}

			XElement element = new XElement(ConstantsHelper.PATENT_ELEMENT);

			AddAttribute(element, ConstantsHelper.NAME_ATTRIBUTE, patent.Name);
			AddAttribute(element, ConstantsHelper.COUNTRY_ATTRIBUTE, patent.Country);
			AddAttribute(element, ConstantsHelper.REG_NUMBER_ATTRIBUTE, patent.RegNumber);
			AddAttribute(element, ConstantsHelper.FILING_DATE_ATTRIBUTE, patent.FilingDate.ToString(CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern, CultureInfo.InvariantCulture));
			AddAttribute(element, ConstantsHelper.PUBLICATION_DATE_ATTRIBUTE, patent.PublicationDate.ToString(CultureInfo.InvariantCulture.DateTimeFormat.ShortDatePattern, CultureInfo.InvariantCulture));
			AddAttribute(element, ConstantsHelper.PAGES_NUMBER_ATTRIBUTE, patent.PagesNumber.ToString());

			AddElement(element, ConstantsHelper.NOTE_ELEMENT, patent.Note);
			AddElement(element, ConstantsHelper.CREATORS_ELEMENT,
				patent.Creators?.Select(a => new XElement(ConstantsHelper.CREATOR_ELEMENT,
					new XAttribute(ConstantsHelper.FIRSTNAME_ATTRIBUTE, a.FirstName),
					new XAttribute(ConstantsHelper.LASTNAME_ATTRIBUTE, a.LastName)
				))
			);

			element.WriteTo(xmlWriter);
		}
	}
}
