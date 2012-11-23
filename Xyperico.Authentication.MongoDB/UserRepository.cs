using System;
using CuttingEdge.Conditions;
using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization;


namespace Xyperico.Authentication.MongoDB
{
  public class UserRepository : GenericRepository<User, Guid>, IUserRepository
  {
    public override void Setup()
    {
      base.Setup();
      Collection.EnsureIndex(new IndexKeysBuilder().Ascending("EMailLowercase"), IndexOptions.SetSparse(true).SetUnique(true));
      Collection.EnsureIndex(new IndexKeysBuilder().Ascending("UserNameLowercase"), IndexOptions.SetUnique(true));
    }

    
    #region IUserRepository Members

    public User GetByUserName(string username)
    {
      Condition.Requires(username, "username").IsNotNull();

      return FindSingle(new { UserName = username });
    }

    #endregion
  }
}
