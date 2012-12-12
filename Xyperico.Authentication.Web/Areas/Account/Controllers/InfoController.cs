using System.Web.Mvc;
using Xyperico.Base.Exceptions;


namespace Xyperico.Authentication.Web.Areas.Account.Controllers
{
  public class InfoController : Xyperico.Web.Mvc.Controller
  {
    #region Dependencies

    public IUserRepository UserRepository { get; set; }

    #endregion


    [ChildActionOnly]
    [AllowAnonymous]
    public ActionResult AccountBox()
    {
      try
      {
        User u = UserRepository.GetByUserName(User.Identity.Name);
        return PartialView("_AccountBox", u);
      }
      catch (MissingResourceException)
      {
        return PartialView("_AccountBox");
      }
    }
  }
}