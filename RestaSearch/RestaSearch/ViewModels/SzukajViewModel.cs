using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaSearch.ViewModels
{
	public class SzukajViewModel
	{
		public List<LokaleListViewModel> LokaleList { get; set; }
		public string MyLat { get; set; }
		public string MyLong { get; set; }
		public int Zoom { get; set; }
		public string CenterLat { get; set; }
		public string CenterLong { get; set; }
	}
}