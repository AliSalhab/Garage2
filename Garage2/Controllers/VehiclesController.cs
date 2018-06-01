using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Garage2.Database;
using Garage2.Models;

namespace Garage2.Controllers
{
	public class VehiclesController : Controller
	{
		private VehicleDb db = new VehicleDb();

		// GET: Vehicles
		public ActionResult Index()
		{
			return View(db.Vehicle.ToList());
		}

		public ActionResult Receipt(Vehicle vehicle)
		{
			ViewBag.CheckinDate = vehicle.CheckIn;
			ViewBag.CheckoutDate = GetDateNow();
			ViewBag.Regnum = vehicle.Reg;
			var tuples = Price(vehicle.CheckIn, GetDateNow());
			ViewBag.TotalTime = tuples.Item1;
			ViewBag.Price = tuples.Item2;
			return View();
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
		public ActionResult Create([Bind(Include = "Id,Reg,Type,Brand,Color,Numberofwheels")] Vehicle vehicle)
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
		public ActionResult Edit([Bind(Include = "Id,Reg,Type,Brand,Color,Numberofwheels,CheckIn")] Vehicle vehicle)
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

		private Tuple<string, string> Price(DateTime checkIn, DateTime checkOut)
		{
			TimeSpan parkedTime = checkOut.Subtract(checkIn);
			double parkedTimeInMinutes = parkedTime.TotalMinutes;
			//var hour = Math.Floor(parkedTimeInMinutes / 60);
			//var Minute = Math.Ceiling(parkedTimeInMinutes - (hour * 60));
			var TotalPrice = Math.Ceiling(parkedTimeInMinutes * 10);
			return Tuple.Create(parkedTime.ToString(@"d\.h\:mm"), TotalPrice.ToString());
		}
	}
}
