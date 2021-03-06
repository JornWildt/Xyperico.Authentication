﻿using NUnit.Framework;
using Xyperico.Base.Exceptions;
using System;


namespace Xyperico.Authentication.Tests
{
  [TestFixture]
  public class UserRepositoryTests : TestHelper
  {
    IUserRepository UserRepository = new Xyperico.Authentication.MongoDB.UserRepository();


    [Test]
    public void CanAddAndGetUser()
    {
      // Arrange
      User u1 = UserBuilder.BuildUser();

      // Act
      User u2 = UserRepository.Get(u1.Id);

      // Assert
      Assert.IsNotNull(u2);
      Assert.AreEqual(u1.Id, u2.Id);
      Assert.AreNotEqual(u1.Id, Guid.Empty, "Persistence layer must assign IDs");
      Assert.AreEqual(u1.UserName, u2.UserName);
    }


    [Test]
    public void CanGetUserByUserName()
    {
      // Arrange
      User u1 = UserBuilder.BuildUser();

      // Act
      User u2 = UserRepository.GetByUserName(u1.UserName);

      // Assert
      Assert.IsNotNull(u2);
      Assert.AreEqual(u1.Id, u2.Id);
      Assert.AreEqual(u1.UserName, u2.UserName);
    }


    [Test]
    public void WhenGettingUserByUserNameItIgnoresCasing()
    {
      // Arrange
      User u1 = UserBuilder.BuildUser();

      // Act
      User u2 = UserRepository.GetByUserName(u1.UserName.ToLower());
      User u3 = UserRepository.GetByUserName(u1.UserName.ToUpper());

      // Assert
      Assert.IsNotNull(u2);
      Assert.AreEqual(u1.Id, u2.Id);
      Assert.AreEqual(u1.UserName, u2.UserName);
      Assert.IsNotNull(u3);
      Assert.AreEqual(u1.Id, u3.Id);
      Assert.AreEqual(u1.UserName, u3.UserName);
    }


    [Test]
    public void WhenGettingUnknownUserByUserNameItThrowsMissingResource()
    {
      AssertThrows<MissingResourceException>(() => UserRepository.GetByUserName("unknownuser"));
    }


    [Test]
    public void CanGetUserByEMail()
    {
      // Arrange
      User u1 = UserBuilder.BuildUser();

      // Act
      User u2 = UserRepository.GetByEMail(u1.EMail);

      // Assert
      Assert.IsNotNull(u2);
      Assert.AreEqual(u1.Id, u2.Id);
      Assert.AreEqual(u1.EMail, u2.EMail);
    }


    [Test]
    public void WhenGettingUserByEMailItIgnoresCasing()
    {
      // Arrange
      User u1 = UserBuilder.BuildUser();

      // Act
      User u2 = UserRepository.GetByEMail(u1.EMail.ToLower());
      User u3 = UserRepository.GetByEMail(u1.EMail.ToUpper());

      // Assert
      Assert.IsNotNull(u2);
      Assert.AreEqual(u1.Id, u2.Id);
      Assert.AreEqual(u1.EMail, u2.EMail);
      Assert.IsNotNull(u3);
      Assert.AreEqual(u1.Id, u3.Id);
      Assert.AreEqual(u1.EMail, u3.EMail);
    }


    [Test]
    public void WhenGettingUnknownUserByEMailItThrowsMissingResource()
    {
      AssertThrows<MissingResourceException>(() => UserRepository.GetByEMail("unknownemail"));
    }


    [Test]
    public void WhenAddingSameUserNameTwiceItThrowsDuplicateKey()
    {
      // Arrange
      User u1 = UserBuilder.BuildUser(userName: "UPPERlower");
      User u2 = new User(u1.UserName.ToLower(), "123", "ll@ll.ll", UserNameValidator, EmptyPasswordPolicy);
      User u3 = new User(u1.UserName.ToUpper(), "123", "ll@ll.ll", UserNameValidator, EmptyPasswordPolicy);
      
      UserBuilder.RegisterInstance(u2);
      UserBuilder.RegisterInstance(u3);

      // Act + Assert
      AssertThrows<DuplicateKeyException>(
        () => UserRepository.Add(u2), 
        ex => ex.Key == "UserName");
      AssertThrows<DuplicateKeyException>(
        () => UserRepository.Add(u3),
        ex => ex.Key == "UserName");
    }


