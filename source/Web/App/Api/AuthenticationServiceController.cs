using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Solution.Application.Applications;
using Solution.CrossCutting.AspNetCore.Attributes;
using Solution.CrossCutting.Utils;
using Solution.Model.Models;

namespace Solution.Web.App
{
    [ApiController]
    [RouteController]
    public class AuthenticationServiceController : ControllerBase
    {
        public AuthenticationServiceController(IAuthenticationApplication authenticationApplication)
        {
            AuthenticationApplication = authenticationApplication;
        }

        private IAuthenticationApplication AuthenticationApplication { get; }

        [AllowAnonymous]
        [HttpPost]
        [RouteAction]
        public string Authenticate(AuthenticationModel authentication)
        {
            return AuthenticationApplication.Authenticate(authentication);
        }

        [HttpPost]
        [RouteAction]
        public void Logout()
        {
            AuthenticationApplication.Logout(User.GetAuthenticatedUserId());
        }
    }
}
