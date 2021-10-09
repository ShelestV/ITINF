using System.Collections.Generic;

namespace ShelestML_2lb.Models
{
	class MatrixRow
	{
		public IEnumerable<string> Row { get; set; }
		public bool Positivity { get; set; }
	}
}
