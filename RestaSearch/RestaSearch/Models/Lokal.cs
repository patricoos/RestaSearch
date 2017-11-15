using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace RestaSearch.Models
{
	public class Lokal
	{
		public Lokal()
		{
			this.Lokal_Kategoria = new HashSet<Lokal_Kategoria>();
		}

		public int LokalId { get; set; }
		[Required(ErrorMessage = "Wprowadz nazwę lokalu")]
		[StringLength(100)]
		public string NazwaLokalu { get; set; }
		[Required(ErrorMessage = "Wprowadz nazwę autora")]
		[StringLength(100)]
		public DateTime DataDodania { get; set; }
		public string Opis { get; set; }
		public string OpisSkrocony { get; set; }
		[RegularExpression(@"(\+\d{2})*[\d\s-]+", ErrorMessage = "Błędny format numeru telefonu.")]
		public string Telefon { get; set; }

		[RegularExpression(@"(\+\d{2})*[\d\s-]+", ErrorMessage = "Błędny format numeru telefonu.")]
		public string TelefonRezerwacja { get; set; }
		public bool Ukryty { get; set; }
		

		public DbGeography Lockalizacja { get; set; }


		public virtual ICollection<Lokal_Kategoria> Lokal_Kategoria { get; private set; }
	}
}