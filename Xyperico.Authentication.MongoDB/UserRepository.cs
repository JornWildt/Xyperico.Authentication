using System;
using CuttingEdge.Conditions;
using MongoDB.Driver.Builders;


namespace Xyperico.Authentication.MongoDB
{
  public class UserRepository : GenericRepository<User, Guid>, IUserRepository
  {
    public override void Setup()
    {
      base.Setup();
      Collection.EnsureIndex(new IndexKeysBuilder().Ascending("EMailLowercase"), IndexOptions.SetUnique(true));
      Collection.EnsureIndex(new IndexKeysBuilder().Ascending("UserNameLowercase"), IndexOptions.SetUnique(true));
    }

    
    #region IUserRepository Members

    public User GetUserByUserName(string username)
    {
      Condition.Requires(username, "username").IsNotNull();

      return FindSingle(new { UserName = username });
    }

    #endregion
  }
}
