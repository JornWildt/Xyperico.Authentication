using DotNetOpenAuth.AspNet;


namespace Xyperico.Authentication
{
  public interface IAuthenticationProvider
  {
    IAuthenticationClient AuthenticationClient { get; }
  }
}
