using System;
using Module05.Enums;

namespace Module05
{
	public class RegisteredObject
	{
		public RegisteredObject(Type typeToResolve, Type concreteType, LifeCycle lifeCycle)
		{
			TypeToResolve = typeToResolve;
			ConcreteType = concreteType;
			LifeCycle = lifeCycle;
		}

		public RegisteredObject(Type typeToResolve, Type concreteType) : this(typeToResolve, concreteType, LifeCycle.Singleton)
		{
			
		}

		public RegisteredObject(Type typeToCreate) : this(typeToCreate, typeToCreate, LifeCycle.Singleton)
		{

		}

		public Type TypeToResolve
		{
			get; private set;
		}

		public Type ConcreteType
		{
			get; private set;
		}

		public object Instance
		{
			get; private set;
		}

		public LifeCycle LifeCycle
		{
			get; private set;
		}

		public void CreateInstance(params object[] args)
		{
			this.Instance = Activator.CreateInstance(this.ConcreteType, args);
		}
	}
}
