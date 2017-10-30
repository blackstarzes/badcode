using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BadApi.Models.Account
{
	public class LoginModel
	{
		public string EmailAddress { get; set; }
		public string Password { get; set; }
	}
}