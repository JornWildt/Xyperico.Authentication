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
  }
}
