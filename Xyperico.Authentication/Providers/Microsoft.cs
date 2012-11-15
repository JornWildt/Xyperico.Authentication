using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;


namespace Xyperico.Authentication.Providers
{
  public class Microsoft : IAuthenticationProvider
  {
    public IAuthenticationClient AuthenticationClient
    {
      get;
      protected set;
    }


    public Microsoft(string clientId, string clientSecret)
    {
      AuthenticationClient = new MicrosoftClient(clientId, clientSecret);
    }
  }
}
