using Xyperico.Base.Testing;


namespace Xyperico.Authentication.Tests.Builders
{
  public class UserBuilder : DisposingBuilder<User>, IUserBuilder
  {
    #region Dependencies

    public IUserRepository UserRepository { get; set; }
    public IUserNameValidator UserNameValidator { get; set; }

    #endregion


    #region IUserBuilder Members

    public User BuildUser(string userName = null, string email = null)
    {
      User u = new User(userName ?? "Berndt", "123456", email ?? "xx@xyz.dk", UserNameValidator);
      RegisterInstance(u);
      UserRepository.Add(u);
      return u;
    }

    #endregion


    protected override void DisposeInstance(User user)
    {
      UserRepository.Remove(user.Id);
    }
  }
}
