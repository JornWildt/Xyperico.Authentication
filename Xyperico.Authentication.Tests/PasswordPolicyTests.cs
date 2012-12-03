using NUnit.Framework;


namespace Xyperico.Authentication.Tests
{
  [TestFixture]
  public class PasswordPolicyTests : TestHelper
  {
    [Test]
    public void CanMatchPasswordLength()
    {
      // Arrange
      PasswordPolicy policy = new PasswordPolicy
      {
        MinPasswordLength = 5
      };

      // Act + Assert
      Assert.IsTrue(policy.IsValid("12345"));
      Assert.IsFalse(policy.IsValid("1234"));
    }


    [Test]
    public void CanMatchMinNoOfUpperCaseChars()
    {
      // Arrange
      PasswordPolicy policy = new PasswordPolicy
      {
        MinNoOfUpperCaseChars = 3
      };

      // Act + Assert
      Assert.IsTrue(policy.IsValid("1A2BC"));
      Assert.IsFalse(policy.IsValid("1a2bc"));
      Assert.IsFalse(policy.IsValid("1a2Bc"));
    }


    [Test]
    public void CanMatchMinNoOfMinNoOfLowerCaseChars()
    {
      // Arrange
      PasswordPolicy policy = new PasswordPolicy
      {
        MinNoOfLowerCaseChars = 3
      };

      // Act + Assert
      Assert.IsTrue(policy.IsValid("1a2bc"));
      Assert.IsFalse(policy.IsValid("1A2bC"));
      Assert.IsFalse(policy.IsValid("1A2BC"));
    }


    [Test]
    public void CanMatchMinNoOfNumbers()
    {
      // Arrange
      PasswordPolicy policy = new PasswordPolicy
      {
        MinNoOfNumbers = 3
      };

      // Act + Assert
      Assert.IsTrue(policy.IsValid("a12b3"));
      Assert.IsFalse(policy.IsValid("a1xb3"));
    }


    [Test]
    public void CanMatchMaxNoOfAllowedCharacterRepetitions()
    {
      // Arrange
      PasswordPolicy policy = new PasswordPolicy
      {
        MaxNoOfAllowedCharacterRepetitions = 2
      };

      // Act + Assert
      Assert.IsTrue(policy.IsValid("aabb"));
      Assert.IsFalse(policy.IsValid("xaaay"));
    }


    //[Test]
    //public void CanMatchMinNoOfSymbols()
    //{
    //  // Arrange
    //  PasswordPolicy policy = new PasswordPolicy
    //  {
    //    MinNoOfSymbols = 3
    //  };

    //  // Act + Assert
    //  Assert.IsTrue(policy.MatchPassword("a/&.k1"));
    //  Assert.IsFalse(policy.MatchPassword("a/&k1"));
    //}


    //[Test]
    //public void CanMatchMinNoOfUniCaseChars()
    //{
    //  // Arrange
    //  PasswordPolicy policy = new PasswordPolicy
    //  {
    //    MinNoOfUniCaseChars = 3
    //  };

    //  // Act + Assert
    //  Assert.IsTrue(policy.MatchPassword("a/&.k1"));
    //  Assert.IsFalse(policy.MatchPassword("a/&k1"));
    //}


    [Test]
    public void CanDescribeValidationRules()
    {
      // Arrange
      PasswordPolicy policy1 = new PasswordPolicy { MinPasswordLength = 5 };
      PasswordPolicy policy2 = new PasswordPolicy { MinNoOfLowerCaseChars = 2 };
      PasswordPolicy policy3 = new PasswordPolicy { MinNoOfUpperCaseChars = 2 };
      PasswordPolicy policy4 = new PasswordPolicy { MinNoOfNumbers = 2 };

      // Act + Assert
      Assert.AreEqual("password must be at least 5 characters long", policy1.GetDescription());
      Assert.AreEqual("password must contain at least 2 lower case characters", policy2.GetDescription());
      Assert.AreEqual("password must contain at least 2 upper case characters", policy3.GetDescription());
      Assert.AreEqual("password must contain at least 2 digits", policy4.GetDescription());
    }
  }
}
