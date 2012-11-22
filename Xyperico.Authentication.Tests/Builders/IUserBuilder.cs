using Xyperico.Base.Testing;


namespace Xyperico.Authentication.Tests.Builders
{
  public interface IUserBuilder : IDisposingBuilder<User>
  {
    User BuildUser(string userName = null, string email = null);
  }
}
