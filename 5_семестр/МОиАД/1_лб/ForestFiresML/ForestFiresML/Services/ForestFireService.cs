namespace ForestFiresML.Services
{
	class ForestFireService
	{
		private static int idCounter = 0;

		public static int GetId()
		{
			return idCounter++;
		}
	}
}
