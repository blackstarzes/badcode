using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadApi.Domain
{
	public class Post
	{
		public long PostId { get; set; }
		public long AccountId { get; set; }
		public string Content { get; set; }
		public DateTime DateTimeCreatedUtc { get; set; }
	}
}