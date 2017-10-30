using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BadApi.Controllers.Base
{
	public class BaseApiController : ApiController
	{
		protected IDbConnection GetConnection()
		{
			var connection = new SqlConnection("Server=localhost;Database=BadApi;User Id=badapi_user;Password=p@ssw0rd;");
			connection.Open();
			return connection;
		}
	}
}