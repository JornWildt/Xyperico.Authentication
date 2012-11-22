using CuttingEdge.Conditions;
using MongoDB.Driver.Builders;
using Xyperico.Base.MongoDB;


namespace Xyperico.Authentication.MongoDB
{
  public class UserAuthRelationRepository : GenericRepository<UserAuthRelation, int>, IUserAuthRelationRepository
  {
    protected CounterCollection UserIdCounter { get; set; }


    public UserAuthRelationRepository()
    {
      UserIdCounter = new CounterCollection(MongoConfigEntry, "UserAuthRelationUserId");
    }


    public override void Setup()
    {
      base.Setup();
      Collection.EnsureIndex(new IndexKeysBuilder().Ascending("Provider", "ProviderUserId"), IndexOptions.SetUnique(true));
    }


    #region IUserAuthRelationRepository Members

    public override void Add(UserAuthRelation rel)
    {
      rel.Id = (int)UserIdCounter.Next();
      base.Add(rel);
    }

    public UserAuthRelation GetByExternalCredentials(string provider, string providerUserId)
    {
      Condition.Requires(provider, "provider").IsNotNull();
      Condition.Requires(providerUserId, "providerUserId").IsNotNull();

      return FindSingle(new { Provider = provider, ProviderUserId = providerUserId });
    }

    #endregion
  }
}
