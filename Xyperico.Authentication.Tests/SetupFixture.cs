﻿using NUnit.Framework;
using Xyperico.Authentication.Tests.Builders;
using Xyperico.Base;


namespace Xyperico.Authentication.Tests
{
  [SetUpFixture]
  public class SetupFixture
  {
    public static void Setup(IObjectContainer container)
    {
      Xyperico.Authentication.MongoDB.Utility.Initialize(container);

      container.AddComponent<IUserBuilder, UserBuilder>();
    }


    [SetUp]
    public void TestSetup()
    {
      Xyperico.Base.Testing.TestHelper.ClearObjectContainer();
      Setup(Xyperico.Base.Testing.TestHelper.ObjectContainer);
    }
  }
}
