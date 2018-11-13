using System.Xml.Linq;
using Module6.Bases;

namespace Module6.Interfaces
{
	public interface IReaderElement
	{
		string ElementName
		{
			get;
		}

		IEntity ReadEntity(XElement element);
	}
}
