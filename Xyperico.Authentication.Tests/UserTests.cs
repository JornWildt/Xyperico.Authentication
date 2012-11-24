using System.Linq;
using NUnit.Framework;


namespace Xyperico.Authentication.Tests
{
  [TestFixture]
  public class UserTests : TestHelper
  {
    [Test]
    public void CanCreateUserWithoutPasswordAndEMail()
    {
      // Arrange
      User u = new User("Bent", null, null);

      // Act
      bool matches = u.PasswordMatches("123");

      // Assert
      Assert.IsFalse(matches);
    }


    [Test]
    public void CanAssociateUserWithExternalLogin()
    {
      // Arrange
      User u = new User("Bent", null, null);

      // Act
      u.AddExternalLogin("Google", "xyz");

      // Assert
      Assert.IsTrue(u.HasExternalLogin("Google", "xyz"));
      Assert.IsFalse(u.HasExternalLogin("Google", "123"));
    }


    [Test]
    public void WhenAddingSameExternalLoginTwiceItIgnoresSecond()
    {
      // Arrange
      User u = new User("Bent", null, null);

      // Act
      u.AddExternalLogin("Google", "xyz");
      u.AddExternalLogin("Google", "abc");
      u.AddExternalLogin("Google", "xyz");

      // Assert
      Assert.IsTrue(u.HasExternalLogin("Google", "xyz"));
      Assert.IsTrue(u.HasExternalLogin("Google", "abc"));
      Assert.AreEqual(2, u.GetExternalLogins().Count());
    }
  }
}
