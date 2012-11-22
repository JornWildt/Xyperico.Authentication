using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Xyperico.Authentication.Tests
{
  [TestFixture]
  public class AuthenticationServiceTests : TestHelper
  {
    public IAuthenticationService AuthenticationService = new AuthenticationService();


    //[Test]
    //public void WhenAuthenticatingWithValidCredentialsItReturnsTrue()
    //{
    //  // Arrange

    //  // Act
    //  bool isAuthenticated = AuthenticationService.LoginUsernamePassword("Berndt", "123456");

    //  // Assert
    //  Assert.IsTrue(isAuthenticated);
    //}


    //[Test]
    //public void WhenAuthenticatingWithInvalidCredentialsItReturnsFalse()
    //{
    //  // Arrange

    //  // Act
    //  bool isAuthenticated1 = AuthenticationService.LoginUsernamePassword("Berndt", "-wrong-");
    //  bool isAuthenticated2 = AuthenticationService.LoginUsernamePassword("-unknown-", "123456");

    //  // Assert
    //  Assert.IsTrue(isAuthenticated1);
    //  Assert.IsFalse(isAuthenticated2);
    //}
  }
}
