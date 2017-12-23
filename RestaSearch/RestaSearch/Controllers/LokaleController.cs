using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestaSearch.Controllers
{
    public class LokaleController : Controller
    {
        // GET: Lokale
        public ActionResult Index()
        {
            return View();
        }
		public ActionResult Lista()
		{
			return View();
		}
	}
}