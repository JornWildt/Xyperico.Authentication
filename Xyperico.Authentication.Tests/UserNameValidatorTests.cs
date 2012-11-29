using NUnit.Framework;


namespace Xyperico.Authentication.Tests
{
  [TestFixture]
  public class UserNameValidatorTests : TestHelper
  {
    int OldUserNameMinLength;
    int OldUserNameMaxLength;

    protected override void SetUp()
    {
      base.SetUp();
      OldUserNameMinLength = Configuration.Settings.UserName.MinLength;
      OldUserNameMaxLength = Configuration.Settings.UserName.MaxLength;
    }


    protected override void TearDown()
    {
      Configuration.Settings.UserName.MinLength = OldUserNameMinLength;
      Configuration.Settings.UserName.MaxLength = OldUserNameMaxLength;
      base.TearDown();
    }


    [Test]
    public void ItRejectsInvalidUserNames()
    {
      Assert.IsFalse(UserNameValidator.IsValidUserName(null));
      Assert.IsFalse(UserNameValidator.IsValidUserName(""));
      Assert.IsFalse(UserNameValidator.IsValidUserName(" "));
      Assert.IsFalse(UserNameValidator.IsValidUserName("*"));
      Assert.IsFalse(UserNameValidator.IsValidUserName("aaa*aaa"));
      Assert.IsFalse(UserNameValidator.IsValidUserName("bb/lkj"));
      Assert.IsFalse(UserNameValidator.IsValidUserName("q+u"));
      Assert.IsFalse(UserNameValidator.IsValidUserName("app"));
      Assert.IsFalse(UserNameValidator.IsValidUserName("api"));
      Assert.IsFalse(UserNameValidator.IsValidUserName("css"));
      Assert.IsFalse(UserNameValidator.IsValidUserName("-"), "Must begin with letter or digit");
      Assert.IsFalse(UserNameValidator.IsValidUserName("."), "Must begin with letter or digit");
      Assert.IsFalse(UserNameValidator.IsValidUserName("_"), "Must begin with letter or digit");
      Assert.IsFalse(UserNameValidator.IsValidUserName("abcde123450123456789x"), "user name too long (max. 20 chars)");
    }


    [Test]
    public void ItAcceptsValidUserNames()
    {
      Assert.IsTrue(UserNameValidator.IsValidUserName("a"));
      Assert.IsTrue(UserNameValidator.IsValidUserName("A"));
      Assert.IsTrue(UserNameValidator.IsValidUserName("bente_bent"));
      Assert.IsTrue(UserNameValidator.IsValidUserName("0"));
      Assert.IsTrue(UserNameValidator.IsValidUserName("0233"));
      Assert.IsTrue(UserNameValidator.IsValidUserName("abcde123450123456789"), "Allow 20 characters for user name");
    }


    [Test]
    public void ItRejectsInvalidUserNamesInDictionaryFile()
    {
      Assert.IsFalse(UserNameValidator.IsValidUserName("admin"));
      Assert.IsFalse(UserNameValidator.IsValidUserName("administrator"));
    }


    [Test]
    public void ItOnlyLoadsTheInvalidUserNameFileOnce()
    {
      Assert.AreEqual(1, Xyperico.Authentication.FilebasedUserNameValidator.FileReloadTimes);
    }


    [Test]
    public void ItRespectsMinAndMaxUserLengthFromConfiguration()
    {
      Configuration.Settings.UserName.MinLength = 10;
      Configuration.Settings.UserName.MaxLength = 13;
      Assert.IsTrue(UserNameValidator.IsValidUserName("1234567890123"), "Allow 13 characters for user name");
      Assert.IsTrue(UserNameValidator.IsValidUserName("1234567890"), "Allow 10 characters for user name");
      Assert.IsFalse(UserNameValidator.IsValidUserName("123456789"), "Disallow <10 characters for user name");
      Assert.IsFalse(UserNameValidator.IsValidUserName("12345678901234"), "Disallow >13 characters for user name");
    }
  }
}
