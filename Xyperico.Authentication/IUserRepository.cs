using System;


namespace Xyperico.Authentication
{
  public interface IUserRepository
  {
    void Add(User user);
    User Get(Guid id);
    User GetByUserName(string username);
    void Update(User user);
    void Remove(Guid id);
  }
}
