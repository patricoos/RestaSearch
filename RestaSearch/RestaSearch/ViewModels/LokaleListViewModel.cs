using RestaSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaSearch.ViewModels
{
	public class LokaleListViewModel
	{
		public int LokalId { get; set; }
		public string NazwaLokalu { get; set; }

		public string OpisSkrocony { get; set; }

		public string Ulica { get; set; }
		public string NumerBudynku { get; set; }
		public string NumerLokalu { get; set; }
		public string NazwMiejscowosc { get; set; }

		public string NazwaPlikuObrazka { get; set; }

		public string Lat { get; set; }
		public string Long { get; set; }
		public double Dystans { get; set; }

		public LokaleListViewModel()
		{
		}

		public LokaleListViewModel(int lokalId)
		{
			this.LokalId = lokalId;
		}

			public LokaleListViewModel(Lokal lokal, double Dystans)
		{
			LokalId = lokal.LokalId;
			NazwaLokalu = lokal.NazwaLokalu;
			OpisSkrocony = lokal.OpisSkrocony;
			Ulica = lokal.Ulica;
			NumerBudynku = lokal.NumerBudynku;
			NumerLokalu = lokal.NumerLokalu;
			NazwMiejscowosc = lokal.NazwMiejscowosc;
			NazwaPlikuObrazka = lokal.NazwaPlikuObrazka;
			Lat = lokal.Lat;
			Long = lokal.Long;

			this.Dystans= Dystans;
		}
		public LokaleListViewModel(Lokal lokal)
		{
			LokalId = lokal.LokalId;
			NazwaLokalu = lokal.NazwaLokalu;
			OpisSkrocony = lokal.OpisSkrocony;
			Ulica = lokal.Ulica;
			NumerBudynku = lokal.NumerBudynku;
			NumerLokalu = lokal.NumerLokalu;
			NazwMiejscowosc = lokal.NazwMiejscowosc;
			NazwaPlikuObrazka = lokal.NazwaPlikuObrazka;
			Lat = lokal.Lat;
			Long = lokal.Long;
		}
	}
}