using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace RestaSearch.Models
{
	public class Lokal
	{

		public int LokalId { get; set; }
		[Required(ErrorMessage = "Wprowadz nazwę lokalu")]
		[StringLength(100)]
		public string NazwaLokalu { get; set; }
		[Required(ErrorMessage = "Wprowadz nazwę autora")]
		public DateTime DataDodania { get; set; }
		public string Opis { get; set; }
		public string OpisSkrocony { get; set; }
		[RegularExpression(@"(\+\d{2})*[\d\s-]+", ErrorMessage = "Błędny format numeru telefonu.")]
		public string Telefon { get; set; }
		[RegularExpression(@"(\+\d{2})*[\d\s-]+", ErrorMessage = "Błędny format numeru telefonu.")]
		public string TelefonRezerwacja { get; set; }

		public string Ulica { get; set; }
		public string NumerBudynku { get; set; }
		public string NumerLokalu { get; set; }
		public string KodPocztowy { get; set; }
		public string NazwMiejscowosc { get; set; }

		public string NazwaPlikuObrazka { get; set; }
		public bool Ukryty { get; set; }
		public int Wyswietlenia { get; set; }

		public string Lat { get; set; }
		public string Long { get; set; }

		public string Link { get; set; }
		public string FbLink { get; set; }

		//public int MiejscowoscId { get; set; }
		//public virtual Miejscowosc Miejscowosc { get; set; }

		public StatusLokalu StatusLokalu { get; set; }
		


		public string UserId { get; set; }
		public virtual ApplicationUser User { get; set; }

		public virtual ICollection<LokalKategoria> LokalKategoria { get; set; }

		//public int UzytkownikId { get; set; }
		//public virtual Uzytkownik Uzytkownik { get; set; }

	}
	public enum StatusLokalu
		{
			Nowy,
			Widoczny,
			Promowany,
			Zablokowany
		}
}