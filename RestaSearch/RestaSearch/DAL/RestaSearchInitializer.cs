using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RestaSearch.Models;

namespace RestaSearch.DAL
{
	public class RestaSearchInitializer : DropCreateDatabaseAlways<RestaSearchContext>
	{

		protected override void Seed(RestaSearchContext context)
		{
			SeedRestaSearchData(context);
			base.Seed(context);
		}

		public static void SeedRestaSearchData(RestaSearchContext context)
		{
			var kategorie = new List<Kategoria>
			{
				new Kategoria() { KategoriaId=1, NazwaKategorii="Restauracja",	NazwaPlikuIkony="przykladowe.png", OpisKategorii="Opis" },
				new Kategoria() { KategoriaId=2, NazwaKategorii="Pizzeria",		NazwaPlikuIkony="przykladowe.png", OpisKategorii="Opis" },
				new Kategoria() { KategoriaId=3, NazwaKategorii="Fast Food",	NazwaPlikuIkony="przykladowe.png", OpisKategorii="Opis" },
				new Kategoria() { KategoriaId=4, NazwaKategorii="Kebab",		NazwaPlikuIkony="przykladowe.png", OpisKategorii="Fotele Opis" },
				new Kategoria() { KategoriaId=5, NazwaKategorii="Kawiarnia i Herbaciarnia", NazwaPlikuIkony="przykladowe.png", OpisKategorii="Opis" },
				new Kategoria() { KategoriaId=6, NazwaKategorii="Cukiernia",	NazwaPlikuIkony="przykladowe.png", OpisKategorii="Opis" },
				new Kategoria() { KategoriaId=7, NazwaKategorii="Piekarnia",	NazwaPlikuIkony="przykladowe.png", OpisKategorii="Opis" },
				new Kategoria() { KategoriaId=8, NazwaKategorii="Food Track",	NazwaPlikuIkony="przykladowe.png", OpisKategorii="Opis" },
				new Kategoria() { KategoriaId=9, NazwaKategorii="Lodziarnia",	NazwaPlikuIkony="przykladowe.png", OpisKategorii="Opis" },
				new Kategoria() { KategoriaId=10, NazwaKategorii="Pub",			NazwaPlikuIkony="przykladowe.png", OpisKategorii="Opis" }
			};

			kategorie.ForEach(k => context.Kategorie.Add(k));
			context.SaveChanges();

			var produkty = new List<Lokal>
			{
				new Lokal() {LokalId=1, NazwaLokalu="Restauracja PapaJack",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Ukryty=false},
				new Lokal() {LokalId=2, NazwaLokalu="Restauracja PapaJack",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Ukryty=false},
				new Lokal() {LokalId=3, NazwaLokalu="Restauracja PapaJack",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Ukryty=false},
				new Lokal() {LokalId=4, NazwaLokalu="Restauracja PapaJack",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Ukryty=false},
				new Lokal() {LokalId=5, NazwaLokalu="Restauracja PapaJack",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Ukryty=false},
				new Lokal() {LokalId=6, NazwaLokalu="Restauracja PapaJack",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Ukryty=false},
				new Lokal() {LokalId=7, NazwaLokalu="Restauracja PapaJack",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Ukryty=false},
				new Lokal() {LokalId=8, NazwaLokalu="Restauracja PapaJack",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Ukryty=false},
				new Lokal() {LokalId=9, NazwaLokalu="Restauracja PapaJack",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Ukryty=false},
				new Lokal() {LokalId=10, NazwaLokalu="Restauracja PapaJack",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Ukryty=false},
				new Lokal() {LokalId=11, NazwaLokalu="Restauracja PapaJack",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Ukryty=false},
				new Lokal() {LokalId=12, NazwaLokalu="Restauracja PapaJack",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Ukryty=false,}

			};
			produkty.ForEach(k => context.Lokale.Add(k));
			context.SaveChanges();

			var lokkat = new List<LokalKategoria>
			{
				new LokalKategoria() {LokalId=1, KategoriaId=1 },
				new LokalKategoria() {LokalId=2, KategoriaId=2 },
				new LokalKategoria() {LokalId=3, KategoriaId=3 },
				new LokalKategoria() {LokalId=4, KategoriaId=4 },
				new LokalKategoria() {LokalId=5, KategoriaId=5 },
				new LokalKategoria() {LokalId=6, KategoriaId=6 },
				new LokalKategoria() {LokalId=7, KategoriaId=7 },
				new LokalKategoria() {LokalId=8, KategoriaId=8 },
				new LokalKategoria() {LokalId=9, KategoriaId=9 },
				new LokalKategoria() {LokalId=10, KategoriaId=10 },
				new LokalKategoria() {LokalId=11, KategoriaId=1 },
				new LokalKategoria() {LokalId=12, KategoriaId=2 },
				new LokalKategoria() {LokalId=1, KategoriaId=3 },
				new LokalKategoria() {LokalId=2, KategoriaId=4 },
				new LokalKategoria() {LokalId=3, KategoriaId=5 }

			};
			lokkat.ForEach(k => context.LokaleKategorie.Add(k));
			context.SaveChanges();

		}

	}
}