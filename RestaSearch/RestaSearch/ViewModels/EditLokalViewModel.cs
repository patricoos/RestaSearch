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
		//public List<CheckBoxViewModel> Kategorie { get; set; }
		public List<KategoriaViewModel> Kategorie { get; set; }
		public List<Miejscowosc> Miejscowosci { get; set; }

		public bool? Potwierdzenie { get; set; }
	}
}