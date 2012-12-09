using System.Web.Mvc;
using System.Web.Security;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Xyperico.Authentication.Web.Areas.Account.Models;
using Xyperico.Base.Exceptions;
using Xyperico.Web.Mvc;


namespace Xyperico.Authentication.Web.Areas.Account.Controllers
{
  public class RegistrationController : Xyperico.Web.Mvc.Controller
  {
    #region Dependencies

    public IUserRepository UserRepository { get; set; }
    public IUserNameValidator UserNameValidator { get; set; }

    #endregion


    #region Registration

    [AllowAnonymous]
    [PageLayout("Simple")]
    public ActionResult Register()
    {
      if (User.Identity.IsAuthenticated)
        return RedirectToHome();

      RegisterModel model = new RegisterModel();
      return View(model);
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
        catch (MembershipCreateUserException ex)
        {
          ModelState.AddModelError("", ErrorCodeToString(ex.StatusCode));
        }
        catch (InvalidUserNameException)
        {
          ModelState.AddModelError("UserName", _.Account.InvalidUserName);
        }
      }

      // If we got this far, something failed, redisplay form
      return View(model);
    }


    [HttpGet]
    [AllowAnonymous]
    public ActionResult CheckUserName(string value) // The name "value" is specified by the common AJAX verifier
    {
      System.Threading.Thread.Sleep(500);
      if (!UserNameValidator.IsValidUserName(value))
        return Json(new { Ok = false, Message = _.Account.InvalidUserName }, JsonRequestBehavior.AllowGet);

      try
      {
        UserRepository.GetByUserName(value);
        return Json(new { Ok = false, Message = _.Account.UserNameNotAvailable }, JsonRequestBehavior.AllowGet);
      }
      catch (MissingResourceException)
      {
      }

      return Json(new 
      { 
        Ok = true, 
        Message = _.Account.UserNameAvailable,
        CheckedValue = value
      }, JsonRequestBehavior.AllowGet);
    }


    [HttpGet]
    [AllowAnonymous]
    public ActionResult CheckEMail(string value) // The name "value" is specified by the common AJAX verifier
    {
      System.Threading.Thread.Sleep(500);

      try
      {
        UserRepository.GetByEMail(value);
        return Json(new { Ok = false, Message = _.Account.EMailAlreadyInUse }, JsonRequestBehavior.AllowGet);
      }
      catch (MissingResourceException)
      {
      }

      return Json(new 
      { 
        Ok = true, 
        Message = _.Account.EMailNotInUse,
        CheckedValue = value
      }, JsonRequestBehavior.AllowGet);
    }


    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    [PageLayout("Simple")]
    public ActionResult RegisterUnknownExternal(RegisterUnknownExternalModel model)
    {
      string provider = null;
      string providerUserId = null;

      if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
        return RedirectToHome();

      model.ProviderName = provider;
      model.ProviderUserId = providerUserId;
      model.EMail = model.ProviderEMail;

      if (!string.IsNullOrEmpty(model.IsRedirect))
      {
        ModelState.Clear();
        return View(model);
      }

      if (ModelState.IsValid)
      {
        // Attempt to register the user
        try
        {
          OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
          User user = UserRepository.GetByUserName(model.UserName);
          if (!string.IsNullOrEmpty(model.Password))
            user.ChangePassword(model.Password, Xyperico.Authentication.Configuration.Settings.GetPasswordPolicy());
          if (!string.IsNullOrEmpty(model.EMail))
            user.ChangeEMail(model.EMail);
          UserRepository.Update(user);
          OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);
          return Configuration.Settings.RegisterSuccessUrl.Redirect();
        }
        catch (DuplicateKeyException ex)
        {
          if (ex.Key == "UserName")
            ModelState.AddModelError("", "User name is already in use");
          else if (ex.Key == "EMail")
            ModelState.AddModelError("", "EMail is already in use");
          else if (ex.Key == "ExternalLogin")
            ModelState.AddModelError("", "External login is already in use");
          else
            ModelState.AddModelError("", "Unknown error");
        }
        catch (InvalidUserNameException)
        {
          ModelState.AddModelError("UserName", _.Account.InvalidUserName);
        }
        catch (MembershipCreateUserException ex)
        {
          ModelState.AddModelError("", ErrorCodeToString(ex.StatusCode));
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
          return _.Account.UserNameNotAvailable;

        case MembershipCreateStatus.DuplicateEmail:
          return _.Account.DuplicateEMail;

        case MembershipCreateStatus.DuplicateProviderUserKey:
          return _.Account.DuplicateProviderUserKey;

        // The rest are not used by this SimplerMembershipProvider

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
