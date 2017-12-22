using System.Collections.Generic;

namespace RestaSearch.Models
{
	public class Panstwo
	{
		public int PanstwoId { get; set; }
		public string Nazwa { get; set; }

		public virtual ICollection<Region> Region { get; private set; }

	}
}