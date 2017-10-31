using BadApi.Helpers;
using BadApi.Models.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BadApi.Models.Account
{
	public class AccountModel : Domain.Account
	{
		public IEnumerable<ViewPostModel> Posts
		{
			get
			{
				return Task.Run(() => AccountHelper.Instance.GetAccountPosts(AccountId, 0, 10)).GetAwaiter().GetResult();
			}
		}
	}
}