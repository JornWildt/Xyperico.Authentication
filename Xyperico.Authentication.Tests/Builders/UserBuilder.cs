using Xyperico.Base.Testing;


namespace Xyperico.Authentication.Tests.Builders
{
  public class UserBuilder : DisposingBuilder<User>, IUserBuilder
  {
    #region Dependencies

    public IUserRepository UserRepository { get; set; }

    #endregion


    #region IUserBuilder Members

    public User BuildUser()
    {
      User u = new User("Berndt", "123456", "xx@xyz.dk");
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
