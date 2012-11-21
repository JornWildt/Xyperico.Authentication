using System;


namespace Xyperico.Authentication
{
  public interface IUserRepository
  {
    void Add(User user);
    User GetUserByUserName(string username);
    void Remove(Guid id);
  }
}
