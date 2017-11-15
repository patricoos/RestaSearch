using RestaSearch.DAL;
using RestaSearch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaSearch.Controllers
{
    public class HomeController : Controller
    {

		private RestaSearchContext db = new RestaSearchContext();


        // GET: Home
        public ActionResult Index()
        {
			Kategoria kategoria = new Kategoria { NazwaKategorii = "Pizzeria", OpisKategorii = "pizza pizza" };
			db.Kategorie.Add(kategoria);
			db.SaveChanges();
		
            return View();
        }
    }
}