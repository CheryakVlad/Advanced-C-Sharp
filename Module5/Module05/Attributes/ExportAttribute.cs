using Module05.Enums;
using System;

namespace Module05.Attributes
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ExportAttribute : Attribute
	{
		public ExportAttribute()
		{

		}

		public ExportAttribute(Type contract)
		{
			Contract = contract;
		}

		public Type Contract
		{
			get; private set;
		}

		public ExportAttribute(LifeCycle mode)
		{
			Mode = mode;
		}

		public LifeCycle Mode
		{
			get; private set;
		}
	}
}
