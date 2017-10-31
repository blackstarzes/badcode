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

		public IEnumerable<ViewPostModel> Top5Posts
		{
			get
			{
				var allPosts = Task.Run(() => AccountHelper.Instance.GetAccountPosts(AccountId, 0, int.MaxValue))
					.GetAwaiter()
					.GetResult()
					.ToList();

				// Get the top 5 posts by positivity
				var postsRequired = 5;
				var topPosts = new List<ViewPostModel>();
				while (postsRequired > 0 && allPosts.Any())
				{
					ViewPostModel post = null;
					foreach (var p in allPosts)
					{
						if (post == null || post.Positives < p.Positives)
						{
							post = p;
						}
					}

					allPosts.Remove(post);
					topPosts.Add(post);
					postsRequired--;
				}

				return topPosts;
			}
		}
	}
}