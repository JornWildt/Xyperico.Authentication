using Xyperico.Base.Testing;
using System;


namespace Xyperico.Authentication.Tests.Builders
{
  public class UserAuthRelationBuilder : DisposingBuilder<UserAuthRelation>, IUserAuthRelationBuilder
  {
    #region Dependencies

    public IUserAuthRelationRepository UserAuthRelationRepository { get; set; }

    #endregion


    #region IUserAuthRelationBuilder Members

    public UserAuthRelation BuildUserAuthRelation(string provider, string providerUserId)
    {
      UserAuthRelation u = new UserAuthRelation(Guid.NewGuid(), provider ?? "google", providerUserId ?? "http://googlesomething");
      RegisterInstance(u);
      UserAuthRelationRepository.Add(u);
      return u;
    }

    #endregion


    protected override void DisposeInstance(UserAuthRelation UserAuthRelation)
    {
      UserAuthRelationRepository.Remove(UserAuthRelation.Id);
    }
  }
}
