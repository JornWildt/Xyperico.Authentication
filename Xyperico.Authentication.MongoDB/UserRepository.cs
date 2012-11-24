using System;
using CuttingEdge.Conditions;
using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization;
using Xyperico.Base.MongoDB;


namespace Xyperico.Authentication.MongoDB
{
  public class UserRepository : GenericRepository<User, int>, IUserRepository
  {
    // Incremental IDs is not exacly tyhe best choice, but it is unfortunately required by SimpleMembershipProvider
    protected CounterCollection UserIdCounter { get; set; }


    public UserRepository()
    {
      UserIdCounter = new CounterCollection(MongoConfigEntry, "UserId");
    }


    public override void Setup()
    {
      base.Setup();
      Collection.EnsureIndex(
        new IndexKeysBuilder().Ascending("EMailLowercase"), 
        IndexOptions.SetSparse(true).SetUnique(true));
      
      Collection.EnsureIndex(
        new IndexKeysBuilder().Ascending("UserNameLowercase"), 
        IndexOptions.SetUnique(true));
      
      Collection.EnsureIndex(
        new IndexKeysBuilder().Ascending("ExternalLogins.Provider").Ascending("ExternalLogins.ProviderUserId"), 
        IndexOptions.SetSparse(true).SetUnique(true));
    }


    protected override string MapDuplicateKeyErrorToKeyName(string error)
    {
      if (error.Contains("$EMailLowercase"))
        return "EMail";
      else if (error.Contains("$UserNameLowercase"))
        return "UserName";
      else if (error.Contains("$ExternalLogins.Provider"))
        return "ExternalLogin";
      else
        return null;
    }

    
    #region IUserRepository Members

    public override void Add(User user)
    {
      user.Id = (int)UserIdCounter.Next();
      base.Add(user);
    }


    public User GetByUserName(string username)
    {
      Condition.Requires(username, "username").IsNotNull();

      return FindSingle(new { UserName = username });
    }


    public User GetByExternalLogin(string provider, string providerUserId)
    {
      Condition.Requires(provider, "provider").IsNotNull();
      Condition.Requires(providerUserId, "providerUserId").IsNotNull();

      var query = Query.ElemMatch("ExternalLogins", Query.And(Query.EQ("Provider", provider), Query.EQ("ProviderUserId", providerUserId)));

      return FindSingle(query);
    }

    #endregion
  }
}
