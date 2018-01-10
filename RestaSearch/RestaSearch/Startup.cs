using Owin;
using RestaSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace RestaSearch
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);

		}
	}
}