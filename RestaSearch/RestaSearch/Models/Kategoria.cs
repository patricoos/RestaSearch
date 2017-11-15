using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestaSearch.Models
{
	public class Kategoria
	{
		public Kategoria()
		{
			this.Lokal_Kategoria = new HashSet<Lokal_Kategoria>();
		}

		public int KategoriaId { get; set; }
		[StringLength(100)]
		public string NazwaKategorii { get; set; }
		public string OpisKategorii { get; set; }
		public string NazwaPlikuIkony { get; set; }
		public bool Ukryty { get; set; }
		public byte Status { get; set; }


		public virtual ICollection<Lokal_Kategoria> Lokal_Kategoria { get; private set; }
	}
}