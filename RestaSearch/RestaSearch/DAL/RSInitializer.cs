using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RestaSearch.Models;

namespace RestaSearch.DAL
{
	public class RSInitializer : DropCreateDatabaseAlways<RSContext>
	{

		protected override void Seed(RSContext context)
		{
			SeedRestaSearchData(context);
			base.Seed(context);
		}

		public static void SeedRestaSearchData(RSContext context)
		{
			var kategorie = new List<Kategoria>
			{
				new Kategoria() { KategoriaId=1, NazwaKategorii="Restauracja",	 OpisKategorii="Opis123", Typ=Typ.Rodzaj },
				new Kategoria() { KategoriaId=2, NazwaKategorii="Pizzeria",		 OpisKategorii="Opis", Typ=Typ.Rodzaj },
				new Kategoria() { KategoriaId=3, NazwaKategorii="Fast Food",	 OpisKategorii="Opis", Typ=Typ.Rodzaj },
				new Kategoria() { KategoriaId=4, NazwaKategorii="Kebab",		 OpisKategorii="Fotele Opis", Typ=Typ.Rodzaj },
				new Kategoria() { KategoriaId=5, NazwaKategorii="Kawiarnia i Herbaciarnia",  OpisKategorii="Opis", Typ=Typ.Rodzaj },
				new Kategoria() { KategoriaId=6, NazwaKategorii="Cukiernia",	 OpisKategorii="Opis", Typ=Typ.Rodzaj },
				new Kategoria() { KategoriaId=7, NazwaKategorii="Piekarnia",	 OpisKategorii="Opis", Typ=Typ.Rodzaj },
				new Kategoria() { KategoriaId=8, NazwaKategorii="Food Track",	 OpisKategorii="Opis", Typ=Typ.Rodzaj },
				new Kategoria() { KategoriaId=9, NazwaKategorii="Pub",			 OpisKategorii="Opis", Typ=Typ.Rodzaj },
				new Kategoria() { KategoriaId=10, NazwaKategorii="Lodziarnie",	 OpisKategorii="Opis", Typ=Typ.Rodzaj },
				new Kategoria() { KategoriaId=10, NazwaKategorii="Cukiernie",    OpisKategorii="Opis", Typ=Typ.Rodzaj },
				new Kategoria() { KategoriaId=11, NazwaKategorii="Lasagne",    OpisKategorii="Opis", Typ=Typ.Danie },
				new Kategoria() { KategoriaId=12, NazwaKategorii="Pizza",    OpisKategorii="Opis", Typ=Typ.Danie },
				new Kategoria() { KategoriaId=13, NazwaKategorii="Kluski",    OpisKategorii="Opis", Typ=Typ.Danie },
				new Kategoria() { KategoriaId=14, NazwaKategorii="Włoska",    OpisKategorii="Opis", Typ=Typ.Kuchnia },
				new Kategoria() { KategoriaId=15, NazwaKategorii="Śląska",    OpisKategorii="Opis", Typ=Typ.Kuchnia },
				new Kategoria() { KategoriaId=16, NazwaKategorii="Azjatycka",    OpisKategorii="Opis", Typ=Typ.Kuchnia },
				new Kategoria() { KategoriaId=17, NazwaKategorii="Sala Weselna",    OpisKategorii="Opis", Typ=Typ.Inne },
				new Kategoria() { KategoriaId=18, NazwaKategorii="Hotel",    OpisKategorii="Opis", Typ=Typ.Inne },
				new Kategoria() { KategoriaId=19, NazwaKategorii="Imprezy Firmowe",    OpisKategorii="Opis", Typ=Typ.Inne },

			};
			kategorie.ForEach(k => context.Kategorie.Add(k));
			context.SaveChanges();


			var panstw = new List<Panstwo>
			{
				new Panstwo() { PanstwoId=1, Nazwa="Polska" }
			};
			panstw.ForEach(k => context.Panstwa.Add(k));
			context.SaveChanges();


			var regio = new List<Region>
			{
				new Region() {RegionId=1, Nazwa="Dolnośląskie",         PanstwoId= 1 },
				new Region() {RegionId=2, Nazwa="Kujawsko-Pomorskie",   PanstwoId= 1 },
				new Region() {RegionId=3, Nazwa="Lubelskie",            PanstwoId= 1 },
				new Region() {RegionId=4, Nazwa="Lubuskie",             PanstwoId= 1 },
				new Region() {RegionId=5, Nazwa="Łódzkie",              PanstwoId= 1 },
				new Region() {RegionId=6, Nazwa="Małopolskie",          PanstwoId= 1 },
				new Region() {RegionId=7, Nazwa="Mazowieckie",          PanstwoId= 1 },
				new Region() {RegionId=8, Nazwa="Opolskie",             PanstwoId= 1 },
				new Region() {RegionId=9, Nazwa="Podkarpackie",         PanstwoId= 1 },
				new Region() {RegionId=10, Nazwa="Podlaskie",           PanstwoId= 1 },
				new Region() {RegionId=11, Nazwa="Pomorskie",           PanstwoId= 1 },
				new Region() {RegionId=12, Nazwa="Śląskie",             PanstwoId= 1 },
				new Region() {RegionId=13, Nazwa="Świętokrzyskie",      PanstwoId= 1 },
				new Region() {RegionId=14, Nazwa="Warmińsko-Mazurskie", PanstwoId= 1 },
				new Region() {RegionId=15, Nazwa="Wielkopolskie",       PanstwoId= 1 },
				new Region() {RegionId=16, Nazwa="Zachodniopomorskie",  PanstwoId= 1 }
			};
			regio.ForEach(k => context.Regiony.Add(k));
			context.SaveChanges();

			var miejsc = new List<Miejscowosc>
			{
				new Miejscowosc() {MiejscowoscId= 1, Nazwa="Opole", RegionId=8},
				new Miejscowosc() {MiejscowoscId= 2, Nazwa="Zawada", RegionId=8},
				new Miejscowosc() {MiejscowoscId= 3, Nazwa="Warszawa", RegionId=7},
				new Miejscowosc() {MiejscowoscId= 4, Nazwa="Kraków", RegionId=6},
				new Miejscowosc() {MiejscowoscId= 5, Nazwa="Wrocław", RegionId=1},
				new Miejscowosc() {MiejscowoscId= 6, Nazwa="Turawa", RegionId=8}

			};
			miejsc.ForEach(k => context.Miejscowosci.Add(k));
			context.SaveChanges();

			

			var lokal = new List<Lokal>
			{
				new Lokal() {LokalId=1, NazwaLokalu="Restauracja PapaJack",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Promowany=true , Ukryty=false, Wyswietlenia=1, NazwaPlikuObrazka="1.jpg", NumerBudynku="7", MiejscowoscId=1, Ulica="Opolska",/* KategoriaId =1,*/      Lat="50.6672293", Long="17.924178"},
				new Lokal() {LokalId=2, NazwaLokalu="Smazalnia u Grubego",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Promowany=false, Ukryty=false, Wyswietlenia=2, NazwaPlikuObrazka="2.jpg", NumerBudynku="19", MiejscowoscId=6, Ulica="Promenada", /*KategoriaId= 2,*/	Lat="50.7348762", Long="18.126601"},	
				new Lokal() {LokalId=3, NazwaLokalu="Restauracja Rucola",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Promowany=false, Ukryty=false, Wyswietlenia=3, NazwaPlikuObrazka="3.jpg", NumerBudynku="45A", MiejscowoscId=2, Ulica="Oleska",/* KategoriaId=1,*/		Lat="50.7267197", Long="17.8012847"},
				new Lokal() {LokalId=4, NazwaLokalu="Restauracja Marcin",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Promowany=false, Ukryty=false, Wyswietlenia=4, NazwaPlikuObrazka="4.jpg", NumerBudynku="43", MiejscowoscId=2, Ulica="Oleska", /*KategoriaId=1,	*/		Lat="50.7138884", Long="17.993392"},
				new Lokal() {LokalId=5, NazwaLokalu="Restauracja PapaJack5",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Promowany=false, Ukryty=false, Wyswietlenia=5, NazwaPlikuObrazka="5.jpg", NumerBudynku="564", MiejscowoscId=6, Ulica="3 Maja",/* KategoriaId= 3, */   Lat="50.074539150401115", Long="18"},
				new Lokal() {LokalId=6, NazwaLokalu="Restauracja PapaJack6",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Promowany=false, Ukryty=false, Wyswietlenia=6, NazwaPlikuObrazka="6.jpg", NumerBudynku="45", MiejscowoscId=5, Ulica="Polna",/* KategoriaId=4,*/       Lat="50.7", Long="18.1" },
				new Lokal() {LokalId=7, NazwaLokalu="Pub Maska",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Promowany=true, Ukryty=false, Wyswietlenia=7, NazwaPlikuObrazka="7.jpg", NumerBudynku="987D", MiejscowoscId=4, Ulica="Ozimska" , /*KategoriaId= 5,*/				Lat="50.6676161", Long="17.9238671"},
				new Lokal() {LokalId=8, NazwaLokalu="Pizza Hut",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Promowany=true, Ukryty=false, Wyswietlenia=8, NazwaPlikuObrazka="8.jpg", NumerBudynku="8C", MiejscowoscId=3, Ulica="Piastowska", /*KategoriaId=6,	*/			Lat="50.6679834", Long="17.923822" },
				new Lokal() {LokalId=9, NazwaLokalu="Pod Arkadami",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Promowany=false, Ukryty=false, Wyswietlenia=9, NazwaPlikuObrazka="9.jpg", NumerBudynku="65", MiejscowoscId=2, Ulica="Pruszkowska", /*KategoriaId=7,*/			Lat="50.6679834", Long="17.923822" },
				new Lokal() {LokalId=10, NazwaLokalu="Lodziarnia Sopelek",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Promowany=true, Ukryty=false, Wyswietlenia=10, NazwaPlikuObrazka="10.jpg", NumerBudynku="1358", MiejscowoscId=1, Ulica="Krakowska", /*KategoriaId=8,*/  Lat="50.6672634", Long="17.9246151" },
				new Lokal() {LokalId=11, NazwaLokalu="Restauracja Bajka",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Promowany=false, Ukryty=false, Wyswietlenia=11, NazwaPlikuObrazka="11.jpg", NumerBudynku="89", MiejscowoscId=5, Ulica="Rzeczna", /*KategoriaId=9,*/		Lat="50.6954790", Long="18.2864856" },
				new Lokal() {LokalId=12, NazwaLokalu="Manekin",Opis="Opis PapaJack", DataDodania=DateTime.Now, OpisSkrocony="Opis S PJ", Telefon="666555444", TelefonRezerwacja="111222333", Promowany=true, Ukryty=false, Wyswietlenia=12, NazwaPlikuObrazka="12.jpg", NumerBudynku="7/8", MiejscowoscId=1, Ulica="plac Wolności", /*KategoriaId=10,*/         Lat="50.6670015", Long="17.924366" }

			};
			lokal.ForEach(k => context.Lokale.Add(k));
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