using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadApi.Domain
{
	public class Account
	{
		public long AccountId { get; set; }
		public string EmailAddress { get; set; }
		public string Password { get; set; }
	}
}