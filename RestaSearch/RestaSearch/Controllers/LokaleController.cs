﻿using RestaSearch.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using RestaSearch.Models;

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
		public ActionResult Lista(string nazwaKategorii)
		{
			var kategoria = db.Kategorie.Include("Lokale").Where(k => k.NazwaKategorii.ToUpper() == nazwaKategorii.ToUpper()).Single();
			var lokale = kategoria.Lokale.ToList();
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





		//public ActionResult LokaleSzukajLokalizacja(string y, string x)
		//{
		//	double lokUzWys = Konwert(y);
		//	double lokUzSzer = Konwert(x);
		//	double wektor;

		//	var lokale = db.Lokale.Where(a => !a.Ukryty).ToList();
		//	lokale.Add(new Lokal() { NazwaKategorii = "crank arm", KategoriaId = 1234 });


		//	return View("Lista", lokale);
		//}

		//public double Konwert(string value) {
		//	double lok = Convert.ToDouble(value);
		//	return lok;
		//}

	}
}