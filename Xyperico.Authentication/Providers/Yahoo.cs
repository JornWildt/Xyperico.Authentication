using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;


namespace Xyperico.Authentication.Providers
{
  public class Yahoo : IAuthenticationProvider
  {
    public IAuthenticationClient AuthenticationClient
    {
      get;
      protected set;
    }


    public Yahoo(string clientId, string clientSecret)
    {
      AuthenticationClient = new YahooOpenIdClient();
    }
  }
}
