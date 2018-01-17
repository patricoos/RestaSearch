using RestaSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaSearch.ViewModels
{
	public class EditLokalViewModel
	{
		public Lokal Lokal { get; set; }
		public List<KategoriaViewModel> Kategorie1 { get; set; }
		public List<KategoriaViewModel> Kategorie2 { get; set; }
		public List<KategoriaViewModel> Kategorie3 { get; set; }
		public List<KategoriaViewModel> Kategorie4 { get; set; }
		public List<Miejscowosc> Miejscowosci { get; set; }

		public bool? Potwierdzenie { get; set; }
	}
}