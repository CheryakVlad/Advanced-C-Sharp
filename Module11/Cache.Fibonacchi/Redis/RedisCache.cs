using System;
using System.IO;
using System.Runtime.Serialization;
using Cache.Fibonacchi.Interfaces;
using StackExchange.Redis;

namespace Cache.Fibonacchi.Redis
{
	public class RedisCache<T> : ICache<T>
	{
		#region Private fields

		private readonly ConnectionMultiplexer _redisConnection;
		private readonly DataContractSerializer _serializer = new DataContractSerializer(typeof(T));
		private readonly string _prefix;

		#endregion

		#region Public methods


		public RedisCache(string hostName, string prefix)
		{
			_prefix = prefix;
			ConfigurationOptions options = new ConfigurationOptions()
			{
				EndPoints = { hostName }
			};

			_redisConnection = ConnectionMultiplexer.Connect(options);
		}

		#endregion

		#region ICache members

		public T Get(string key)
		{
			IDatabase db = _redisConnection.GetDatabase();
			var s = db.StringGet(_prefix + key);

			if (s.IsNull)
			{
				return default(T);
			}

			return (T)_serializer.ReadObject(new MemoryStream(s));
		}

		public void Set(string key, T value, DateTimeOffset expirationDate)
		{
			var db = _redisConnection.GetDatabase();
			var redisKey = _prefix + key;

			if (value == null)
			{
				db.StringSet(redisKey, RedisValue.Null);
			}
			else
			{
				var stream = new MemoryStream();
				_serializer.WriteObject(stream, value);
				db.StringSet(redisKey, stream.ToArray(), expirationDate - DateTimeOffset.Now);
			}
		}

		#endregion
	}
}
