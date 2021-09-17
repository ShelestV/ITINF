using System;
using System.Linq;
using System.Threading.Tasks;
using WinesML.Models;

namespace WinesML.Services
{
	public class WineService
	{
		private Wine wine;
		
		public WineService(Wine wine)
		{
			this.wine = wine;
		}
	}
}
