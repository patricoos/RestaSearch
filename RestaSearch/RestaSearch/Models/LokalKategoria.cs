using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaSearch.Models
{
	public class LokalKategoria
	{
		public int Lokal_KategoriaId { get; set; }
		public int LokalId { get; set; }
		public int KategoriaId { get; set; }
		public virtual Lokal Lokal { get; set; }
		public virtual Kategoria Kategoria { get; set; }
	}
}