using System;
using System.Net.Http;
using System.Web.Http.Routing;

namespace UserManagement
{
    public class ModelFactory
    {
        private UrlHelper _urlHelper;
        private ApplicationUserManager _applicationUserManager;

        public ModelFactory(HttpRequestMessage request, ApplicationUserManager appUserManager)
        {
            _urlHelper = new UrlHelper(request);
            _applicationUserManager = appUserManager;
        }

        public UserReturnModel Create(ApplicationUser appUser)
        {
            return new UserReturnModel
            {
                Url = _urlHelper.Link("")
            };

        }
    }
}
