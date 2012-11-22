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


    [Test]
    public void WhenAddingSameUserNameTwiceItThrowsDuplicateKey()
    {
      // Arrange
      User u1 = UserBuilder.BuildUser(userName: "UPPERlower");
      User u2 = new User(u1.UserName.ToLower(), "123", "ll@ll.ll");
      User u3 = new User(u1.UserName.ToUpper(), "123", "ll@ll.ll");
      
      UserBuilder.RegisterInstance(u2);
      UserBuilder.RegisterInstance(u3);

      // Act + Assert
      AssertThrows<DuplicateKeyException>(() => UserRepository.Add(u2));
      AssertThrows<DuplicateKeyException>(() => UserRepository.Add(u3));
    }

    // FIXME: allow same e-mail? It's not a "key" like the user name

    [Test]
    public void WhenAddingSameEMailTwiceItThrowsDuplicateKey()
    {
      // Arrange
      User u1 = UserBuilder.BuildUser(email: "UPPERlower@DK.dk");
      User u2 = new User(u1.UserName + "2", "123", u1.EMail.ToUpper());
      User u3 = new User(u1.UserName + "3", "123", u1.EMail.ToLower());

      UserBuilder.RegisterInstance(u2);
      UserBuilder.RegisterInstance(u3);

      // Act + Assert
      AssertThrows<DuplicateKeyException>(() => UserRepository.Add(u2));
      AssertThrows<DuplicateKeyException>(() => UserRepository.Add(u3));
    }
  }
}
