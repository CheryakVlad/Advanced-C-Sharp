using System;
using System.Collections.Generic;
using System.Text;

namespace Module2_Task2.Interfaces
{
	public interface IConverter<T>
	{
		T Convert(string value);
	}
}
