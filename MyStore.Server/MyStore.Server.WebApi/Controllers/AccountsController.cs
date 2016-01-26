using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.ApplicationInsights.Extensibility.Implementation;

namespace MyStore.Server.WebApi.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : BaseApiController
    {
        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            return Ok(this.AppUserManager.Users.ToList().Select(u => this.ModelFactory.Create(u)));
        }

        [Route("user/{id:guid}")]
        public async Task<IHttpActionResult> GetUser(string id)
        {
            var user = await this.AppUserManager.FindByIdAsync(id);

            if (user != null)
                return Ok(this.ModelFactory.Create(user));

            return NotFound();
        }
    }
}