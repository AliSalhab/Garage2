using Garage2.Database;
using Garage2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Garage2.Validations
{


	public class Custom_Validations : ValidationAttribute
	{

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			VehicleDb db = new VehicleDb();
			List<Vehicle> model;
			Vehicle vehicle = (Vehicle)validationContext.ObjectInstance;

			model = db.Vehicle.Where(i => i.Reg == value.ToString() && i.Id != vehicle.Id).ToList();

			if (model.Count != 0)
			{
				model.Clear();
				return new ValidationResult("the registration Number is in used ... try another");
			}
			else
			{
				model.Clear();
				return ValidationResult.Success;
			}


		}

	}
}