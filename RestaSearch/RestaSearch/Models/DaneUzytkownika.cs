using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaSearch.Models
{
	public class DaneUzytkownika
	{
		public string Imie { get; set; }

		public string Nazwisko { get; set; }

		public string Ulica { get; set; }
		public string NumerBudynku { get; set; }
		public string NumerLokalu { get; set; }
		public string KodPocztowy { get; set; }

		public string Miejscowosc { get; set; }

		[RegularExpression(@"(\+\d{2})*[\d\s-]+", ErrorMessage = "Błędny format numeru telefonu.")]
		public string Telefon { get; set; }

		[EmailAddress(ErrorMessage = "Błędny format adresu e-mail.")]
		public string Email { get; set; }

	}
}