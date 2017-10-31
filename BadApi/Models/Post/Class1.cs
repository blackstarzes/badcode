using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadApi.Models.Post
{
	public class ViewPostModel
	{
		public long PostId { get; set; }
		public long AccountId { get; set; }
		public string Content { get; set; }
		public DateTime DateTimeCreatedUtc { get; set; }
		public bool? MyRank { get; set; }
		public int Positives { get; set; }
		public int Negatives { get; set; }
	}
}