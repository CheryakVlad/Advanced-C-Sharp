using System;
using System.Xml;
using Module6.Bases;

namespace Module6.Interfaces
{
	public interface IWriterElement
	{
		Type TypeToWrite
		{
			get;
		}

		void WriteEntity(XmlWriter xmlWriter, IEntity entity);
	}
}
