using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaSearch.Tools
{
	public static class DoubleExtensions
	{
		public static double ToRadians(this double val)
		{
			return (Math.PI / 180) * val;
		}
	}
}