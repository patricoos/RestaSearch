using System.Collections.Generic;

namespace RestaSearch.Models
{
	public class Panstwo
	{
		public Panstwo()
			{
			this.Region = new HashSet<Region>();
			}
		public int PanstwoId { get; set; }
		public string Nazwa { get; set; }

		public virtual ICollection<Region> Region { get; private set; }

	}
}