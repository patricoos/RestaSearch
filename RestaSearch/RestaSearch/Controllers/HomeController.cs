using RestaSearch.DAL;
using RestaSearch.Models;
using RestaSearch.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaSearch.Controllers
{
    public class HomeController : Controller
    {

		private RSContext db = new RSContext();


        // GET: Home
        public ActionResult Index()
        {
			var kategorieRodzaj = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Rodzaj).ToList().Select(x => new KategoriaViewModel(x)).ToList();
			var kategorieKuchnia = db.Kategorie.Where(a => !a.Ukryty && a.Typ==Typ.Kuchnia).ToList().Select( x=> new KategoriaViewModel(x)).ToList();
			var kategorieDanie = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Danie).ToList().Select(x => new KategoriaViewModel(x)).ToList();
			var kategorieInne = db.Kategorie.Where(a => !a.Ukryty && a.Typ == Typ.Inne).ToList().Select(x => new KategoriaViewModel(x)).ToList();

			//	var miejscowosci = db.Miejscowosci.ToList();
			var nowosci = db.Lokale.Where(a => !a.Ukryty).OrderByDescending(a => a.DataDodania).Take(4).ToList();
			var promowane = db.Lokale.Where(a => !a.Ukryty && a.Promowany).OrderBy(a => Guid.NewGuid()).Take(4).ToList();
			var najwyswietlen = db.Lokale.Where(a => !a.Ukryty).OrderByDescending(a => a.Wyswietlenia).Take(4).ToList();

			var vm = new HomeViewModel()
			{
				Kategorie1 = kategorieRodzaj,
				Kategorie2 = kategorieKuchnia,
				Kategorie3 = kategorieDanie,
				Kategorie4 = kategorieInne,
		//		Miejscowosci=miejscowosci,
				Nowosci = nowosci,
				Promowane = promowane,
				NajWyswietlen = najwyswietlen
			};

			return View(vm);
        }
	}
}