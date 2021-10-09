using System;
using System.Collections.Generic;

namespace tmo_2lb
{
	static class ListMerger
	{
		public static List<int> MergeIntLists(List<int> first, List<int> second) 
		{
			if (IsNullOrEmpty(first) || IsNullOrEmpty(second))
				throw new ArgumentException();

			var result = new List<int>();
			for (int i = 0; i < Math.Min(first.Count, second.Count); ++i)
				result.Add(first[i] + second[i]);

			if (first.Count != second.Count)
			{
				var greaterList = first.Count > second.Count ? first : second;
				var smallerList = first.Count < second.Count ? first : second;

				for (int i = smallerList.Count; i < greaterList.Count; ++i)
					result.Add(greaterList[i]);
			}

			return result;
		}

		private static bool IsNullOrEmpty(List<int> list) => list == null || list.Count == 0;
	}
}
