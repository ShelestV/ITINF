using ForestFiresML.Services;
using System.Text;

namespace ForestFiresML.Models
{
	class ForestFire
	{
		private int id;
		private string name;
		private int x;
		private int y;
		private Month month;
		private Day day;
		private double ffmc;
		private double dmc;
		private double dc;
		private double isi;
		private double temp;
		private double rh;
		private double wind;
		private double rain;
		private double area;

		public int Id
		{
			get => id;
		}

		public string Name
		{
			get => string.IsNullOrEmpty(name) ? id.ToString() : name;
			set => name = value;
		}

		public int X
		{
			get => x;
			set
			{
				if (1 <= value && value <= 9)
				{
					x = value;
				}
			}
		}

		public int Y
		{
			get => y;
			set
			{
				if (2 <= value && value <= 9)
				{
					y = value;
				}
			}
		}

		public Month Month
		{
			get => month;
			set => month = value;
		}

		public Day Day
		{
			get => day;
			set => day = value;
		}

		public double FFMC
		{
			get => ffmc;
			set
			{
				if (18.7 <= value && value <= 96.20)
				{
					ffmc = value;
				}
			}
		}

		public double DMC
		{
			get => dmc;
			set
			{
				if (1.1 <= value && value <= 291.3)
				{
					dmc = value;
				}
			}
		}

		public double DC
		{
			get => dc;
			set
			{
				if (7.9 <= value && value <= 860.6)
				{
					dc = value;
				}
			}
		}

		public double ISI
		{
			get => isi;
			set
			{
				if (0.0 <= value && value <= 56.10)
				{
					isi = value;
				}
			}
		}

		public double Temp
		{
			get => temp;
			set
			{
				if (2.2 <= value && value <= 33.30)
				{
					temp = value;
				}
			}
		}

		public double RH
		{
			get => rh;
			set
			{
				if (15.0 <= value && value <= 100.0)
				{
					rh = value;
				}
			}
		}

		public double Wind
		{
			get => wind;
			set
			{
				if (0.40 <= value && value <= 9.40)
				{
					wind = value;
				}
			}
		}

		public double Rain
		{
			get => rain;
			set
			{
				if (0.0 <= value && value <= 6.4)
				{
					rain = value;
				}
			}
		}

		public double Area
		{
			get => area;
			set
			{
				if (0.0 <= value && value <= 1090.84)
				{
					area = value;
				}
			}
		}

		public ForestFire()
		{
			id = ForestFireService.GetId();
		}

		public ForestFire(int id)
		{
			this.id = id;
		}

		public string ShortForm()
		{
			return Name + " X -> " + X + " Y -> " + Y + " " + Month + " " + Day;
		}

		public override string ToString()
		{
			var builder = new StringBuilder();
			builder.Append(Name).Append("\n");
			builder.Append("X: ").Append(X).Append("\n");
			builder.Append("Y: ").Append(Y).Append("\n");
			builder.Append("Month: ").Append(Month).Append("\n");
			builder.Append("Day: ").Append(Day).Append("\n");
			builder.Append("FFMC: ").Append(FFMC).Append("\n");
			builder.Append("DMC: ").Append(DMC).Append("\n");
			builder.Append("DC: ").Append(DC).Append("\n");
			builder.Append("ISI: ").Append(ISI).Append("\n");
			builder.Append("Temp: ").Append(Temp).Append("\n");
			builder.Append("RH: ").Append(RH).Append("\n");
			builder.Append("Rain: ").Append(Rain).Append("\n");
			builder.Append("Area: ").Append(Area).Append("\n");
			return builder.ToString();
		}
	}
}
