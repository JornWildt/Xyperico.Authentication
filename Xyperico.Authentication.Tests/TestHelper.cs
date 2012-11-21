using Xyperico.Authentication.Tests.Builders;
namespace Xyperico.Authentication.Tests
{
  public class TestHelper : Xyperico.Base.Testing.TestHelper
  {
    public const string KnownUserName = "Berndt";
    public const string KnownUserPassword = "123456";

    protected IUserBuilder UserBuilder = ObjectContainer.Resolve<IUserBuilder>();


    protected override void TestFixtureTearDown()
    {
      base.TestFixtureTearDown();
      UserBuilder.DisposeInstances();
    }
  }
}
