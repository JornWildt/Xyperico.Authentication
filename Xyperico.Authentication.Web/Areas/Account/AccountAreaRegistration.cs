using System.Web.Mvc;
using log4net;
using Xyperico.Base;

namespace Xyperico.Authentication.Web.Areas.Account
{
  public class AccountAreaRegistration : AreaRegistration
  {
    ILog Logger = LogManager.GetLogger(typeof(AccountAreaRegistration));

    public override string AreaName
    {
      get
      {
        return "Account";
      }
    }

    public override void RegisterArea(AreaRegistrationContext context)
    {
      Logger.Debug("Register Account area");

      context.MapRoute(
          "Account_default",
          "app/account/{controller}/{action}",
          new { controller = "home", action = "show" }
      );

      ConfigureDependencies(ObjectContainer.Container);
    }


    private void ConfigureDependencies(IObjectContainer container)
    {
    }
  }
}
