using System.Threading;
using Cache.Fibonacchi.Cache;
using Cache.Fibonacchi.Fibonacchi;
using Cache.Fibonacchi.Redis;
using Xunit;

namespace Cache.Fibonacchi.Tests
{
	public class FibonacchiNumberTests
	{
		#region Tests

		[Fact]
		public void Test_should_check_Fibonacci_MemoryCache()
		{
			#region ARRANGE

			Cache<long> cache = new Cache<long>("fibonacchi");
			FibonacchiNumber fibonacchi = new FibonacchiNumber(cache);

			#endregion

			#region ACT

			long result = fibonacchi.Calculate(5);

			#endregion

			#region ASSERT

			Assert.Equal(5, result);
			Assert.Equal(5, cache.Get("5"));

			#endregion
		}

		[Fact]
		public void Test_should_check_expiration_date_Fibonacci_MemoryCache()
		{
			#region ARRANGE

			Cache<long> cache = new Cache<long>("fibonacchi");
			FibonacchiNumber fibonacchi = new FibonacchiNumber(cache);

			#endregion

			#region ACT

			long result = fibonacchi.Calculate(5);

			#endregion

			#region ASSERT

			Assert.Equal(5, result);
			Assert.Equal(5, cache.Get("5"));
			Thread.Sleep(2001);
			Assert.NotEqual(5, cache.Get("5"));

			#endregion
		}

		[Fact]
		public void Test_should_check_Fibonacci_RedisCache()
		{
			#region ARRANGE

			RedisCache<long> redisCache = new RedisCache<long>("localhost", "fibonacchi");
			FibonacchiNumber fibonacchi = new FibonacchiNumber(redisCache);

			#endregion

			#region ACT

			long result = fibonacchi.Calculate(5);

			#endregion

			#region ASSERT

			Assert.Equal(5, result);
			Assert.Equal(5, redisCache.Get("5"));

			#endregion
		}

		[Fact]
		public void Test_should_check_expiration_date_Fibonacci_RedisCache()
		{
			#region ARRANGE

			RedisCache<long> redisCache = new RedisCache<long>("localhost", "fibonacchi");
			FibonacchiNumber fibonacchi = new FibonacchiNumber(redisCache);

			#endregion

			#region ACT

			long result = fibonacchi.Calculate(5);

			#endregion

			#region ASSERT

			Assert.Equal(5, result);
			Assert.Equal(5, redisCache.Get("5"));
			Thread.Sleep(2001);
			Assert.NotEqual(5, redisCache.Get("5"));

			#endregion
		}

		#endregion
	}
}
