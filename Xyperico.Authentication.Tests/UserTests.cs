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


    [Test]
    public void CanChangeEMail()
    {
      // Arrange
      User u = new User("Bent", null, null);

      // Act
      u.ChangeEMail("abc@DE.DK");

      // Assert
      Assert.AreEqual("abc@DE.DK", u.EMail);
      Assert.AreEqual("abc@de.dk", u.EMailLowercase);
    }


    [Test]
    public void CanChangeEMailToNull()
    {
      // Arrange
      User u = new User("Bent", null, "mymail@lkj.dk");

      // Act
      u.ChangeEMail(null);

      // Assert
      Assert.IsNull(u.EMail);
      Assert.IsNull(u.EMailLowercase);
    }
  }
}
