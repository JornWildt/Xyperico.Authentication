using System;


namespace Xyperico.Authentication
{
  public interface IUserRepository
  {
    void Add(User user);
    User Get(int id);
    User GetByUserName(string username);
    User GetByExternalLogin(string provider, string providerUserId);
    void Update(User user);
    void Remove(int id);
  }
}
