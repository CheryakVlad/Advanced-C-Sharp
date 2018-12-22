﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BasicSerialization.BusinessEntities
{
	[XmlRoot("catalog", Namespace = @"http://library.by/catalog")]
	public class Catalog
	{
		[XmlElement(ElementName = "book")]
		public List<Book> Books
		{
			get; set;
		}

		[XmlIgnore]
		public DateTime Date
		{
			get; set;
		}

		[XmlAttribute(AttributeName = "date")]
		public string DateString
		{
			get => Date.ToString("yyyy-MM-dd");
			set => Date = DateTime.Parse(value);
		}
	}
}
