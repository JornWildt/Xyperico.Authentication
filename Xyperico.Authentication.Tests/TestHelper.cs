﻿using Xyperico.Authentication.Tests.Builders;


namespace Xyperico.Authentication.Tests
{
  public class TestHelper : Xyperico.Base.Testing.TestHelper
  {
    protected IUserBuilder UserBuilder = ObjectContainer.Resolve<IUserBuilder>();
    protected IUserAuthRelationBuilder UserAuthRelationBuilder = ObjectContainer.Resolve<IUserAuthRelationBuilder>();


    protected override void TearDown()
    {
      base.TestFixtureTearDown();
      UserBuilder.DisposeInstances();
      UserAuthRelationBuilder.DisposeInstances();
    }
  }
}