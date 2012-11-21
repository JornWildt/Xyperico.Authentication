using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Xyperico.Authentication.Tests
{
  [TestFixture]
  public class UserTests : TestHelper
  {
    [Test]
    public void WhenComparingIdenticalPasswordsItReturnsTrue()
    {
      // Arrange
      User u = new User("Bent", "123", "q@q.dk");

      // Act
      bool matches = u.PasswordMatches("123");

      // Assert
      Assert.IsTrue(matches);
    }


    [Test]
    public void WhenComparingDifferentPasswordsItReturnsFalse()
    {
      // Arrange
      User u = new User("Bent", "123", "q@q.dk");

      // Act
      bool matches = u.PasswordMatches("abc");

      // Assert
      Assert.IsFalse(matches);
    }


    [Test]
    public void TwoUsersWithTheSamePasswordHasDifferentHashes()
    {
      // Arrange
      User u1 = new User("Bent", "123", "q@q.dk");
      User u2 = new User("Lisa", "123", "l@q.dk");

      // Act
      bool matches = u1.PasswordHash.SequenceEqual(u2.PasswordHash);

      // Assert
      Assert.IsFalse(matches);
    }
  }
}
