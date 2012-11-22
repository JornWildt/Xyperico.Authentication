using System;
using Xyperico.Base;
using CuttingEdge.Conditions;


namespace Xyperico.Authentication
{
  public class UserAuthRelation : IHaveId<int>
  {
    #region Public persisted properties

    public int Id { get; set; }

    public Guid User_Id { get; protected set; }

    public string Provider { get; protected set; }

    public string ProviderUserId { get; protected set; }

    #endregion

    
    public UserAuthRelation()
    {
    }


    public UserAuthRelation(Guid user_id, string provider, string providerUserId)
    {
      Condition.Requires(provider, "provider").IsNotNullOrEmpty();
      Condition.Requires(providerUserId, "providerUserId").IsNotNullOrEmpty();

      Id = -1;
      User_Id = user_id;
      Provider = provider;
      ProviderUserId = providerUserId;
    }
  }
}
