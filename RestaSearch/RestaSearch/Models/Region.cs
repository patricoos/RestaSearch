using System.Collections.Generic;

namespace RestaSearch.Models
{
	public class Region
	{
		public int RegionId { get; set; }
		public string Nazwa { get; set; }
		public int PanstwoId { get; set; }

		public virtual Panstwo Panstwo { get; set; }

		public virtual ICollection<Miejscowosc> Miejscowosc { get; private set; }
	}
}