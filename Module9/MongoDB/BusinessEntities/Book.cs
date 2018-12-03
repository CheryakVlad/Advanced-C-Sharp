﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB.BusinessEntities
{
	public class Book
	{
		public ObjectId Id
		{
			get; set;
		}

		[BsonElement("name")]
		public string Name
		{
			get; set;
		}

		[BsonElement("author")]
		public string Author
		{
			get; set;
		}

		[BsonElement("count")]
		public int Count
		{
			get; set;
		}

		[BsonElement("genre")]
		public string[] Genre
		{
			get; set;
		}

		[BsonElement("year")]
		public int Year
		{
			get; set;
		}
	}
}
