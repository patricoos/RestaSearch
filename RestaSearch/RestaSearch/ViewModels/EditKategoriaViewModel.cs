using RestaSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaSearch.ViewModels
{
	public class EditKategoriaViewModel
	{
		public Kategoria kategoria { get; set; } 
		public bool? Potwierdzenie { get; set; }
	}
}