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
		private static List<Post> _postCache = new List<Post>();

		[HttpGet]
		[Route("")]
		public async Task<Post> GetPost(long id)
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
					_postCache.Add(post);
					return post;
				}
			}
			catch (Exception ex)
			{
				return new Post { PostId = -1, AccountId = -1, Content = "No post found", DateTimeCreatedUtc = DateTime.UtcNow };
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
    }
}
