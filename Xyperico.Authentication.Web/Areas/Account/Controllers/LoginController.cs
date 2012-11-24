using System.Web.Mvc;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using Xyperico.Authentication.Web.Areas.Account.Models;
using WebMatrix.WebData;
using Xyperico.Web.Mvc;


namespace Xyperico.Authentication.Web.Areas.Account.Controllers
{
  public class LoginController : Xyperico.Web.Mvc.Controller
  {
    #region Standard login

    [HttpGet]
    [PageLayout("Simple")]
    public ActionResult show(string returnUrl)
    {
      ViewBag.ReturnUrl = returnUrl;
      return View();
    }


    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [PageLayout("Simple")]
    public ActionResult show(LoginModel model, string returnUrl)
    {
      if (ModelState.IsValid && WebSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
      {
        return Configuration.Settings.LoginSuccessUrl.Redirect();
      }

      // If we got this far, something failed, redisplay form
      ModelState.AddModelError("", _.Account.WrongPassword);
      return View(model);
    }


    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public ActionResult logout()
    {
      WebSecurity.Logout();

      return RedirectToHome();
    }

    #endregion


    #region External logins (OpenID, Facebook etc.)

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public ActionResult ExternalLogin(string provider, string returnUrl)
    {
      return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
    }


    [HttpGet]
    [AllowAnonymous]
    [PageLayout("Simple")]
    public ActionResult ExternalLoginCallback(string returnUrl)
    {
      AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
      if (!result.IsSuccessful)
      {
        return RedirectToAction("ExternalLoginFailure");
      }

      if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
      {
        return RedirectToLocal(returnUrl);
      }

      if (User.Identity.IsAuthenticated)
      {
        // If the current user is logged in add the new account
        OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
        return RedirectToLocal(returnUrl);
      }
      else
      {
        // User is new, redirect to registration page
        string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
        RegisterExternalModel model = new RegisterExternalModel
        {
          ProviderName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName,
          ExternalLoginData = loginData,
          ProviderUserName = result.UserName,
          EMail = result.UserName,
          ReturnUrl = returnUrl
        };
        return View("../Manage/RegisterExternal", model);
      }
    }


    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public ActionResult FIXME_UNUSED_ExternalLoginConfirmation(RegisterExternalModel model, string returnUrl)
    {
      string provider = null;
      string providerUserId = null;

      if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
      {
        return RedirectToAction("Manage");
      }

      if (ModelState.IsValid)
      {
        // Insert a new user into the database
        //using (UsersContext db = new UsersContext())
        {
          //UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
          // Check if user already exists
          //if (user == null)
          {
            // Insert name into the profile table
            //db.UserProfiles.Add(new UserProfile { UserName = model.UserName });
            //db.SaveChanges();

            OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
            OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

            return RedirectToLocal(returnUrl);
          }
          //else
          //{
          //  ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name.");
          //}
        }
      }

      ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
      ViewBag.ReturnUrl = returnUrl;
      return View(model);
    }


    [ChildActionOnly]
    [AllowAnonymous]
    public ActionResult ExternalLoginsList(string returnUrl)
    {
      ViewBag.ReturnUrl = returnUrl;
      return PartialView("_ExternalLoginsList", OAuthWebSecurity.RegisteredClientData);
    }

    #endregion


    #region Helpers

    private ActionResult RedirectToLocal(string returnUrl)
    {
      if (Url.IsLocalUrl(returnUrl))
      {
        return Redirect(returnUrl);
      }
      else
      {
        return RedirectToHome();
      }
    }

    #endregion
  }
}