    [Test]
    public void WhenAddingSameEMailTwiceItThrowsDuplicateKey()
    {
      // Arrange
      User u1 = UserBuilder.BuildUser(email: "UPPERlower@DK.dk");
      User u2 = new User(u1.UserName + "2", "123", u1.EMail.ToUpper(), UserNameValidator, EmptyPasswordPolicy);
      User u3 = new User(u1.UserName + "3", "123", u1.EMail.ToLower(), UserNameValidator, EmptyPasswordPolicy);

      UserBuilder.RegisterInstance(u2);
      UserBuilder.RegisterInstance(u3);

      // Act + Assert
      AssertThrows<DuplicateKeyException>(
        () => UserRepository.Add(u2),
        ex => ex.Key == "EMail");
      AssertThrows<DuplicateKeyException>(
        () => UserRepository.Add(u3),
        ex => ex.Key == "EMail");
    }


    [Test]
    public void CanAddTwoUsersWithoutPasswordAndEMail()
    {
      // Arrange
      User u1a = new User("Gert", null, null, UserNameValidator, EmptyPasswordPolicy);
      User u2a = new User("Pia", null, null, UserNameValidator, EmptyPasswordPolicy);

      UserBuilder.RegisterInstance(u1a);
      UserBuilder.RegisterInstance(u2a);

      // Act
      UserRepository.Add(u1a);
      UserRepository.Add(u2a);

      User u1b = UserRepository.GetByUserName(u1a.UserName);
      User u2b = UserRepository.GetByUserName(u2a.UserName);

      // Assert
      Assert.IsNotNull(u1b);
      Assert.IsNotNull(u2b);
    }


    [Test]
    public void CanUpdateUser()
    {
      // Arrange
      User u1 = UserBuilder.BuildUser();

      // Act
      u1.ChangePassword("xxxx", EmptyPasswordPolicy);
      UserRepository.Update(u1);

      User u2 = UserRepository.Get(u1.Id);

      // Assert
      Assert.IsTrue(u2.PasswordMatches("xxxx"));
    }


    [Test]
    public void CanGetUserByExternalLogin()
    {
      // Arrange - two users with different external logins
      User u1a = UserBuilder.BuildUser();
      u1a.AddExternalLogin("Google", "abc");
      UserRepository.Update(u1a);

      User u2a = UserBuilder.BuildUser("Kama", "kama@kama.dk");
      u2a.AddExternalLogin("Google", "123");
      UserRepository.Update(u2a);

      // Act
      User u1b = UserRepository.GetByExternalLogin("Google", "abc");
      User u2b = UserRepository.GetByExternalLogin("Google", "123");

      // Assert
      Assert.IsNotNull(u1b);
      Assert.IsNotNull(u2b);
      Assert.AreEqual(u1a.Id, u1b.Id);
      Assert.AreEqual(u2a.Id, u2b.Id);
      Assert.AreNotEqual(u1a.Id, u2a.Id);
    }


    [Test]
    public void WhenGettingUserByExternalLoginItThrowsMissingResource()
    {
      // Act + assert
      AssertThrows<MissingResourceException>(() => UserRepository.GetByExternalLogin("Google", "abc111"));
    }


    [Test]
    public void WhenAddingSameExternalLoginTwiceItThrowsDuplicateKey()
    {
      // Arrange
      User u1 = UserBuilder.BuildUser();
      u1.AddExternalLogin("Google", "abc");
      UserRepository.Update(u1);

      User u2 = new User("kima", "123", "lkj@lkj.dl", UserNameValidator, EmptyPasswordPolicy);
      u2.AddExternalLogin("Google", "abc");
      UserBuilder.RegisterInstance(u2);

      // Act + Assert
      AssertThrows<DuplicateKeyException>(
        () => UserRepository.Add(u2),
        ex => ex.Key == "ExternalLogin");
    }


    [Test]
    public void WhenUpdatingToExistingExternalLoginItThrowsDuplicateKey()
    {
      // Arrange
      User u1 = UserBuilder.BuildUser();
      u1.AddExternalLogin("Google", "abc");
      UserRepository.Update(u1);

      User u2 = UserBuilder.BuildUser("Kimmy", "lkj@lkj.do");
      u2.AddExternalLogin("Google", "abc");

      // Act + Assert
      AssertThrows<DuplicateKeyException>(
        () => UserRepository.Update(u2),
        ex => ex.Key == "ExternalLogin");
    }


    [Test]
    public void WhenAddingTwiceItGivesNewUserIdNumbers()
    {
      // Act
      User u1 = UserBuilder.BuildUser("lll1", "ll1@ll.dk");
      User u2 = UserBuilder.BuildUser("lll2", "ll2@ll.dk");

      // Assert
      Assert.Greater(u1.UserId, 0);
      Assert.AreEqual(u1.UserId + 1, u2.UserId);
    }
  }
}
