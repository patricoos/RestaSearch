﻿using System;
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
			this.LokalKategoria = new HashSet<LokalKategoria>();
		}

		public int LokalId { get; set; }
		[Required(ErrorMessage = "Wprowadz nazwę lokalu")]
		[StringLength(100)]
		public string NazwaLokalu { get; set; }
		[Required(ErrorMessage = "Wprowadz nazwę autora")]
		public DateTime DataDodania { get; set; }
		public string Opis { get; set; }
		public string OpisSkrocony { get; set; }
		[RegularExpression(@"(\+\d{2})*[\d\s-]+", ErrorMessage = "Błędny format numeru telefonu.")]
		public string Telefon { get; set; }

		[RegularExpression(@"(\+\d{2})*[\d\s-]+", ErrorMessage = "Błędny format numeru telefonu.")]
		public string TelefonRezerwacja { get; set; }
		public bool Ukryty { get; set; }

		public virtual AdresLokalu AdresLokalu { get; private set; }
		


		public virtual ICollection<LokalKategoria> LokalKategoria { get; private set; }
	}
}