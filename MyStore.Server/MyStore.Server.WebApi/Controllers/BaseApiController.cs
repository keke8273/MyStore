using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using UserManagement;

namespace MyStore.Server.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        private ModelFactory _modelFactory;
        private readonly ApplicationUserManager _appUserManager;

        public BaseApiController()
        {
        }

        protected ApplicationUserManager AppUserManager { get { return _appUserManager ??  } }

        protected ModelFactory ModelFactory
        {
            get
            {
                if(_modelFactory == null)
                    _modelFactory = new ModelFactory(this.Request, this.AppUserManager);
            }
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
                return InternalServerError();

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}