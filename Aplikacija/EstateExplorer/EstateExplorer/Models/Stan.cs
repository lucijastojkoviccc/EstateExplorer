using System;
using System.ComponentModel.DataAnnotations;

namespace EstateExplorer.Models
{
	public class Stan : Nekretnina
	{
		
		public double BrojSoba { get; set; }
		public double CenaPoKvadratuBezPDV { get; set; }
		public int Sprat { get; set; }
		public string BrojUlaza { get; set; } = default!;
		public string Orijentacija { get; set; }=default!;
		public string? Opis { get; set; }
		
	}
}
