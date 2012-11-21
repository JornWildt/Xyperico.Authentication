namespace Xyperico.Authentication
{
  public interface IAuthenticationService
  {
    bool LoginUsernamePassword(string userName, string password);
  }
}
