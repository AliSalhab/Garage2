using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
	public class Receipt
	{
		public int Id { get; set; }
		public string Registration { get; set; }
		public DateTime CheckInDate { get; set; }
		public DateTime CheckOutDate { get; set; }
		public string TotalTime { get; set; }
		public float Price { get; set; }

		public Receipt(int id, string reg,DateTime checkIn,DateTime checkOut)
		{
			Id = id;
			Registration = reg;
			CheckInDate = checkIn;
			CheckOutDate = checkOut;

			PriceResult(checkIn, checkOut);
		}

		private void PriceResult(DateTime checkIn, DateTime checkOut)
		{
			TimeSpan parkedTime = checkOut.Subtract(checkIn);
			TotalTime = parkedTime.ToString(@"d\.h\:mm");

			double parkedTimeInMinutes = parkedTime.TotalMinutes;
			//var hour = Math.Floor(parkedTimeInMinutes / 60);
			//var Minute = Math.Ceiling(parkedTimeInMinutes - (hour * 60));
			Price = (float) Math.Ceiling(parkedTimeInMinutes * 10);

			//parkedTime.ToString(@"d\.h\:mm"), TotalPrice.ToString();
		}
	}
}