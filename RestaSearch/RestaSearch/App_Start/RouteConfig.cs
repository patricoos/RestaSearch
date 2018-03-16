using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestaSearch
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				 name: "LokaleSzczegoly",
				 url: "lokal-{id}",
				 defaults: new { controller = "Lokale", action = "Szczegoly" });

			routes.MapRoute(
				 name: "LokaleList",
				 url: "kategoria/{nazwaKategorii}",
				 defaults: new { controller = "Lokale", action = "Lista" });

			routes.MapRoute(
				 name: "LokaleSzukaj",
				 url: "szukaj-{term}",
				 defaults: new { controller = "Lokale", action = "LokaleSzukaj" });

			//routes.MapRoute(
			//	name: "LokaleWyszykiwanie",
			//	url: "wysz-{term}",
			//	defaults: new { controller = "Lokale", action = "LokaleWyszykiwanie" });

			routes.MapRoute(
				name: "StronyStatyczne",
				url: "strona/{nazwa}.html",
				defaults: new { controller = "Home", action = "StronyStatyczne" });

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
