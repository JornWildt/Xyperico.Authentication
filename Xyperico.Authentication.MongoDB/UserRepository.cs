using System;
using CuttingEdge.Conditions;


namespace Xyperico.Authentication.MongoDB
{
  public class UserRepository : GenericRepository<User, Guid>, IUserRepository
  {
    #region IUserRepository Members

    public User GetUserByUserName(string username)
    {
      Condition.Requires(username, "username").IsNotNull();

      return FindSingle(new { UserName = username });
    }

    #endregion
  }
}
