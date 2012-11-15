using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;


namespace Xyperico.Authentication.Providers
{
  public class Twitter : IAuthenticationProvider
  {
    public IAuthenticationClient AuthenticationClient
    {
      get;
      protected set;
    }


    public Twitter(string clientId, string clientSecret)
    {
      AuthenticationClient = new TwitterClient(clientId, clientSecret);
    }
  }
}
