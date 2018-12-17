using System;
using System.Runtime.Caching;
using Cache.Fibonacchi.Interfaces;

namespace Cache.Fibonacchi.Cache
{
	public class Cache<T> : ICache<T>
	{
		#region Private fields

		private MemoryCache _cache = new MemoryCache("CachingProvider");
		private readonly string _prefix;

		#endregion

		#region Constructors

		public Cache(string prefix)
		{
			_prefix = prefix;
		}

		#endregion

		#region Public methods

		public T Get(string key)
		{
			var result = _cache.Get(key);

			if (result == null)
			{
				return default(T);
			}

			return (T)result;
		}

		public void Set(string key, T value, DateTimeOffset expirationDate)
		{
			_cache.Add(key, value, expirationDate);
		}

		public void Set(string key, T value, CacheItemPolicy policy)
		{
			_cache.Set(key, value, policy);
		}

		#endregion
	}
}
