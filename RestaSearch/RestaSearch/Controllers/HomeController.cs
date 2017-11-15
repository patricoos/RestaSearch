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

		private RSContext db = new RSContext();


        // GET: Home
        public ActionResult Index()
        {
			var listaKategorii = db.Kategorie.ToList();
		
            return View();
        }
    }
}