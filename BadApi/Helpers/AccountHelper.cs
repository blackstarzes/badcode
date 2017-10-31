using BadApi.Domain;
using BadApi.Models.Post;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BadApi.Helpers
{
	public class AccountHelper
	{
		private static AccountHelper instance;

		public static AccountHelper Instance {
			get {
				if (instance == null) {
					instance = new AccountHelper();
				}
				return instance;
			}
		}

		private IDbConnection _connection;

		public AccountHelper()
		{
			_connection = new SqlConnection("Server=localhost;Database=BadApi;User Id=badapi_user;Password=p@ssw0rd;");
			_connection.Open();
		}

		public async Task<IEnumerable<ViewPostModel>> GetAccountPosts(long accountId, int start, int count)
		{
			var posts = await _connection.QueryAsync<Post>("SELECT TOP (@Top) * FROM dbo.Post WHERE AccountId = @AccountId", new { Top = start + count, AccountId = accountId });
			var models = new List<ViewPostModel>();
			foreach (var post in posts.Skip(start))
			{
				var myRank = await _connection.QueryFirstOrDefaultAsync<Rank>("SELECT * FROM dbo.Rank WHERE PostId = @PostId AND AccountId = 1", new { post.PostId });
				var positives = await _connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.Rank WHERE PostId = @PostId AND IsPositive = 1", new { post.PostId });
				var negatives = await _connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM dbo.Rank WHERE PostId = @PostId AND IsPositive = 0", new { post.PostId });
				var postModel = new ViewPostModel
				{
					PostId = post.PostId,
					AccountId = post.AccountId,
					Content = post.Content,
					DateTimeCreatedUtc = post.DateTimeCreatedUtc,
					MyRank = myRank?.IsPositive,
					Positives = positives,
					Negatives = negatives
				};
				models.Add(postModel);
			}
			return models;
		}
	}
}