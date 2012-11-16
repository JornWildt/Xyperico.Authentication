using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Xyperico.Authentication.Web.Areas.Account.Controllers
{
  public class InfoController : Xyperico.Web.Mvc.Controller
  {
    [ChildActionOnly]
    [AllowAnonymous]
    public ActionResult AccountBox()
    {
      return PartialView("_AccountBox");
    }
  }
}