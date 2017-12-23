using RestaSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaSearch.ViewModels
{
	public class HomeViewModel
	{
		public IEnumerable<Kategoria> Kategorie { get; set; }
		public IEnumerable<Lokal> Nowosci { get; set; }
		public IEnumerable<Lokal> Promowane { get; set; }
	//	public IEnumerable<Lokal> NajOcena { get; set; }
		public IEnumerable<Lokal> NajWyswietlen { get; set; }
	}
}