using System.Collections.Generic;

namespace _2_lb_TPR
{
	interface IMyList<T>
	{
		List<Alternative> Alternatives { get; }
		void Add(T value);
		void Remove(T value);
		T FindByStringOrReturnNull(string str);
		bool IsContained(string str);
	}
}
