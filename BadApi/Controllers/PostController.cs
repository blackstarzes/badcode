using BadApi.Controllers.Base;
using BadApi.Domain;
using BadApi.Models.Post;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace BadApi.Controllers
{
	[RoutePrefix("api/post")]
    public class PostController : BaseApiController
    {
		private static List<ViewPostModel> _postCache = new List<ViewPostModel>();

		[HttpGet]
		[Route("")]
		public async Task<ViewPostModel> GetPost(long id)
		{
			try
			{
				if (_postCache.Any(p => p.PostId == id))
				{
					return _postCache.First(p => p.PostId == id);
				}

				using (var connection = GetConnection())
				{
					var post = await connection.QueryFirstAsync<Post>(string.Format("SELECT * FROM dbo.Post WHERE PostId = {0}", id));
					var myRank = await connection.QueryFirstOrDefaultAsync<Rank>(string.Format("SELECT * FROM dbo.Rank WHERE PostId = {0} AND AccountId = 1", id));
					var positives = await connection.ExecuteScalarAsync<int>(string.Format("SELECT COUNT(*) FROM dbo.Rank WHERE PostId = {0} AND IsPositive = 1", id));
					var negatives = await connection.ExecuteScalarAsync<int>(string.Format("SELECT COUNT(*) FROM dbo.Rank WHERE PostId = {0} AND IsPositive = 0", id));
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
					_postCache.Add(postModel);
					return postModel;
				}
			}
			catch (Exception ex)
			{
				return new ViewPostModel { PostId = -1, AccountId = -1, Content = "No post found", DateTimeCreatedUtc = DateTime.UtcNow, MyRank = null, Positives = 0, Negatives = 0 };
			}
		}

		[Route("create")]
		[HttpPost]
		public async Task<long> CreatePost([FromBody] CreatePostModel model)
		{
			try
			{
				using (var connection = GetConnection())
				{
					model.DateTimeCreatedUtc.Subtract(TimeZoneInfo.Local.BaseUtcOffset); // Adjust the timezone from the client to UTC
					return await connection.ExecuteScalarAsync<long>(string.Format("INSERT INTO dbo.Post (AccountId, Content, DateTimeCreatedUtc) VALUES ({0}, '{1}', '{2:yyyy-MM-dd HH:mm:ss}')", model.AccountId, model.Content, model.DateTimeCreatedUtc));
				}
			}
			catch (Exception ex)
			{
				throw ex;
				return -1;
			}
		}

		[Route("rank")]
		[HttpPost]
		public async Task RankPost([FromBody] RankPostModel model)
		{
			try
			{
				using (var connection = GetConnection())
				{
					if (connection.QueryFirstOrDefault<Rank>("SELECT * FROM dbo.Rank WHERE PostId = @PostId AND AccountId = @AccountId", new { model.PostId, model.AccountId }) == null)
					{
						connection.ExecuteAsync("INSERT INTO dbo.Rank ( PostId, AccountId, IsPositive ) VALUES ( @PostId, @AccountId, @IsPositive )", model);
					}
					else
					{
						connection.ExecuteAsync("UPDATE dbo.Rank SET IsPositive = @IsPositive WHERE PostId = @PostId AND AccountId = @AccountId", model);
					}
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
    }
}
