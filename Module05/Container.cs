using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Module05.Attributes;
using Module05.Enums;
using Module05.Exception;

namespace Module05
{
	public class Container : IContainer
	{
		#region Private fields

		private readonly IList<RegisteredObject> _registeredObjects = new List<RegisteredObject>();

		#endregion

		#region Implements IContainer

		public void AddAssembly(Assembly assembly)
		{
			var types = assembly.ExportedTypes;
			foreach (var type in types)
			{
				var constructorImportAttribute = type.GetCustomAttribute<ImportConstructorAttribute>();
				if (constructorImportAttribute != null || HasImportProperties(type))
				{
					_registeredObjects.Add(new RegisteredObject(type, type));
				}

				var exportAttributes = type.GetCustomAttributes<ExportAttribute>();
				foreach (var exportAttribute in exportAttributes)
				{
					_registeredObjects.Add(new RegisteredObject(exportAttribute.Contract ?? type, type));
				}
			}
		}

		public void AddType(Type type)
		{
			_registeredObjects.Add(new RegisteredObject(type, type));
		}

		public void AddType(Type type, Type baseType)
		{
			_registeredObjects.Add(new RegisteredObject(baseType, type));
		}

		public object CreateInstance(Type type)
		{
			var instance = ConstructInstanceOfType(type);
			return instance;
		}

		public T CreateInstance<T>()
		{
			var type = typeof(T);
			var instance = (T)ConstructInstanceOfType(type);
			return instance;
		}

		#endregion

		#region Private methods

		private object ConstructInstanceOfType(Type type)
		{
			if (!_registeredObjects.Any(x => x.TypeToResolve == type))
			{
				throw new RegisteredException($"Cannot create instance of {type.FullName}. Dependency is not provided");
			}

			Type dependendType = _registeredObjects.FirstOrDefault(x => x.TypeToResolve == type).ConcreteType;
			object instance = GetInstance(_registeredObjects.FirstOrDefault(x => x.TypeToResolve == type));

			if (dependendType.GetCustomAttribute<ImportConstructorAttribute>() != null)
			{
				return instance;
			}

			ResolveProperties(dependendType, instance);
			return instance;
		}

		private bool HasImportProperties(Type type)
		{
			var propertiesInfo = GetPropertiesRequiedImport(type);
			return propertiesInfo.Any();
		}

		private IEnumerable<PropertyInfo> GetPropertiesRequiedImport(Type type)
		{
			return type.GetProperties().Where(p => p.GetCustomAttribute<ImportAttribute>() != null);
		}

		private void ResolveProperties(Type type, object instance)
		{
			var propertiesInfo = GetPropertiesRequiedImport(type);
			foreach (var property in propertiesInfo)
			{
				var resolvedProperty = ConstructInstanceOfType(property.PropertyType);
				property.SetValue(instance, resolvedProperty);
			}
		}

		private ConstructorInfo GetConstructor(Type type)
		{
			ConstructorInfo[] constructors = type.GetConstructors();

			if (constructors.Length == 0)
			{
				throw new RegisteredException($"There are no public constructors for type {type.FullName}");
			}

			return constructors.First();
		}

		private object GetInstance(RegisteredObject registeredObject)
		{
			if (registeredObject.Instance == null ||
				registeredObject.LifeCycle == LifeCycle.Transient)
			{
				var parameters = ResolveConstructorParameters(registeredObject);
				registeredObject.CreateInstance(parameters.ToArray());
			}
			return registeredObject.Instance;
		}

		private IEnumerable<object> ResolveConstructorParameters(RegisteredObject registeredObject)
		{
			var constructorInfo = registeredObject.ConcreteType.GetConstructors().First();
			foreach (var parameter in constructorInfo.GetParameters())
			{
				yield return ResolveObject(parameter.ParameterType);
			}
		}

		private object ResolveObject(Type typeToResolve)
		{
			var registeredObject = _registeredObjects.FirstOrDefault(o => o.TypeToResolve == typeToResolve);
			if (registeredObject == null)
			{
				throw new RegisteredException(string.Format(
					"The type {0} has not been registered", typeToResolve.Name));
			}
			return GetInstance(registeredObject);
		}

		#endregion
	}
}
