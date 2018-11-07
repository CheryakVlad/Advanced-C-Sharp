using System;
using System.Reflection;

namespace Module05
{
	public interface IContainer
	{
		void AddAssembly(Assembly assembly);

		void AddType(Type type);

		void AddType(Type type, Type baseType);

		object CreateInstance(Type type);

		T CreateInstance<T>();
	}
}
