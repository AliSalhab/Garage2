namespace Garage2.Migrations
{
	using Garage2.Models;
	using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Garage2.Database.VehicleDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Garage2.Database.VehicleDb context)
        {
			context.Vehicle.AddOrUpdate(
				p=>p.Reg,
				new Vehicle {}
				);

		}
    }
}
