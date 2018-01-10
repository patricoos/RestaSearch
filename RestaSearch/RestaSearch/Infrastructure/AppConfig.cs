using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace RestaSearch.Infrastructure
{
	public class AppConfig
	{

		private static string _ikonyKategoriFolderWzgledny = ConfigurationManager.AppSettings["IkonyKategoriiFolder"];

		public static string IkonyKategoriFolderWzgledny
		{
			get
			{
				return _ikonyKategoriFolderWzgledny;
			}
		}
		private static string _obrazkiLokaliFolderWzgledny = ConfigurationManager.AppSettings["ObrazkiLokaliFolder"];

		public static string ObrazkiLokaliFolderWzgledny
		{
			get
			{
				return _obrazkiLokaliFolderWzgledny;
			}
		}
	}
}