using System;
using Cache.Fibonacchi.Interfaces;

namespace Cache.Fibonacchi.Fibonacchi
{
	public class FibonacchiNumber
	{
		#region Private fields

		private ICache<long> _cache;

		#endregion

		#region Constructors

		public FibonacchiNumber(ICache<long> cache)
		{
			_cache = cache;
		}

		#endregion

		#region Public methods

		public long Calculate(long index)
		{
			if (index <= 0L)
			{
				throw new ArgumentException($"{nameof(index)} must be positive");
			}

			if (index <= 2L)
			{
				return 1L;
			}

			long cache = _cache.Get(index.ToString());

			if (cache != default(long))
			{
				return cache;
			}

			long result = Calculate(index - 1) + Calculate(index - 2);

			_cache.Set(index.ToString(), result, DateTimeOffset.Now.AddSeconds(2));

			return result;
		}

		#endregion
	}
}
