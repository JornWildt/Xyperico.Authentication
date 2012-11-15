using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;


namespace Xyperico.Authentication.Providers
{
  public class Google : IAuthenticationProvider
  {
    public IAuthenticationClient AuthenticationClient
    {
      get;
      protected set;
    }


    public Google(string clientId, string clientSecret)
    {
      AuthenticationClient = new GoogleOpenIdClient();
    }
  }
}
