using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaSearch.Models
{
	public class Kategoria
	{
		public int KategoriaId { get; set; }
		[StringLength(100)]
		public string NazwaKategorii { get; set; }
		public string OpisKategorii { get; set; }
		public string MyProperty { get; set; }
		public bool Ukryty { get; set; }
		public Typ Typ { get; set; }
		public string NazwaPlikuIkony { get; set; }

		public virtual ICollection<LokalKategoria> LokalKategoria { get; set; }
	}
	public enum Typ 
	{
		Rodzaj,
		Kuchnia,
		Danie,
		Inne
	}
}