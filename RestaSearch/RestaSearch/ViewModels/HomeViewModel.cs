using RestaSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaSearch.ViewModels
{
	public class HomeViewModel
	{
		//public List<Kategoria> Kategorie { get; set; }
		public List<KategoriaViewModel> Kategorie { get; set; }
		public IEnumerable<Lokal> Nowosci { get; set; }
		public IEnumerable<Lokal> Promowane { get; set; }
	
		public IEnumerable<Lokal> NajWyswietlen { get; set; }

		public string Latitude { get; set; }
		public string Longitude { get; set; }

		//	public IEnumerable<Miejscowosc> Miejscowosci { get; set; }
		//	public IEnumerable<Lokal> NajOcena { get; set; }
	}
}