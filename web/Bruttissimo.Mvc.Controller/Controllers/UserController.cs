using System;
using System.Web.Mvc;
using Bruttissimo.Common;
using Bruttissimo.Common.Mvc;
using Bruttissimo.Domain;
using Bruttissimo.Mvc.Model;

namespace Bruttissimo.Mvc.Controller
{
    public class UserController : ExtendedController
    {
        private readonly IFormsAuthentication formsAuthentication;
        private readonly IAuthenticationService authenticationService;

        public UserController(IFormsAuthentication formsAuthentication, IAuthenticationService authenticationService)
        {
            if (formsAuthentication == null)
            {
                throw new ArgumentNullException("formsAuthentication");
            }
            if (authenticationService == null)
            {
                throw new ArgumentNullException("authenticationService");
            }
            this.formsAuthentication = formsAuthentication;
            this.authenticationService = authenticationService;
        }

        [HttpGet]
        [NotAjax]
        public ActionResult Login(string returnUrl)
        {
            UserLoginModel model = new UserLoginModel {ReturnUrl = returnUrl};
            return View(model);
        }

        [HttpPost]
        [NotAjax]
        [ValidateAntiForgeryToken(Salt = "g#CzX3w")]
        public ActionResult Login(UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return InvalidModelState(model);
            }
            AuthenticationResult result = authenticationService.AuthenticateWithCredentials(model.Email, model.Password);
            return ProcessAuthenticationResult(model, result);
        }

        [NotAjax]
        [ValidateInput(false)]
        public ActionResult Authenticate(UserLoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return InvalidModelState(model);
            }
            Func<AuthenticationResult, ActionResult> process = r => ProcessAuthenticationResult(model, r);
            switch (model.Source)
            {
                case AuthenticationSource.Facebook:
                {
                    return process(authenticationService.AuthenticateWithFacebook(model.UserId, model.AccessToken));
                }
                case AuthenticationSource.Twitter:
                {
                    return process(authenticationService.AuthenticateWithTwitter(model.UserId, model.DisplayName));
                }
                case AuthenticationSource.OpenId:
                {
                    string route = urlHelper.PublicAction("Authenticate", "User", new
                    {
                        source = "openid",
                        returnUrl = model.ReturnUrl // the actual returnUrl where the user will be ultimately redirected to.
                    });
                    Uri returnUrl = new Uri(route); // this is the returnUrl for the provider to complete authentication.
                    return process(authenticationService.AuthenticateWithOpenId(model.OpenIdProvider, returnUrl));
                }
                default:
                {
                    return process(null);
                }
            }
        }

        [NonAction]
        internal ActionResult ProcessAuthenticationResult(UserLoginModel model, AuthenticationResult result)
        {
            if (result == null)
            {
                return RedirectToAction("Login"); // sanity
            }
            switch (result.Status)
            {
                case ConnectionStatus.Canceled:
                {
                    return RedirectToAction("Login");
                }
                case ConnectionStatus.Faulted:
                {
                    ModelState.AddModelError("Authentication", Common.Resources.User.AuthenticationFaulted);
                    return InvalidModelState(model);
                }
                case ConnectionStatus.RedirectToProvider:
                {
                    return result.Action;
                }
                case ConnectionStatus.InvalidCredentials:
                {
                    ModelState.AddModelError("Authentication", Common.Resources.User.AuthenticationError);
                    return InvalidModelState(model);
                }
                case ConnectionStatus.Authenticated:
                {
                    if (result.UserId.HasValue)
                    {
                        if (!model.ReturnUrl.NullOrEmpty())
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                    break;
                }
            }
            return RedirectToAction("Login"); // sanity
        }

        [HttpGet]
        [NotAjax]
        public ActionResult Logout()
        {
            formsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
