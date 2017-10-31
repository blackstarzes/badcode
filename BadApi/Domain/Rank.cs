using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadApi.Domain
{
	public class Rank
	{
		public long PostId { get; set; }
		public long AccountId { get; set; }
		public bool IsPositive { get; set; }
	}
}