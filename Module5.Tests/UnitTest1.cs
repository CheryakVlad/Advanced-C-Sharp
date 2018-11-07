using Module05;
using Module05.Enums;
using Module05.Exception;
using NUnit.Framework;
using System;

namespace Module5.Tests
{
	[TestFixture]
	public class SimpleIocContainerTests
	{
		[Test]
		public void should_resolve_object()
		{
			var container = new Container();

			container.Register<ITypeToResolve, ConcreteType>();

			var instance = container.Resolve<ITypeToResolve>();

			Assert.That(instance, Is.InstanceOf(typeof(ConcreteType)));
		}

		[Test]
		public void should_throw_exception_if_type_not_registered()
		{
			var container = new Container();

			Exception exception = null;
			try
			{
				container.Resolve<ITypeToResolve>();
			}
			catch (Exception ex)
			{
				exception = ex;
			}

			Assert.That(exception, Is.InstanceOf(typeof(TypeNotRegisteredException)));
		}

		[Test]
		public void should_resolve_object_with_registered_constructor_parameters()
		{
			var container = new Container();

			container.Register<ITypeToResolve, ConcreteType>();
			container.Register<ITypeToResolveWithConstructorParams, ConcreteTypeWithConstructorParams>();

			var instance = container.Resolve<ITypeToResolveWithConstructorParams>();

			Assert.That(instance, Is.InstanceOf(typeof(ConcreteTypeWithConstructorParams)));
		}

		[Test]
		public void should_create_singleton_instance_by_default()
		{
			var container = new Container();

			container.Register<ITypeToResolve, ConcreteType>();

			var instance = container.Resolve<ITypeToResolve>();

			Assert.That(container.Resolve<ITypeToResolve>(), Is.SameAs(instance));
		}

		[Test]
		public void can_create_transient_instance()
		{
			var container = new Container();

			container.Register<ITypeToResolve, ConcreteType>(LifeCycle.Transient);

			var instance = container.Resolve<ITypeToResolve>();

			Assert.That(container.Resolve<ITypeToResolve>(), Is.Not.SameAs(instance));
		}
	}

	public interface ITypeToResolve
	{
	}

	public class ConcreteType : ITypeToResolve
	{
	}

	public interface ITypeToResolveWithConstructorParams
	{
	}

	public class ConcreteTypeWithConstructorParams : ITypeToResolveWithConstructorParams
	{
		public ConcreteTypeWithConstructorParams(ITypeToResolve typeToResolve)
		{
		}
	}
}
