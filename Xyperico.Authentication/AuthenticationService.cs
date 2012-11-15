namespace Xyperico.Authentication
{
  public class AuthenticationService : IAuthenticationService
  {
    #region IAuthenticationService Members

    public bool LoginUsernamePassword(string userName, string password, bool persistCookie = false)
    {
      return true;
    }

    #endregion
  }
}
