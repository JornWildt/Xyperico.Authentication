using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;


namespace Xyperico.Authentication.Providers
{
  public class LinkedIn : IAuthenticationProvider
  {
    public IAuthenticationClient AuthenticationClient
    {
      get;
      protected set;
    }


    public LinkedIn(string clientId, string clientSecret)
    {
      AuthenticationClient = new LinkedInClient(clientId, clientSecret);
    }
  }
}
