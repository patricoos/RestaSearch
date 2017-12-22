﻿using System.Collections.Generic;

namespace RestaSearch.Models
{
	public class Miejscowosc
	{
		public int MiejscowoscId { get; set; }
		public string Nazwa { get; set; }

		public string RegionId { get; set; }
		public Region Region { get; set; }

		public virtual ICollection<AdresLokalu> AdresLokalu { get; private set; }

		public virtual ICollection<Uzytkownik> Uzytkownik { get; private set; }
	}
}