using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RestaSearch.Models
{
	public class AdresLokalu
	{
		public AdresLokalu()
		{
			this.Lokal = new HashSet<Lokal>();
		}
		public int AdresLokaluId { get; set; }
		public string Ulica { get; set; }
		public string Numer { get; set; }
		public string KodPocztowy { get; set; }

		public string MiejscowoscId { get; set; }
		public Miejscowosc Miejscowosc { get; set; }

		public virtual ICollection<Lokal> Lokal { get; private set; }



	}
}