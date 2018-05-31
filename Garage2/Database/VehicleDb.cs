using Garage2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Garage2.Database
{
	public class VehicleDb : DbContext
	{
		public VehicleDb() :base("DbConnection")
		{

		}
		public DbSet<Vehicle> Vehicle { get; set; }
	}
}