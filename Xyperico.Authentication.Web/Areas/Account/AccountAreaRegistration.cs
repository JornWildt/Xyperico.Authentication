using System;
using System.Collections.Generic;
using System.Web.Mvc;
using log4net;
using Microsoft.Web.WebPages.OAuth;
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

      context.MapRoute(
          "Login_default",
          "app/login/{controller}/{action}",
          new { controller = "login", action = "show" }
      );

      ConfigureDependencies(ObjectContainer.Container);
      ConfigureAuthentication();
    }


    private void ConfigureDependencies(IObjectContainer container)
    {
      container.AddComponent<IAuthenticationService, AuthenticationService>();
    }


    private void ConfigureAuthentication()
    {
      //System.Web.Security.Membership.Provider = new SimpleMembershipProvider();
      //WebSecurity.InitializeDatabaseConnection("x", "u", "ui", "un", false);

      if (Configuration.Settings == null || Configuration.Settings.ExternalProviders == null)
        return;

      foreach (Configuration.AuthenticationProvider providerCfg in Configuration.Settings.ExternalProviders)
      {
        if (providerCfg.Active)
        {
          Type providerType = Type.GetType(providerCfg.Type);
          object[] args = new object[]
          {
            providerCfg.ClientId,
            providerCfg.ClientSecret
          };
          IExternalAuthenticationProvider provider = (IExternalAuthenticationProvider)Activator.CreateInstance(providerType, args);

          OAuthWebSecurity.RegisterClient(provider.AuthenticationClient, providerCfg.DisplayName, new Dictionary<string, object>());
        }
      }
    }
  }
}
