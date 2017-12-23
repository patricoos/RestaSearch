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
			var kategorie = db.Kategorie.ToList();
			var nowosci = db.Lokale.Where(a => !a.Ukryty).OrderByDescending(a => a.DataDodania).Take(4).ToList();
			var promowane = db.Lokale.Where(a => !a.Ukryty && a.Promowany).OrderBy(a => Guid.NewGuid()).Take(4).ToList();
			var najwyswietlen = db.Lokale.Where(a => !a.Ukryty).OrderByDescending(a => a.Wyswietlenia).Take(4).ToList();

			var vm = new HomeViewModel()
			{
				Kategorie = kategorie,
				Nowosci = nowosci,
				Promowane = promowane,
				NajWyswietlen = najwyswietlen
			};

			return View(vm);
        }
    }
}