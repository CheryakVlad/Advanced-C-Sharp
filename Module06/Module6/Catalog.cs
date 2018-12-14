using Module6.Bases;
using Module6.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Module6
{
	public class Catalog
	{
		#region Constants

		public const string CATALOG_NAME = "catalog";
		public const string UNKNOWN_ELEMENT = "Founded unknown element tag: {0}";
		public const string NO_ENTITY_WRITER = "Cannot find entity writer for type {0}";

		#endregion

		#region Private variable

		private readonly IDictionary<string, IReaderElement> _elementReaders;
		private readonly IDictionary<Type, IWriterElement> _elementWriters;

		#endregion

		#region Constructors

		public Catalog()
		{
			_elementReaders = new Dictionary<string, IReaderElement>();
			_elementWriters = new Dictionary<Type, IWriterElement>();
		}

		#endregion

		#region Public methods

		public void AddReaders(params IReaderElement[] elementReaders)
		{
			foreach (var reader in elementReaders)
			{
				_elementReaders.Add(reader.ElementName, reader);
			}
		}

		public void AddWriters(params IWriterElement[] writers)
		{
			foreach (var writer in writers)
			{
				_elementWriters.Add(writer.TypeToWrite, writer);
			}
		}

		public IEnumerable<IEntity> ReadFrom(TextReader input)
		{
			using (XmlReader xmlReader = XmlReader.Create(input, new XmlReaderSettings
			{
				IgnoreWhitespace = true,
				IgnoreComments = true
			}))
			{
				xmlReader.ReadToFollowing(CATALOG_NAME);
				xmlReader.ReadStartElement();

				do
				{
					while (xmlReader.NodeType == XmlNodeType.Element)
					{
						var node = XNode.ReadFrom(xmlReader) as XElement;
						IReaderElement parser;

						if (_elementReaders.TryGetValue(node.Name.LocalName, out parser))
						{
							yield return parser.ReadEntity(node);
						}
						else
						{
							throw new ArgumentNullException(string.Format(UNKNOWN_ELEMENT, node.Name.LocalName));
						}
					}
				} while (xmlReader.Read());
			}
		}

		public void WriteTo(TextWriter output, IEnumerable<IEntity> catalogEntities)
		{
			using (XmlWriter xmlWriter = XmlWriter.Create(output, new XmlWriterSettings()))
			{
				xmlWriter.WriteStartDocument();
				xmlWriter.WriteStartElement(CATALOG_NAME);

				foreach (var catalogEntity in catalogEntities)
				{
					IWriterElement writer;

					if (_elementWriters.TryGetValue(catalogEntity.GetType(), out writer))
					{
						writer.WriteEntity(xmlWriter, catalogEntity);
					}
					else
					{
						throw new ArgumentNullException(string.Format(NO_ENTITY_WRITER, catalogEntity.GetType().FullName));
					}
				}

				xmlWriter.WriteEndElement();
			}
		}

		#endregion
	}
}
