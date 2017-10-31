using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadApi.Models.Post
{
	public class RankPostModel
	{
		public long PostId { get; set; }
		public long AccountId { get; set; }
		public bool IsPositive { get; set; }
	}
}