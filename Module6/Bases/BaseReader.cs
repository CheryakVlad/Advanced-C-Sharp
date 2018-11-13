using System.Xml.Linq;
using Module6.Exception;
using Module6.Interfaces;

namespace Module6.Bases
{
	public abstract class BaseReader : IReaderElement
	{

		#region Implement IReaderElement

		public abstract string ElementName
		{
			get;
		}
		public abstract IEntity ReadEntity(XElement element);

		#endregion

		protected string GetAttributeValue(XElement element, string name, bool isMandatory = false)
		{
			var attribute = element.Attribute(name);

			if (string.IsNullOrWhiteSpace(attribute?.Value) && isMandatory)
			{
				throw new MandatoryMissedException($"{name}");
			}

			return attribute?.Value;
		}

		protected XElement GetElement(XElement parentElement, string name, bool isMandatory = false)
		{
			var element = parentElement.Element(name);

			if (string.IsNullOrWhiteSpace(element?.Value) && isMandatory)
			{
				throw new MandatoryMissedException($"{name}");
			}

			return element;
		}
	}
}
