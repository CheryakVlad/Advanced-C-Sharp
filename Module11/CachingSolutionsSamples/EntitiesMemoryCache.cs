using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Caching;
using System.Threading;
using Cache.Fibonacchi.Cache;
using NorthwindLibrary;

namespace CachingSolutionsSamples
{
	public class EntitiesMemoryCache<T> where T : class
	{
		#region Private fields

		private readonly Cache<IEnumerable<T>> _cache;
		private readonly string _monitorCommand;

		#endregion

		#region Constructors

		public EntitiesMemoryCache(Cache<IEnumerable<T>> cache, string monitorCommand)
		{
			_cache = cache;
			_monitorCommand = monitorCommand;
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
				string connectionString;
				using (var dbContext = new Northwind())
				{
					dbContext.Configuration.LazyLoadingEnabled = false;
					dbContext.Configuration.ProxyCreationEnabled = false;
					entities = dbContext.Set<T>().ToList();
					connectionString = dbContext.Database.Connection.ConnectionString;
				}

				SqlDependency.Start(connectionString);
				_cache.Set(user, entities, GetCachePolicy(_monitorCommand, connectionString));
			}
			return entities;
		}

		#endregion

		#region Internal implementation

		private CacheItemPolicy GetCachePolicy(string monitorCommand, string connectionString)
		{
			return new CacheItemPolicy
			{
				ChangeMonitors = { GetMonitor(monitorCommand, connectionString) }
			};
		}

		private SqlChangeMonitor GetMonitor(string query, string connectionString)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				var command = new SqlCommand(query, connection);
				var monitor = new SqlChangeMonitor(new SqlDependency(command));
				command.ExecuteNonQuery();
				return monitor;
			}
		}

		#endregion
	}
}