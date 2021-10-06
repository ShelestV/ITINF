using CarEvaluationML.Models.Enums;
using System;

namespace CarEvaluationML.Models
{
	class Car
	{
		private int numberOfDoors;
		private int numberOfPersons;

		private string idName;
		
		public int Id { get; set; }
		public string IdName
		{
			get => string.IsNullOrEmpty(idName) ? Id.ToString() : idName;
			set => idName = value;
		}

		public Buying Buying { get; set; }
		public Maint Maint { get; set; }

		public int NumberOfDoors 
		{
			get => numberOfDoors;
			set
			{
				if (value > 4)
				{
					numberOfDoors = 5;
				}
				else if (value >= 2)
				{
					numberOfDoors = value;
				}
				else
				{
					throw new ArgumentException();
				}
			}
		}

		public string NumberOfDoorsString
		{
			get => numberOfDoors > 4 ? "5 or more" : numberOfDoors.ToString();
		}

		public int NumberOfPersons 
		{
			get => numberOfDoors;
			set
			{
				if (value > 4)
				{
					numberOfPersons = 5;
				}
				else if (value == 2 || value == 4)
				{
					numberOfPersons = value;
				}
				else
				{ 
					throw new ArgumentException();
				}
			} 
		}

		public string NumberOfPersonsString
		{
			get => numberOfPersons > 4 ? "5 or more" : numberOfPersons.ToString();
		}

		public LugBoot LugBoot { get; set; }
		public Safety Safety { get; set; }
		public Class Class { get; set; }

		public override string ToString()
		{
			return IdName + "(" + Class + "): " +
				Buying + " (buying), " +
				Maint + " (maint), " +
				NumberOfDoorsString + " doors, " +
				NumberOfPersonsString + " persons, " +
				LugBoot + " (lug boot), " +
				Safety + " (safety);";
		}
	}
}
