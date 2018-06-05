using Garage2.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
	public class Statistics
	{

		public int Numberofcar { get; set; }
		public int Numberofbus { get; set; }
		public int Numberofboat { get; set; }
		public int Numberofmotorcycle { get; set; }
		public int Numberofairplane { get; set; }
		public int TotalNumberofVehicles { get; set; }
		public int Totalnumberofwheels { get; set; }
		public double TotalMinutes { get; set; }
		public float TotalPrice{ get; set; }

		public Statistics(int cars, int buses, int boats, int moto, int airplanes, int Wheels, double totalminutes)
		{
			Numberofcar = cars;
			Numberofbus = buses;
			Numberofboat = boats;
			Numberofmotorcycle = moto;
			Numberofairplane = airplanes;
			Totalnumberofwheels = Wheels;
			TotalMinutes = totalminutes;

			TotalPrice = TotalCost();
			TotalNumberofVehicles = TotalVehicles();
		}

		private int TotalVehicles()
		{
			return Numberofcar + Numberofbus + Numberofboat + Numberofmotorcycle + Numberofairplane;
		}

		private float TotalCost()
		{
			return (float) TotalMinutes * 10;
		}

	}
}
		
	
