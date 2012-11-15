using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;


namespace Xyperico.Authentication.Providers
{
  public class Facebook : IAuthenticationProvider
  {
    public IAuthenticationClient AuthenticationClient
    {
      get;
      protected set;
    }


    public Facebook(string clientId, string clientSecret)
    {
      AuthenticationClient = new FacebookClient(clientId, clientSecret);
    }
  }
}
