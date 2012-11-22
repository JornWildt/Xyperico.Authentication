using Xyperico.Base.Testing;


namespace Xyperico.Authentication.Tests.Builders
{
  public interface IUserAuthRelationBuilder : IDisposingBuilder<UserAuthRelation>
  {
    UserAuthRelation BuildUserAuthRelation(string provider = null, string providerUserId = null);
  }
}
