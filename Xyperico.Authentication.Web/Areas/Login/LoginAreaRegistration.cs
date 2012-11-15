using System.Web.Mvc;
using log4net;
using Microsoft.Web.WebPages.OAuth;
using DotNetOpenAuth.AspNet.Clients;
using System.Collections.Generic;
using System;
using DotNetOpenAuth.AspNet;
using Xyperico.Base;


namespace Xyperico.Authentication.Web.Areas.Login
{
  public class LoginAreaRegistration : AreaRegistration
  {
    ILog Logger = LogManager.GetLogger(typeof(LoginAreaRegistration));

    public override string AreaName
    {
      get
      {
        return "Login";
      }
    }

    public override void RegisterArea(AreaRegistrationContext context)
    {
      Logger.Debug("Register Login area");

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
          IAuthenticationProvider provider = (IAuthenticationProvider)Activator.CreateInstance(providerType, args);

          OAuthWebSecurity.RegisterClient(provider.AuthenticationClient, providerCfg.DisplayName, new Dictionary<string, object>());
        }
      }
    }
  }
}
