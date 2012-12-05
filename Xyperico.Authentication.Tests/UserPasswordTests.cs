using System.Linq;
using NUnit.Framework;


namespace Xyperico.Authentication.Tests
{
  [TestFixture]
  public class UserPasswordTests : TestHelper
  {
    [Test]
    public void WhenComparingIdenticalPasswordsItReturnsTrue()
    {
      // Arrange
      User u = new User("Bent", "123", "q@q.dk", UserNameValidator, EmptyPasswordPolicy);

      // Act
      bool matches = u.PasswordMatches("123");

      // Assert
      Assert.IsTrue(matches);
    }


    [Test]
    public void WhenComparingDifferentPasswordsItReturnsFalse()
    {
      // Arrange
      User u = new User("Bent", "123", "q@q.dk", UserNameValidator, EmptyPasswordPolicy);

      // Act
      bool matches = u.PasswordMatches("abc");

      // Assert
      Assert.IsFalse(matches);
    }


    [Test]
    public void TwoUsersWithTheSamePasswordHasDifferentHashes()
    {
      // Arrange
      User u1 = new User("Bent", "123", "q@q.dk", UserNameValidator, EmptyPasswordPolicy);
      User u2 = new User("Lisa", "123", "l@q.dk", UserNameValidator, EmptyPasswordPolicy);

      // Act
      bool matches = u1.PasswordHash.SequenceEqual(u2.PasswordHash);

      // Assert
      Assert.IsFalse(matches);
    }

    
    [Test]
    public void CanChangePasswordHashingAlgorithmAndStillValidateOldPasswords()
    {
      // Arrange
      User u1 = new User("Bent", "123", "q@q.dk", UserNameValidator, EmptyPasswordPolicy);
      Configuration.Settings.PasswordHashAlgorithm = "MD5";
      User u2 = new User("Lisa", "123", "l@q.dk", UserNameValidator, EmptyPasswordPolicy);

      // Act
      bool validates1 = u1.PasswordMatches("123");
      bool validates2 = u2.PasswordMatches("123");

      // Assert
      Assert.IsTrue(validates1);
      Assert.IsTrue(validates2);
      Assert.AreNotEqual(u1.PasswordHashAlgorithm, u2.PasswordHashAlgorithm);
    }


    [Test]
    public void CanChangePassword()
    {
      // Arrange
      User u = new User("Adam", "123", "lkl@mlml.dl", UserNameValidator, EmptyPasswordPolicy);

      // Act
      u.ChangePassword("456", EmptyPasswordPolicy);

      // Assert
      Assert.IsFalse(u.PasswordMatches("123"));
      Assert.IsTrue(u.PasswordMatches("456"));
    }


    [Test]
    public void CanChangePasswordToNull()
    {
      // Arrange
      User u = new User("Adam", "123", "lkl@mlml.dl", UserNameValidator, EmptyPasswordPolicy);

      // Act
      u.ChangePassword(null, null);

      // Assert
      Assert.IsFalse(u.PasswordMatches("123"));
      Assert.IsFalse(u.PasswordMatches(null));
    }


    [Test]
    public void ItDoesRespectPasswordPolicy()
    {
      // Arrange
      PasswordPolicy p = new PasswordPolicy { MinPasswordLength = 3 };

      // Act + Assert
      AssertThrows<InvalidPasswordException>(() => new User("Adam", "x", "lkl@mlml.dl", UserNameValidator, p));
    }


    [Test]
    public void WhenChangingPasswordItRespectPasswordPolicy()
    {
      // Arrange
      PasswordPolicy policy = new PasswordPolicy { MinPasswordLength = 3 };
      User u = new User("Bent", null, "mymail@lkj.dk", UserNameValidator, null);

      // Act + Assert
      AssertThrows<InvalidPasswordException>(() => u.ChangePassword("x", policy));
    }
  }
}
