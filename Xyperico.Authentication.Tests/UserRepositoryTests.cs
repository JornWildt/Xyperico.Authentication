using NUnit.Framework;
using Xyperico.Base.Exceptions;


namespace Xyperico.Authentication.Tests
{
  [TestFixture]
  public class UserRepositoryTests : TestHelper
  {
    IUserRepository UserRepository = new Xyperico.Authentication.MongoDB.UserRepository();


    [Test]
    public void CanAddAndGetUser()
    {
      // Arrange
      User u1 = UserBuilder.BuildUser();

      // Act
      User u2 = UserRepository.GetUserByUserName(u1.UserName);

      // Assert
      Assert.IsNotNull(u2);
      Assert.AreEqual(u1.Id, u2.Id);
      Assert.AreEqual(u1.UserName, u2.UserName);
    }


    [Test]
    public void WhenGettingUnknownUserItThrowsMissingResource()
    {
      AssertThrows<MissingResourceException>(() => UserRepository.GetUserByUserName("unknownuser"));
    }
  }
}
