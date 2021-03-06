﻿using System.Linq;
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
      User u = new User("Bent", null, null, UserNameValidator, null);

      // Act
      bool matches = u.PasswordMatches("123");

      // Assert
      Assert.IsFalse(matches);
    }


    [Test]
    public void CanAssociateUserWithExternalLogin()
    {
      // Arrange
      User u = new User("Bent", null, null, UserNameValidator, EmptyPasswordPolicy);

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
      User u = new User("Bent", null, null, UserNameValidator, EmptyPasswordPolicy);

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
      User u = new User("Bent", null, null, UserNameValidator, EmptyPasswordPolicy);

      // Act
      u.ChangeEMail("abc@DE.DK");

      // Assert
      Assert.AreEqual("abc@DE.DK", u.EMail);
    }


    [Test]
    public void CanChangeEMailToNull()
    {
      // Arrange
      User u = new User("Bent", null, "mymail@lkj.dk", UserNameValidator, null);

      // Act
      u.ChangeEMail(null);

      // Assert
      Assert.IsNull(u.EMail);
    }


    [Test]
    public void CannotCreateUserWithInvalidUserName()
    {
      AssertThrows<InvalidUserNameException>(() => new User("*", "lkj", "lkj@lkj.dk", UserNameValidator, EmptyPasswordPolicy));
    }
  }
}
