using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadApi.Models.Post
{
	public class CreatePostModel
	{
		public long AccountId { get; set; }
		public string Content { get; set; }
		public DateTime DateTimeCreatedUtc { get; set; }
	}
}