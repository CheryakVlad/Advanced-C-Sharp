using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cache.Fibonacchi.Interfaces;
using NorthwindLibrary;

namespace CachingSolutionsSamples
{
	public class EntityManager<T> where T : class
	{

		#region Private fields

		private ICache<IEnumerable<T>> _cache;

		#endregion

		#region Constructors

		public EntityManager(ICache<IEnumerable<T>> cache)
		{
			_cache = cache;
		}

		#endregion

		#region Public methods

		public IEnumerable<T> GetEntities()
		{
			Console.WriteLine("Get Entities");
			var user = Thread.CurrentPrincipal.Identity.Name;
			var entities = _cache.Get(user);

			if (entities == null)
			{
				Console.WriteLine("From no cache storage");
				using (var dbContext = new Northwind())
				{
					dbContext.Configuration.LazyLoadingEnabled = false;
					dbContext.Configuration.ProxyCreationEnabled = false;
					entities = dbContext.Set<T>().ToList();
				}

				_cache.Set(user, entities, DateTimeOffset.Now.AddDays(1));
			}
			return entities;
		}

		#endregion
	}
}