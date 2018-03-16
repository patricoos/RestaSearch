using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaSearch.Models
{
	public class KategoriaViewModel
	{
		public int Id { get; set; }

		public string Nazwa { get; set; }

		public bool Checked { get; set; }

		public Typ Typ { get; set; }

		public KategoriaViewModel()
		{
		}

		public KategoriaViewModel(Kategoria kategoria)
		{
			Id = kategoria.KategoriaId;
			Nazwa = kategoria.NazwaKategorii;
			Typ = kategoria.Typ;
		}

	}
}