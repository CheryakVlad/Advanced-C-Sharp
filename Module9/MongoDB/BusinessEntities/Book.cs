using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB.BusinessEntities
{
	public class Book : IComparable
	{
		#region Public properties

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

		#endregion

		#region Implement IComparable

		public int CompareTo(object obj)
		{
			Book book = obj as Book;

			if (book != null)
			{
				return this.Name.CompareTo(book.Name);
			}
			else
			{
				throw new Exception("Unable to compare two objects");
			}
		}

		#endregion
	}
}
