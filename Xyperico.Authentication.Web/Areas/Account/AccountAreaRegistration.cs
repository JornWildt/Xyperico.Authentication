using System;
using System.Collections.Generic;
using System.Web.Mvc;
using log4net;
using Microsoft.Web.WebPages.OAuth;
using Xyperico.Base;
using BaseConfiguration = Xyperico.Authentication.Configuration;
using Xyperico.Authentication.ConfigurationElements;


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
          new { controller = "login", action = "show" }
      );

      ConfigureDependencies(ObjectContainer.Container);
      ConfigureAuthentication();
    }


    private void ConfigureDependencies(IObjectContainer container)
    {
      Xyperico.Authentication.MongoDB.Utility.Initialize(container);
      container.AddComponent<IUserNameValidator, FilebasedUserNameValidator>();
    }


    private void ConfigureAuthentication()
    {
      if (BaseConfiguration.Settings == null || BaseConfiguration.Settings.ExternalProviders == null)
        return;

      foreach (AuthenticationProviderSection providerCfg in BaseConfiguration.Settings.ExternalProviders)
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
