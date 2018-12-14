using Module6.Entities;
using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Module6.Bases;
using Module6.Constants;

namespace Module6.Writers
{
	public class BookWriter : BaseWriter
	{
		public override Type TypeToWrite => typeof(Book);

		public override void WriteEntity(XmlWriter xmlWriter, IEntity entity)
		{
			Book book = entity as Book;

			if (book == null)
			{
				throw new ArgumentNullException(nameof(Book));
			}

			XElement element = new XElement(ConstantsHelper.BOOK_ELEMENT);

			AddAttribute(element, ConstantsHelper.NAME_ATTRIBUTE, book.Name);
			AddAttribute(element, ConstantsHelper.PUBLICATION_PLACE_ATTRIBUTE, book.PublicationPlace);
			AddAttribute(element, ConstantsHelper.PUBLISHING_HOUSE_ATTRIBUTE, book.PublishingHouse);
			AddAttribute(element, ConstantsHelper.PUBLISH_YEAR_ATTRIBUTE, book.PublishYear.ToString());
			AddAttribute(element, ConstantsHelper.PAGES_COUNT_ATTRIBUTE, book.PagesCount.ToString());
			AddAttribute(element, ConstantsHelper.ISBN_ATTRIBUTE, book.ISBN);
			AddElement(element, ConstantsHelper.AUTHORS_ELEMENT,
				book.Authors?.Select(a => new XElement(ConstantsHelper.AUTHOR_ELEMENT,
					new XAttribute(ConstantsHelper.FIRSTNAME_ATTRIBUTE, a.FirstName),
					new XAttribute(ConstantsHelper.LASTNAME_ATTRIBUTE, a.LastName)
				))
			);
			AddElement(element, ConstantsHelper.NOTE_ELEMENT, book.Note);

			element.WriteTo(xmlWriter);
		}
	}
}
