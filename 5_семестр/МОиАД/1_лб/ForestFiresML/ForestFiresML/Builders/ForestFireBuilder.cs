using ForestFiresML.Models;

namespace ForestFiresML.Builders
{
	class ForestFireBuilder
	{
		private ForestFire forestFire;

		public ForestFireBuilder()
		{
			Reset();
		}

		public void Reset()
		{
			forestFire = new ForestFire();
		}

		public void BuildX(int x)
		{
			forestFire.X = x;
		}

		public void BuildY(int y)
		{
			forestFire.Y = y;
		}

		public void BuildMonth(Month month)
		{
			forestFire.Month = month;
		}

		public void BuildDay(Day day)
		{
			forestFire.Day = day;
		}

		public void BuildFFMC(double ffmc)
		{
			forestFire.FFMC = ffmc;
		}

		public void BuildDMC(double dmc)
		{
			forestFire.DMC = dmc;
		}

		public void BuildDC(double dc)
		{
			forestFire.DC = dc;
		}

		public void BuildISI(double isi)
		{
			forestFire.ISI = isi;
		}

		public void BuildTemp(double temp)
		{
			forestFire.Temp = temp;
		}

		public void BuildRH(double rh)
		{
			forestFire.RH = rh;
		}

		public void BuildWind(double wind)
		{
			forestFire.Wind = wind;
		}

		public void BuildRain(double rain)
		{
			forestFire.Rain = rain;
		}

		public void BuildArea(double area)
		{
			forestFire.Area = area;
		}

		public ForestFire GetForestFire()
		{
			return forestFire;
		}
	}
}
