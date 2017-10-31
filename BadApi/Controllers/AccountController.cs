using BadApi.Controllers.Base;
using BadApi.Domain;
using BadApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

using Dapper;
using BadApi.Models.Account;

namespace BadApi.Controllers
{
	[Authorize]
	[RoutePrefix("api/account")]
    public class AccountController : BaseApiController
    {
        // GET: Account
		[AllowAnonymous]
		[HttpGet]
		[Route("login")]
        public async Task<Account> Login([FromUri] LoginModel model)
        {
            var connection = GetConnection();
			var users = await connection.QueryAsync<Account>("SELECT * FROM dbo.Account;");
			return users.First(u => u.EmailAddress == model.EmailAddress && u.Password == model.Password);
        }

		[AllowAnonymous]
		[HttpGet]
		[Route("{id}")]
		public async Task<AccountModel> GetAccount(long id)
		{

			var connection = GetConnection();
			var account = await connection.QueryFirstOrDefaultAsync<Account>("SELECT * FROM dbo.Account WHERE AccountId = @AccountId;", new { AccountId = id });
			return new AccountModel { AccountId = account.AccountId, EmailAddress = account.EmailAddress, Password = account.Password };
		}
    }
}