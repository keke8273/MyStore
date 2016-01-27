using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNet.Identity;
using MyStore.Server.WebApi.Models;
using UserManagement;

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

        [Route("create")]
        public async Task<IHttpActionResult> CreateUser(CreateUserBindingModel createUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser()
            {
                UserName = createUserModel.Username,
                Email = createUserModel.Email,
                FirstName = createUserModel.FirstName,
                LastName = createUserModel.LastName,
                Level = 3,
                JoinDate = DateTime.Now.Date,
            };

            IdentityResult addUserResult = await this.AppUserManager.CreateAsync(user, createUserModel.Password);

            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }

            var locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

            return Created(locationHeader, ModelFactory.Create(user));
        }
    }
}