using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaSearch.Models
{
	public class Uzytkownik
	{
		public int UzytkownikId { get; set; }
		public string Nick { get; set; }
		public string Imie { get; set; }
		public string Nazwisko { get; set; }
		public string Ulica { get; set; }
		public string Numer { get; set; }
		public string KodPocztowy { get; set; }

		public string MiejscowoscId { get; set; }
		public Miejscowosc Miejscowosc { get; set; }

		[RegularExpression(@"(\+\d{2})*[\d\s-]+", ErrorMessage = "Błędny format numeru telefonu.")]
		public string Telefon { get; set; }

		[EmailAddress(ErrorMessage = "Błędny format adresu e-mail.")]
		public string Email { get; set; }
	}
}