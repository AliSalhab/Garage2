using Garage2.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2.Models
{
	public class Vehicle
	{
		public int Id { get; set; }
		public Brand Brand { get; set; }
		public TypeVehicle Type { get; set; }
		[Custom_Validations,Required]
		public string Reg { get; set; }		
		public Color Color { get; set; }
		public Numberofwheels Wheels { get; set; }
		public DateTime CheckIn { get; set;}
		
	}
	public enum TypeVehicle
	{
		Car,
		Motorcycle,
		Airplane,
		Boat,
		Bus
	}
	public enum Brand
	{
		Volvo,
		Abarth,
		Acura,
		[Display(Name = "Alfa Romeo")]
		AlfaRomeo,
		[Display(Name = "Aston Martin")]
		AstonMartin,
		Audi,
		Bentley,
		BMW,
		Buick
	}
	public enum Color
	{
		Amaranth,
		Amber,
		Amethyst,
		Apricot,
		Aquamarine,
		Azure,
		Beige,
		Black,
		Blue,
		Blush,
		Bronze,
		Brown,
		Burgundy,
		Byzantium,
		Carmine,
		Cerise,
		Cerulean,
		Champagne,
		Chocolate,
		Coffee,
		Copper,
		Coral,
		Crimson,
		Cyan,
		Emerald,
		Erin,
		Gold,
		Gray,
		Green,
		Harlequin,
		Indigo,
		Ivory,
		Jade,
		Lavender,
		Lemon,
		Lilac,
		Lime,
		Magenta,
		Maroon,
		Mauve,
		Ocher,
		Olive,
		Orange,
		Orchid,
		Peach,
		Pear,
		Periwinkle,
		Pink,
		Plum,
		Puce,
		Purple,
		Raspberry,
		Red,
		Rose,
		Ruby,
		Salmon,
		Sangria,
		Sapphire,
		Scarlet,
		Silver,
		Tan,
		Taupe,
		Teal,
		Turquoise,
		Violet,
		Viridian,
		White,
		Yellow
	}
	public enum Numberofwheels
	{
		Zero,
		Two,
		Three,
		Four,
		Six,
		Eight,
		Twelve
	}

}