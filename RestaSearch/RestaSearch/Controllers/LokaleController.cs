using RestaSearch.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using RestaSearch.Models;
using RestaSearch.ViewModels;
using Klubowo.Core.Models.Locations;

namespace RestaSearch.Controllers
{
	public class LokaleController : Controller
	{
		private RSContext db = new RSContext();
		// GET: Lokale
		public ActionResult Index()
		{
			return View();
		}
		public ActionResult Lista(HomeViewModel model)
		{
			double MaxLat = -85;
			double MinLat = 85;
			double MaxLong = -180;
			double MinLong = 180;
			double M;
			int Z = 0;

			var selectedKategorie = model.Kategorie1.Where(x => x.Checked).Select(x => x.Id).ToList();
			selectedKategorie.AddRange(model.Kategorie2.Where(x => x.Checked).Select(x => x.Id).ToList());
			selectedKategorie.AddRange(model.Kategorie3.Where(x => x.Checked).Select(x => x.Id).ToList());
			selectedKategorie.AddRange(model.Kategorie4.Where(x => x.Checked).Select(x => x.Id).ToList());

			var lokale = db.Lokale.Where(x => x.LokalKategoria.Any(b => selectedKategorie.Contains(b.KategoriaId))).ToList();
			List<LokaleListViewModel> vm = new List<LokaleListViewModel>();

			if (model.Latitude != null & model.Longitude != null)
			{
				double Lat = Double.Parse(model.Latitude, System.Globalization.CultureInfo.InvariantCulture);
				double Long = Double.Parse(model.Longitude, System.Globalization.CultureInfo.InvariantCulture);
				var location = new Location(Lat, Long);


				lokale = lokale.Where(a => !a.Ukryty).OrderBy(x => new Location(Double.Parse(x.Lat, System.Globalization.CultureInfo.InvariantCulture), Double.Parse(x.Long, System.Globalization.CultureInfo.InvariantCulture)).GetDistance(location)).ToList();


				foreach (Lokal lokal in lokale)
				{
					double Y = Double.Parse(lokal.Lat, System.Globalization.CultureInfo.InvariantCulture);
					double X = Double.Parse(lokal.Long, System.Globalization.CultureInfo.InvariantCulture);
					vm.Add(new LokaleListViewModel(
						lokal,
						Math.Round((new Location(Y, X).GetDistance(location)) / 1000, 2)
				));
					if (Y > MaxLat) MaxLat = Y;
					if (Y < MinLat) MinLat = Y;
					if (X > MaxLong) MaxLong = X;
					if (X < MinLong) MinLong = X;
				}
				if (Lat > MaxLat) MaxLat = Lat;
				if (Lat < MinLat) MinLat = Lat;
				if (Long > MaxLong) MaxLong = Long;
				if (Long < MinLong) MinLong = Long;
			}

			else {
				foreach (Lokal lokal in lokale)
				{
					double Y = Double.Parse(lokal.Lat, System.Globalization.CultureInfo.InvariantCulture);
					double X = Double.Parse(lokal.Long, System.Globalization.CultureInfo.InvariantCulture);
					vm.Add(new LokaleListViewModel(lokal));
					if (Y > MaxLat) MaxLat = Y;
					if (Y < MinLat) MinLat = Y;
					if (X > MaxLong) MaxLong = X;
					if (X < MinLong) MinLong = X;
				}
			}
		

			if ((MaxLat - MinLat) > (MaxLong - MinLong)) M = MaxLat - MinLat;
			else M = MaxLong - MinLong;


			if (M >= 0 && M <= 0.0037) Z = 17;
			else if (M > 0.0037 && M <= 0.0070) Z = 16;
			else if (M > 0.0070 && M <= 0.0130) Z = 15;
			else if (M > 0.0130 && M <= 0.0290) Z = 14;
			else if (M > 0.0290 && M <= 0.0550) Z = 13;
			else if (M > 0.0550 && M <= 0.1200) Z = 12;
			else if (M > 0.1200 && M <= 0.4640) Z = 10;
			else if (M > 0.4640 && M <= 1.8580) Z = 8;
			else if (M > 1.8580 && M <= 3.5310) Z = 7;
			else if (M > 3.5310 && M <= 7.3367) Z = 6;
			else if (M > 7.3367 && M <= 14.222) Z = 5;
			else if (M > 14.222 && M <= 28.000) Z = 4;
			else if (M > 28.000 && M <= 58.000) Z = 3;
			else Z = 1;

			var result = new SzukajViewModel();
			result.LokaleList = vm;
			result.MyLat = model.Latitude;
			result.MyLong = model.Longitude;
			result.Zoom = Z;
			result.CenterLat = Math.Round(((MinLat + MaxLat) / 2), 4).ToString("F").Replace(",", ".");
			result.CenterLong = Math.Round(((MinLong + MaxLong) / 2), 4).ToString("F").Replace(",", ".");

			return View(result);
		}
		public ActionResult Szczegoly(int id)
		{
			var lokal = db.Lokale.Find(id);
			lokal.Wyswietlenia++;
			db.SaveChanges();
			return View(lokal);
		}
		public ActionResult KategorieMenu()
		{
			var kategorieRodzaj = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Rodzaj).ToList().Select(x => new KategoriaViewModel(x)).ToList();
			var kategorieKuchnia = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Kuchnia).ToList().Select(x => new KategoriaViewModel(x)).ToList();
			var kategorieDanie = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Danie).ToList().Select(x => new KategoriaViewModel(x)).ToList();
			var kategorieInne = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Inne).ToList().Select(x => new KategoriaViewModel(x)).ToList();
			var vm = new HomeViewModel()
			{
				Kategorie1 = kategorieRodzaj,
				Kategorie2 = kategorieKuchnia,
				Kategorie3 = kategorieDanie,
				Kategorie4 = kategorieInne
			};
			return PartialView("_KategorieMenu", vm);
		}

		public ActionResult LokalePodpowiedzi(string term)
		{
			var lokale = db.Lokale.Where(a => !a.Ukryty && a.NazwaLokalu.ToLower().Contains(term.ToLower()))
						.Take(5).Select(a => new { label = a.NazwaLokalu });

			return Json(lokale, JsonRequestBehavior.AllowGet);
		}

		public ActionResult LokaleSzukaj(string term)
		{
			var lokale = db.Lokale.Where(a => !a.Ukryty && a.NazwaLokalu.ToLower().Contains(term.ToLower())).ToList();
			return View(lokale);
		}
	}
}