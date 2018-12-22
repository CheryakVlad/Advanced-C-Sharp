using System.Data.Entity.Infrastructure;
using System.Runtime.Serialization;
using CustomSerialization.DB;

namespace CustomSerialization.Surrogates
{
	public class OrderDetailSerializationSurrogate : ISerializationSurrogate
	{
		#region ISerializationSurrogate members

		public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			var data = (Order_Detail)obj;
			data.OrderID = info.GetInt32(nameof(data.OrderID));
			data.ProductID = info.GetInt32(nameof(data.ProductID));
			data.UnitPrice = info.GetDecimal(nameof(data.UnitPrice));
			data.Quantity = info.GetInt16(nameof(data.Quantity));
			data.Discount = (float)info.GetValue(nameof(data.Discount), typeof(float));
			data.Order = (Order)info.GetValue(nameof(data.Order), typeof(Order));
			data.Product = (Product)info.GetValue(nameof(data.Product), typeof(Product));
			return data;
		}

		public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			var data = (Order_Detail)obj;
			var dbContext = (context.Context as IObjectContextAdapter).ObjectContext;
			dbContext.LoadProperty(data, x => x.Order);
			dbContext.LoadProperty(data, x => x.Product);
			info.AddValue(nameof(data.OrderID), data.OrderID);
			info.AddValue(nameof(data.ProductID), data.ProductID);
			info.AddValue(nameof(data.UnitPrice), data.UnitPrice);
			info.AddValue(nameof(data.Quantity), data.Quantity);
			info.AddValue(nameof(data.Discount), data.Discount);
			info.AddValue(nameof(data.Order), data.Order);
			info.AddValue(nameof(data.Product), data.Product);
		}

		#endregion
	}
}
