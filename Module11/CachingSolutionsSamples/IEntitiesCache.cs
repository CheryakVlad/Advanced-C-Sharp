using System;

namespace CachingSolutionsSamples
{
	public interface IEntitiesCache<T>
	{
		T Get(string key);
		void Set(string key, T value, DateTimeOffset expirationDate);
	}
}