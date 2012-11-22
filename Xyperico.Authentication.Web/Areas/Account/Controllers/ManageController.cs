using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using Xyperico.Authentication.Web.Areas.Account.Models;
using Xyperico.Web.Mvc;
using Microsoft.Web.WebPages.OAuth;

namespace Xyperico.Authentication.Web.Areas.Account.Controllers
{
  public class ManageController : Xyperico.Web.Mvc.Controller
  {
    #region Registration

    [AllowAnonymous]
    [PageLayout("Simple")]
    public ActionResult Register()
    {
      return View();
    }


    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [PageLayout("Simple")]
    public ActionResult Register(RegisterModel model)
    {
      if (User.Identity.IsAuthenticated)
        return RedirectToHome();

      if (ModelState.IsValid)
      {
        // Attempt to register the user
        try
        {
          WebSecurity.CreateUserAndAccount(model.UserName, model.Password, new { EMail = model.EMail });
          WebSecurity.Login(model.UserName, model.Password);
          return Configuration.Settings.RegisterSuccessUrl.Redirect();
        }
        catch (MembershipCreateUserException e)
        {
          ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
        }
      }

      // If we got this far, something failed, redisplay form
      return View(model);
    }


    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [PageLayout("Simple")]
    public ActionResult RegisterExternal(RegisterExternalModel model)
    {
      string provider = null;
      string providerUserId = null;

      if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
        return RedirectToHome();

      if (ModelState.IsValid)
      {
        // Attempt to register the user
        try
        {
          OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
          OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);
          return Configuration.Settings.RegisterSuccessUrl.Redirect();
        }
        catch (MembershipCreateUserException e)
        {
          ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
        }
      }

      // If we got this far, something failed, redisplay form
      return View(model);
    }


    #endregion


    private static string ErrorCodeToString(MembershipCreateStatus createStatus)
    {
      // See http://go.microsoft.com/fwlink/?LinkID=177550 for
      // a full list of status codes.
      switch (createStatus)
      {
        case MembershipCreateStatus.DuplicateUserName:
          return "User name already exists. Please enter a different user name.";

        case MembershipCreateStatus.DuplicateEmail:
          return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

        case MembershipCreateStatus.InvalidPassword:
          return "The password provided is invalid. Please enter a valid password value.";

        case MembershipCreateStatus.InvalidEmail:
          return "The e-mail address provided is invalid. Please check the value and try again.";

        case MembershipCreateStatus.InvalidAnswer:
          return "The password retrieval answer provided is invalid. Please check the value and try again.";

        case MembershipCreateStatus.InvalidQuestion:
          return "The password retrieval question provided is invalid. Please check the value and try again.";

        case MembershipCreateStatus.InvalidUserName:
          return "The user name provided is invalid. Please check the value and try again.";

        case MembershipCreateStatus.ProviderError:
          return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

        case MembershipCreateStatus.UserRejected:
          return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

        default:
          return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
      }
    }

  }
}
