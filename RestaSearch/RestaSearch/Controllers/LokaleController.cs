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

			var selectedKategorie = model.Kategorie1.Where(x => x.Checked).Select(x => x.Id).ToList();
			selectedKategorie.AddRange(model.Kategorie2.Where(x => x.Checked).Select(x => x.Id).ToList());
			selectedKategorie.AddRange(model.Kategorie3.Where(x => x.Checked).Select(x => x.Id).ToList());
			selectedKategorie.AddRange(model.Kategorie4.Where(x => x.Checked).Select(x => x.Id).ToList());

			var lokale = db.Lokale.Where(x => x.LokalKategoria.Any(b => selectedKategorie.Contains(b.KategoriaId))).ToList();

			if (model.Latitude != null & model.Longitude != null)
			{
				double Lat = Double.Parse(model.Latitude, System.Globalization.CultureInfo.InvariantCulture);
				double Long = Double.Parse(model.Longitude, System.Globalization.CultureInfo.InvariantCulture);
				var location = new Location(Lat, Long);

				lokale = lokale.OrderBy(x => new Location(Double.Parse(x.Lat, System.Globalization.CultureInfo.InvariantCulture), Double.Parse(x.Long, System.Globalization.CultureInfo.InvariantCulture)).GetDistance(location)).ToList();

			}
			return View(lokale);
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
			var kategorie = db.Kategorie.ToList();
			return PartialView("_KategorieMenu", kategorie);
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
			return View("Lista", lokale);
		}
	}
}