using Module2_Task2.Interfaces;
using System;
using System.Linq;

namespace Module2_Task2
{
	public sealed class StringConverter : IConverter<int>
	{
		#region Public methods

		public int Convert(string value)
		{
			int convertValue = 0;

			try
			{
				convertValue = ToInt32(value);
			}
			catch (ArgumentNullException ex)
			{
				throw new ConverterException(ex.Message);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				throw new ConverterException(ex.Message);
			}

			return convertValue;
		}

		#endregion

		#region Private methods

		private int ToInt32(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentNullException(nameof(value));
			}

			if (value.Any(symbol => symbol < 48 && symbol > 57))
			{
				throw new ArgumentOutOfRangeException(nameof(value));
			}

			int result = 0;

			for (int i = 0; i < value.Length - 1; i++)
			{
				result = checked(result * 10 + value[i] - 48);
			}

			return result;
		}

		#endregion
	}
}
