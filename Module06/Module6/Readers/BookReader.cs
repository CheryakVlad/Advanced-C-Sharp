using System;
using System.Linq;
using System.Xml.Linq;
using Module6.Bases;
using Module6.Constants;
using Module6.Entities;
using Module6.Interfaces;

namespace Module6.Readers
{
	public class BookReader : IReaderElement
	{
		public string ElementName => ConstantsHelper.BOOK_ELEMENT;

		public IEntity ReadEntity(XElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException(nameof(element));
			}

			Book book = new Book
			{
				Name = element.Attribute(ConstantsHelper.NAME_ATTRIBUTE).Value,
				PublicationPlace = element.Attribute(ConstantsHelper.PUBLICATION_PLACE_ATTRIBUTE).Value,
				PublishingHouse = element.Attribute(ConstantsHelper.PUBLISHING_HOUSE_ATTRIBUTE).Value,
				PagesCount = int.Parse(element.Attribute(ConstantsHelper.PAGES_COUNT_ATTRIBUTE).Value),
				PublishYear = int.Parse(element.Attribute(ConstantsHelper.PUBLISH_YEAR_ATTRIBUTE).Value),
				ISBN = element.Attribute(ConstantsHelper.ISBN_ATTRIBUTE).Value,
				Note = element.Element(ConstantsHelper.NOTE_ELEMENT).Value,
				Authors = element.Element(ConstantsHelper.AUTHORS_ELEMENT).Elements(ConstantsHelper.AUTHOR_ELEMENT).Select(x => new Author
				{
					FirstName = x.Attribute(ConstantsHelper.FIRSTNAME_ATTRIBUTE).Value,
					LastName = x.Attribute(ConstantsHelper.LASTNAME_ATTRIBUTE).Value
				}).ToList()
			};

			return book;
		}
	}
}
