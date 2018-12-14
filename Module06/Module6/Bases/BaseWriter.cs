using System;
using System.Xml;
using System.Xml.Linq;
using Module6.Exception;
using Module6.Interfaces;

namespace Module6.Bases
{
	public abstract class BaseWriter : IWriterElement
	{
		#region Constants

		private const string EMPTY_MANDATORY_ATTRIBUTE = "Value of mandatory attribute \"{0}\" is null or empty";
		private const string EMPTY_MANDATORY_ELEMENT = "Value of mandatory element \"{0}\" is null";

		#endregion

		#region Implement IWriterElement

		public abstract Type TypeToWrite
		{
			get;
		}

		public abstract void WriteEntity(XmlWriter xmlWriter, IEntity entity);

		#endregion


		#region Protected methods

		protected void AddAttribute(XElement element, string attributeName, string value, bool isMandatory = false)
		{
			if (string.IsNullOrEmpty(value) && isMandatory)
			{
				throw new MandatoryMissedException(string.Format(EMPTY_MANDATORY_ATTRIBUTE, attributeName));
			}

			element.SetAttributeValue(attributeName, value);
		}

		protected void AddElement(XElement element, string newElementName, object value, bool isMandatory = false)
		{
			if (value == null && isMandatory)
			{
				throw new MandatoryMissedException(string.Format(EMPTY_MANDATORY_ELEMENT, newElementName));
			}

			var newElement = new XElement(newElementName, value);
			element.Add(newElement);
		}

		#endregion
	}
}
