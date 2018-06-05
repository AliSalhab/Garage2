using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Garage2.Database;
using Garage2.Models;

namespace Garage2.Controllers
{
	public class VehiclesController : Controller
	{
		private VehicleDb db = new VehicleDb();
		private List<Vehicle> empty = new List<Vehicle>();

		// GET: Vehicles
		public ActionResult Index(string a, string b)
		{
			//TODO: can be refactorised
			//and add functionality to hide input field unless the reg radio button is pressed
			//all other radio buttons should show an enum drop down list
			switch (a)
			{
				case "Reg":
					if (db.Vehicle.Where(i => i.Reg == b).ToList().Count() > 0)
					{
						return View(db.Vehicle.Where(i => i.Reg == b).ToList());
					}
					else
					{
						ViewBag.output = ($".... {b} couldn't be found....");
						return View(empty);
					}
				case "Type":
					if (db.Vehicle.Where(i => i.Type.ToString() == b).ToList().Count() > 0)
					{
						return View(db.Vehicle.Where(i => i.Type.ToString().ToLower() == b.ToLower()).ToList());
					}
					else
					{
						ViewBag.output = ($".... {b} couldn't be found....");
						return View(empty);
					}
				case "Brand":
					if (db.Vehicle.Where(i => i.Brand.ToString() == b).ToList().Count() > 0)
					{
						return View(db.Vehicle.Where(i => i.Brand.ToString().ToLower() == b.ToLower()).ToList());
					}
					else
					{
						ViewBag.output = ($".... {b} couldn't be found....");
						return View(empty);
					}
				default:
					if (a == null && b == null)
					{ return View(db.Vehicle.ToList()); }
					else if (a == null || b == null)
					{
						ViewBag.output = ("....Please select a filter and write an input....");
						return View(empty);
					}
					else
					{
						ViewBag.output = ("....The garage is empty....");
						return View(empty);
					}
			}
		}

		[HttpPost, ActionName("search")]
		[ValidateAntiForgeryToken]
		public ActionResult searchConfirmed(string filter, string search)
		{

			return RedirectToAction("Index", new { a = filter, b = search });
		}

		public ActionResult Statistics()
		{
			//TODO: Refactorise and improve how to aquire these values
			Statistics statistics = new Statistics(
			db.Vehicle.Where(i => i.Type.ToString() == "Car").Count(),
			db.Vehicle.Where(i => i.Type.ToString() == "Bus").Count(),
			db.Vehicle.Where(i => i.Type.ToString() == "Boat").Count(),
			db.Vehicle.Where(i => i.Type.ToString() == "Motorcycle").Count(),
			db.Vehicle.Where(i => i.Type.ToString() == "Airplane").Count(),
			GetTotalAmountofWheels(),
			GetTotalAmountofMinutes()
			);

			return View(statistics);
		}

		public ActionResult GetChartImage()
		{
			//HACK: we shouldn't do like this!
			int cars = db.Vehicle.Where(i => i.Type.ToString() == "Car").Count();
			int buses = db.Vehicle.Where(i => i.Type.ToString() == "Bus").Count();
			int motorcycles = db.Vehicle.Where(i => i.Type.ToString() == "Motorcycle").Count();
			int boats = db.Vehicle.Where(i => i.Type.ToString() == "Boat").Count();
			int airplanes = db.Vehicle.Where(i => i.Type.ToString() == "Airplane").Count();

			var myChart = new Chart(width: 600, height: 400, theme: ChartTheme.Vanilla)
			.AddTitle("Overview")
			.AddSeries(
				chartType: "Column",
				xValue: new[] { "Cars", "Buses", "Motorcycles", "Boats", "Airplanes" },
				yValues: new[] { cars.ToString(), buses.ToString(), motorcycles.ToString(), boats.ToString(), airplanes.ToString() });
			return File(myChart.ToWebImage().GetBytes(), "Image/png");
		}

		private int GetTotalAmountofWheels()
		{
			var numberofwheels = from a in db.Vehicle.ToList()
								 select a.Wheels;
			int totalWheels = 0;
			foreach (var item in numberofwheels)
			{
				totalWheels += (int)item;
			}

			return totalWheels;
		}

		private double GetTotalAmountofMinutes()
		{
			var amountofminutes = from a in db.Vehicle.ToList()
								  select a.CheckIn;
			double totalminutes = 0;
			foreach (var item in amountofminutes)
			{
				TimeSpan parkedTime = DateTime.Now.Subtract(item);
				double parkedTimeInMinutes = parkedTime.TotalMinutes;

				totalminutes += parkedTimeInMinutes;
			}

			return totalminutes;
		}

		public ActionResult Receipt(Vehicle vehicle)
		{                                                                           //Checkout
			Receipt receipt = new Receipt(vehicle.Id, vehicle.Reg, vehicle.CheckIn, DateTime.Now);
			return View(receipt);
		}

		[HttpPost, ActionName("Receipt")]
		[ValidateAntiForgeryToken]
		public ActionResult ReceiptConfirmed()
		{
			return RedirectToAction("Index");
		}
		// GET: Vehicles/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Vehicle vehicle = db.Vehicle.Find(id);
			if (vehicle == null)
			{
				return HttpNotFound();
			}
			return View(vehicle);
		}

		// GET: Vehicles/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Vehicles/Create
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "Id,Reg,Type,Brand,Color,Wheels")] Vehicle vehicle)
		{
			if (ModelState.IsValid)
			{
				vehicle.CheckIn = GetDateNow();
				db.Vehicle.Add(vehicle);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(vehicle);
		}

		// GET: Vehicles/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Vehicle vehicle = db.Vehicle.Find(id);
			if (vehicle == null)
			{
				return HttpNotFound();
			}
			return View(vehicle);
		}

		// POST: Vehicles/Edit/5
		// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
		// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,Reg,Type,Brand,Color,Wheels,CheckIn")] Vehicle vehicle)
		{

			if (ModelState.IsValid)
			{
				//TODO: protect from date changed trhough the view
				db.Entry(vehicle).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(vehicle);
		}

		// GET: Vehicles/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Vehicle vehicle = db.Vehicle.Find(id);
			if (vehicle == null)
			{
				return HttpNotFound();
			}
			return View(vehicle);
		}

		// POST: Vehicles/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Vehicle vehicle = db.Vehicle.Find(id);
			db.Vehicle.Remove(vehicle);
			db.SaveChanges();
			return RedirectToAction("Receipt", vehicle);
		}

		private DateTime GetDateNow()
		{
			return DateTime.Now;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}


	}
}
